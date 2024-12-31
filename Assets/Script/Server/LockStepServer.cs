using LockStep;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class LockStepServer : MonoBehaviour
{
    public Text text;
    public GameObject prefabs;
    public Transform playerTrans;
    private ChasingFrames chasingFrames;
    private Dictionary<int, PlayerB2> players = new Dictionary<int, PlayerB2>();

    // 客户端处理的帧
    private int currentFrame = 1;
    private void Awake()
    {
        SubScribe();
    }

    private void Start()
    {

    }

    private void SubScribe()
    {
        Events.UpdateFrame += OnOperate;
        Events.ChasingFramesResponse += OnChasingFrames;
    }
    private void UnSubScribe()
    {
        Events.UpdateFrame -= OnOperate;
        Events.ChasingFramesResponse -= OnChasingFrames;
    }
    // 处理断线重连的追帧
    public void OnChasingFrames(ChasingFrames frames)
    {
        chasingFrames = frames;
        Debug.Log("收到消息了！" +  chasingFrames.Operates.Count);
        // 当前同步了的帧id为服务端的集合的最后一个元素的帧id
        currentFrame = chasingFrames.Operates[chasingFrames.Operates.Count - 1].FrameId;
        StartCoroutine(IChasingFrames());
    }
    // 开启协程追帧
    public IEnumerator IChasingFrames()
    {
        while (chasingFrames.Operates.Count > 0)
        {
            yield return new WaitForSeconds(0f); 
            Operate operate = chasingFrames.Operates[0];
            chasingFrames.Operates.RemoveAt(0);
            UpdatePlayers(operate);
        }
        // 重连状态结束
        Events.Reload.Call(false); 
    }

    // 收到服务端发送的一帧的操作
    public void OnOperate(Operate operate)
    {
        // 已经同步过这一帧了
        if (operate.FrameId <= currentFrame) return;
        // 同步到这一帧
        currentFrame = operate.FrameId;
        // 如果是正在追帧状态，那么数据先保存到集合中
        if (UdpClient.Instance.IsReload) 
        {
            // 追帧的集合再增加1帧
            chasingFrames.Operates.Add(operate);
            return;
        }
        
        UpdatePlayers(operate);

        // 在客户端收到服务端发过来的帧之后采集操作
        Operate op = new Operate()
        {
            FrameId = operate.FrameId + 1,
        };
        op.Move.Add(new Move()
        {
            UserId = UdpClient.Instance.UserId,
            MoveX = InputController.HorizontalMove,
            MoveY = InputController.VerticalMove,
        });
        Events.ChangeOperateRequest.Call(op);
    }

    public void InitPlayers(int userId)
    {
        GameObject go = Instantiate(prefabs, playerTrans);
        PlayerB2 b2 = go.AddComponent<PlayerB2>();
        b2.SetId(userId);
        players.Add(userId, go.AddComponent<PlayerB2>());
    }

    public void UpdatePlayers(Operate operate)
    {
        text.text = "当前帧:" + operate.FrameId;
        foreach (var item in operate.Move)
        {
            if (players.ContainsKey(item.UserId))
            {
                Events.UpdateMove.Call(item);
            }
            else
            {
                InitPlayers(item.UserId);
            }
        }
    }

    private void OnDestroy()
    {
        UnSubScribe();
    }
}
