
using System.Net.Sockets;
using System.Net;
using System.Text;
using LockStep;
using UnityEngine;
using Commit.Config;
using Google.Protobuf;
using Commit.Utils;

public partial class UdpClient
{
    // 谁在发送，发送请求的udp
    private System.Net.Sockets.UdpClient udpClient = new System.Net.Sockets.UdpClient();
    // 发送给谁， 
    private IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(NetConfig.IP), NetConfig.UDP_PORT);

    public void RequestSubScribe()
    {
        Events.LoginRequest += Login; // 登陆
        Events.MatchRequest += Match;
        Events.ChangeOperateRequest += ChangeOperate;
        Events.ChasingFramesRequest += Reload;
    }
    public void RequestUnSubScribe()
    {
        Events.LoginRequest -= Login;
        Events.MatchRequest -= Match;
        Events.ChangeOperateRequest -= ChangeOperate;
        Events.ChasingFramesRequest -= Reload;
    }

    private void InitUdpClient()
    {
        udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0)); // 0：自己分配端口号 也可以指定具体的端口
    }
    // 登陆请求
    private void Login(string userName, string password)
    {
        BaseRequest request = new BaseRequest()
        {
            RequestType = RequestType.RtLogin,
            RequestData = RequestData.RdUser,
            User = new User()
            {
                Name = userName,
                Password = password
            }
        };
        byte[] msg = ProtoBufUtils.SerializeBaseRequest(request); // 实体类 => byte数组
        udpClient.Send(msg, msg.Length, serverEndPoint);
    }
    // 匹配（取消匹配）请求
    private void Match(bool isMatch)
    {
        BaseRequest request = new BaseRequest()
        {
            RequestType = RequestType.RtMatch,
            RequestData = RequestData.RdMatch,
            Matching = new Matching()
            {
                UserId = userId,
                IsMatch = isMatch
            }
        };
        byte[] msg = ProtoBufUtils.SerializeBaseRequest(request); // 实体类 => byte数组
        udpClient.Send(msg, msg.Length, serverEndPoint);
    }

    // 上传这一帧的操作
    public void ChangeOperate(Operate operate)
    {
        BaseRequest request = new BaseRequest()
        {
            RequestType = RequestType.RtOperate,
            RequestData = RequestData.RdOperate,
            Operate = operate
        };

        byte[] msg = ProtoBufUtils.SerializeBaseRequest(request); // 实体类 => byte数组
        udpClient.Send(msg, msg.Length, serverEndPoint);
    }

    // 重连
    public void Reload()
    {
        BaseRequest request = new BaseRequest()
        {
            RequestType = RequestType.RtMatch,
            RequestData = RequestData.RdStatus,
            Status = new Status()
            {
                Id = userId,
                St = StatusType.StReload,
            }
        };

        byte[] msg = ProtoBufUtils.SerializeBaseRequest(request); // 实体类 => byte数组
        udpClient.Send(msg, msg.Length, serverEndPoint);
    }
}
