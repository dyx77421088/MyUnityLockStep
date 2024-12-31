using System;
using UnityEngine;

namespace LockStep
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent (typeof(CharacterController))]
    public partial class PlayerBehaviour : MonoBehaviour
    {
        public PlayerMoveSettings moveSettings; // 
        [HideInInspector] public int userId; // �����û���½֮���id
        // ����������
        private Animator _animator;
        // ����
        private static PlayerBehaviour instance; 
        // ��ɫ������
        private CharacterController _characterController;
        private bool isPause;
        private bool isAlive = true; // ���
        public static PlayerBehaviour Instance { get { return instance; } }
        public bool IsAlive { get { return isAlive; } }
        private void Awake()
        {
            instance = this;
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            // ע��һЩ����
            SubScribe();
        }

        private void OnDestroy()
        {
            // ע������
            UnSubScribe();
        }



        private void Update()
        {
            if (isPause) return;
            // ����Ƿ��ŵ�
            UpdateGround();
            // ����
            UpdateWalk();
            // ��
            UpdateRun();
            // ���ݽ�ɫ��ǰ״̬�ı��˶��ٶ�
            UpdateMovementSpeed();
            // �ӵ�Ӧ�û��еĵ�
            //UpdateFirePoint();
            // ����
            UpdateGravity();
        }

        // ��update֮��ִ�У�������update���º������
        private void LateUpdate()
        {
            if (isPause) return;
            // ���¼�׵����ת
            //UpdateSpineRotate();
        }
        /// <summary>
        /// һЩ����
        /// </summary>
        private void SubScribe()
        {

            Events.JumpRequest += OnJumpRequested;

            Events.PlayerOpenBag += OnPause;
            Events.PlayerCloseBag += OnResume;

            Events.PlayerReturnHPAndMP += OnAddHPAndMP;
            Events.PlayerAddBulletAmount += OnAddBulletAmount;

            Events.LoginSuccess += OnLoginSuccess;
        }

        /// <summary>
        /// ע��һЩ����
        /// </summary>
        private void UnSubScribe()
        {
            Events.JumpRequest -= OnJumpRequested;

            Events.PlayerOpenBag -= OnPause;
            Events.PlayerCloseBag -= OnResume;
            // ui��ص�
            Events.PlayerReturnHPAndMP -= OnAddHPAndMP;
            Events.PlayerAddBulletAmount -= OnAddBulletAmount;

            Events.LoginSuccess -= OnLoginSuccess;
        }

        

        private void OnAnimatorIK(int layerIndex)
        {
            // ���ֵ�ik
            //UpdateLeftHandIk();
        }

        private void OnLoginSuccess(int userId)
        {
            this.userId = userId;
        }

        private void OnPause()
        {
            isPause = true;
            _animator.enabled = false;
        }
        private void OnResume()
        {
            isPause = false;
            _animator.enabled = true;
        }

        private void OnAddHPAndMP(int hp, int mp)
        {
            //AddHP(hp);
            //AddMP(mp);
        }

        private void OnAddBulletAmount(int count, Action complete)
        {
            //if (!IsGunWeapon)
            //{
            //    Events.PlayerInfoTipShow.Call("����װ��ǹ", UI.PlayerInfoTipUI.PlayerInfoTipPoint.Center);
            //    return;
            //}
            //// �ɹ����ӵ�
            //complete?.Invoke();
            //CurrentGun.bulletsAmount += count;
        }
    }

}