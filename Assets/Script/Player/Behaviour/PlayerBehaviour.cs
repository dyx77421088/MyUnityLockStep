using System;
using UnityEngine;

namespace LockStep
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent (typeof(CharacterController))]
    public partial class PlayerBehaviour : MonoBehaviour
    {
        public PlayerMoveSettings moveSettings; // 
        [HideInInspector] public int userId; // 保存用户登陆之后的id
        // 动画控制器
        private Animator _animator;
        // 单例
        private static PlayerBehaviour instance; 
        // 角色控制器
        private CharacterController _characterController;
        private bool isPause;
        private bool isAlive = true; // 存活
        public static PlayerBehaviour Instance { get { return instance; } }
        public bool IsAlive { get { return isAlive; } }
        private void Awake()
        {
            instance = this;
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            // 注册一些订阅
            SubScribe();
        }

        private void OnDestroy()
        {
            // 注销订阅
            UnSubScribe();
        }



        private void Update()
        {
            if (isPause) return;
            // 检测是否着地
            UpdateGround();
            // 行走
            UpdateWalk();
            // 跑
            UpdateRun();
            // 根据角色当前状态改变运动速度
            UpdateMovementSpeed();
            // 子弹应该击中的点
            //UpdateFirePoint();
            // 重力
            UpdateGravity();
        }

        // 在update之后执行，依赖于update更新后的数据
        private void LateUpdate()
        {
            if (isPause) return;
            // 更新脊椎的旋转
            //UpdateSpineRotate();
        }
        /// <summary>
        /// 一些订阅
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
        /// 注销一些订阅
        /// </summary>
        private void UnSubScribe()
        {
            Events.JumpRequest -= OnJumpRequested;

            Events.PlayerOpenBag -= OnPause;
            Events.PlayerCloseBag -= OnResume;
            // ui相关的
            Events.PlayerReturnHPAndMP -= OnAddHPAndMP;
            Events.PlayerAddBulletAmount -= OnAddBulletAmount;

            Events.LoginSuccess -= OnLoginSuccess;
        }

        

        private void OnAnimatorIK(int layerIndex)
        {
            // 左手的ik
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
            //    Events.PlayerInfoTipShow.Call("请先装备枪", UI.PlayerInfoTipUI.PlayerInfoTipPoint.Center);
            //    return;
            //}
            //// 成功加子弹
            //complete?.Invoke();
            //CurrentGun.bulletsAmount += count;
        }
    }

}