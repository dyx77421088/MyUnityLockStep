
// ��Ӧ
namespace LockStep
{
    public partial class Events
    {
        public static Event<BaseResponse> LoginResponse; // ��½��Ӧ

        public static Event<int> LoginSuccess; // ��½�ɹ���
        public static Event<bool> Reload; // ������

        public static Event<Operate> UpdateFrame; // ����֡
        public static Event<Move> UpdateMove; // �����ƶ�
        public static Event<ChasingFrames> ChasingFramesResponse; // ׷֡
    }

}