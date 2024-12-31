using LockStep;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerB2 : MonoBehaviour
{
    private int userId = -1;
    private void Awake()
    {
        SubScribe();
    }

    private void SubScribe()
    {
        Events.UpdateMove += OnWalk; // µÇÂ½ÇëÇó
    }
    private void UnSubScribe()
    {
        Events.UpdateMove -= OnWalk;
    }

    public void SetId(int id)
    {
        if (id == UdpClient.Instance.UserId)
        {
            GetComponent<Image>().color = Color.red;
        }
        userId = id;
    }

    private void Update()
    {
        //UpdateWalk();
    }

    private void OnDestroy()
    {
        UnSubScribe();
    }
}
