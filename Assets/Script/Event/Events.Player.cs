using System;
using UnityEngine;

namespace LockStep
{
    public partial class Events
    {
        // ��ǹ
        public static Event PlayerFire;
        
        public static Event PlayerShowGunWeapon; // �ó�ǹ
        public static Event PlayerHideGunWeapon; // ����ǹ

        public static Event PlayerShowSwordWeapon; // �ý���״̬
        public static Event PlayerHideSwordWeapon; // ���ؽ���״̬

        public static Event PlayerReloaded; // �������

        public static Event PlayerAimActive; // ��׼����
        public static Event PlayerAimOut; // ��׼����

        

        public static Event<int, Action> PlayerAddBulletAmount; // �����ӵ�
        // ��ɫ������ص�
        public static Event<int, int> PlayerReturnHPAndMP; // ��ɫ��Ѫ�ͻ���
        public static Event PlayerChangeCurrentHP; // �ı䵱ǰѪ��
        public static Event PlayerChangeMAXHP; // ��ɫ�ı����Ѫ��
        public static Event PlayerChangeCurrentMP; // �ı䵱ǰ����
        public static Event PlayerChangeMAXMP; // ��ɫ�ı��������

        public static Event PlayerOpenBag; // ��ɫ�򿪱���
        public static Event PlayerCloseBag; // ��ɫ�رձ���
        public static Event PlayerDied; // ��ɫ����

        public static Event<int> PlayerAddExp; // ��ɫ��þ���
        public static Event PlayerChangeEXP; // ��ɫ�ı䵱ǰ�ľ�����
        public static Event PlayerUpgradeRequest; // ��ɫ��������
        public static Event PlayerGradeChange; // ��ɫ�ȼ��ı�

    }

}