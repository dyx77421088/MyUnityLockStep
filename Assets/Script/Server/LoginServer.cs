using LockStep;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginServer : MonoBehaviour
{
    public InputField userName;
    public InputField password;
    public Text message;

    private void Awake()
    {
        SubScribe();
    }

    private void SubScribe()
    {
        Events.LoginResponse += LoginResponse; // 登陆请求
        Events.LoginSuccess += OnLoginSuccess; // 登陆成功
    }
    private void UnSubScribe()
    {
        Events.LoginResponse -= LoginResponse;
        Events.LoginSuccess -= OnLoginSuccess;
    }

    /// <summary>
    /// 获得登陆消息的响应信息
    /// </summary>
    /// <param name="baseRequest">响应信息</param>
    public void LoginResponse(BaseResponse response)
    {
        message.text = response.Status.Msg;
        if (response.Status.St == StatusType.StSuccess || response.Status.St == StatusType.StReload)
        {
            Events.LoginSuccess.Call(response.Status.Id);
            if (response.Status.St == StatusType.StReload)
            {
                Events.Reload.Call(true);
            }
        }
    }

    /// <summary>
    /// 用户登录成功！
    /// </summary>
    public void OnLoginSuccess(int userId)
    {
        SceneManager.LoadScene(SceneConst.MATCH);
    }

    // 绑定的按钮事件
    public void ClickLogin()
    {
        Events.LoginRequest.Call(userName.text, password.text); // 发送登陆请求
    }

    private void OnDestroy()
    {
        UnSubScribe();
    }
}
