using DG.Tweening;
using LockStep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerB2 : MonoBehaviour
{

    private float _forward;
    private float _right;
    private Vector3 lastPos = Vector3.zero;
    private readonly float _movementSpeed = 10f;
    /// <summary>
    /// 移动
    /// </summary>
    public void OnWalk(Move move)
    {
        if (userId != move.UserId) return;
        //_forward = move.MoveX;
        //_right = move.MoveY;
        //// 每3帧更新一次
        //transform.position = lastPos + new Vector3(move.MoveX, move.MoveY) * _movementSpeed * 2;
        //lastPos = transform.position;
        if (UdpClient.Instance.IsReload)
        {
            transform.position += new Vector3(move.MoveX, move.MoveY) * _movementSpeed;
        }
        else
        {
            transform.DOMove(transform.position + new Vector3(move.MoveX, move.MoveY) * _movementSpeed, 0.04f);
        }
        //transform.position += new Vector3(InputController.HorizontalMove , InputController.VerticalMove);
    }

    public void UpdateWalk()
    {
        //transform.position += new Vector3(_forward, _right) * _movementSpeed;
    }

}
