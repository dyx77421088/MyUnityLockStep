using Commit.Config;
using Commit.Utils;
using LockStep;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class UdpClient
{
    // 开启协程，接收消息
    private void StartReceiving()
    {
        // 使用协程接收数据
        StartCoroutine(ReceiveData());
    }

    // 启动协程
    private IEnumerator ReceiveData()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f); // 限制接收频率，可以调整

            if (udpClient.Available > 0)
            {
                ReceiveMessages(); // 处理接收的信息
            }
        }
    }

    private void ReceiveMessages()
    {
        IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, NetConfig.UDP_PORT);
        try
        {
            byte[] receivedData = udpClient.Receive(ref listenEndPoint); // 收到消息
            BaseResponse requset = ProtoBufUtils.DeSerializeBaseResponse(receivedData);
            // 处理接收到的消息
            HandleReceivedData(requset);
            
        }
        catch (ObjectDisposedException)
        {
            Debug.Log("UdpClient已关闭。");
            //break; // 可以选择停止接收
        }
        catch (Exception ex)
        {
            Debug.Log($"异常: {ex.Message}");
        }
    }

    private void HandleReceivedData(BaseResponse response)
    {
        if (response.ResponseType == ResponseType.RtLogin) // 如果是登陆请求
        {
            if (response.ResponseData == ResponseData.RdStatus) // 且携带的数据是status
            {
                Events.LoginResponse.Call(response); // 那么就是登陆请求后服务端给响应的status数据
            }
        }
        else if (response.ResponseType == ResponseType.RtOperate)
        {
            if (response.ResponseData == ResponseData.RdOperate) // 操作
            {
                Events.UpdateFrame.Call(response.Operate);
            }
            else if (response.ResponseData == ResponseData.RdChasingframes) // 追帧
            {
                Events.ChasingFramesResponse.Call(response.ChasingFrames);
            }
        }
    }


}
