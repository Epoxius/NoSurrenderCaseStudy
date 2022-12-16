using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDirectionController : MonoBehaviour
{

    private void Update()
    {
        LookClosestEnemy();
    }

    public void LookClosestEnemy()
    {
        var player = GameManager.Instance.playerMoveController;
        transform.LookAt(player.transform.position);
    }
}
