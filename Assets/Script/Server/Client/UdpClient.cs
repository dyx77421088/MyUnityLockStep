using LockStep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class UdpClient : MonoBehaviour
{
    private static UdpClient instance; // ����ģʽ
    private int userId;
    private bool isReload;
    public int UserId { get { return userId; } }
    public bool IsReload { get { return isReload; } }
    public static UdpClient Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
        RequestSubScribe(); // ����request�еĶ���
        DontDestroyOnLoad(gameObject);
        Events.LoginSuccess += OnLoginSuccess; // ��½�ɹ�
        Events.Reload += OnReload;
        Application.targetFrameRate = 120;
    }
    void Start()
    {
        InitUdpClient();
        StartReceiving();
    }

    public void OnLoginSuccess(int userId)
    {
        this.userId = userId;
    }
    public void OnReload(bool isReload)
    {
        this.isReload = isReload;
    }

    private void OnDestroy()
    {
        Events.MatchRequest.Call(false);
        RequestUnSubScribe(); // ����request�еĶ���
        Events.LoginSuccess -= OnLoginSuccess; // ��½�ɹ�
        Events.Reload -= OnReload;
    }
}
