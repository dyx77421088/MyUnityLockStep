
// ����
namespace LockStep
{
    public partial class Events
    {
        
        public static Event<string, string> LoginRequest;// ��½����

        public static Event<bool> MatchRequest; // ƥ������(true:ƥ�䣬false��ȡ��ƥ��)
        public static Event ChasingFramesRequest; // ����

        public static Event<Operate> ChangeOperateRequest;
    }

}