using LockStep;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MatchServer : MonoBehaviour
{
    public Text text;
    private Button btn;
    private bool isMatch;

    private void Awake()
    {
        SubScribe();
    }

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ClickMatch);
        if (UdpClient.Instance.IsReload) text.text = "重连！";
        else text.text = "匹配";
    }

    private void SubScribe()
    {
        Events.UpdateFrame += OnOperate;
    }
    private void UnSubScribe()
    {
        Events.UpdateFrame -= OnOperate;
    }

    // 绑定的按钮事件
    public void ClickMatch()
    {
        if (UdpClient.Instance.IsReload)
        {
            // 重连
            Debug.Log("重连");
            Events.ChasingFramesRequest.Call();
            return;
        }
        isMatch = !isMatch;
        text.text = isMatch ? "匹配中...." : "匹配";
        Events.MatchRequest.Call(isMatch); // 匹配        
    }

    public void OnOperate(Operate op)
    {
        btn.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnSubScribe();
    }
}
