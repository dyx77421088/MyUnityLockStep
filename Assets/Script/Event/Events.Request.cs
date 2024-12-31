
// 请求
namespace LockStep
{
    public partial class Events
    {
        
        public static Event<string, string> LoginRequest;// 登陆请求

        public static Event<bool> MatchRequest; // 匹配请求(true:匹配，false：取消匹配)
        public static Event ChasingFramesRequest; // 重连

        public static Event<Operate> ChangeOperateRequest;
    }

}