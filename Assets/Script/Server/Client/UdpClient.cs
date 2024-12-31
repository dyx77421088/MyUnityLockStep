using LockStep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class UdpClient : MonoBehaviour
{
    private static UdpClient instance; // 单例模式
    private int userId;
    private bool isReload;
    public int UserId { get { return userId; } }
    public bool IsReload { get { return isReload; } }
    public static UdpClient Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
        RequestSubScribe(); // 开启request中的订阅
        DontDestroyOnLoad(gameObject);
        Events.LoginSuccess += OnLoginSuccess; // 登陆成功
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
        RequestUnSubScribe(); // 销毁request中的订阅
        Events.LoginSuccess -= OnLoginSuccess; // 登陆成功
        Events.Reload -= OnReload;
    }
}
