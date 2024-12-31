
// 响应
namespace LockStep
{
    public partial class Events
    {
        public static Event<BaseResponse> LoginResponse; // 登陆响应

        public static Event<int> LoginSuccess; // 登陆成功！
        public static Event<bool> Reload; // 重连！

        public static Event<Operate> UpdateFrame; // 更新帧
        public static Event<Move> UpdateMove; // 更新移动
        public static Event<ChasingFrames> ChasingFramesResponse; // 追帧
    }

}