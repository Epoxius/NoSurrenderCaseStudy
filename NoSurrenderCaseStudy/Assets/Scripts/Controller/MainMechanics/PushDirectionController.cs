using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDirectionController : MonoBehaviour
{
    public EnemyController selfEnemyController;

    private void Update()
    {
        LookClosestEnemy();
    }

    public void LookClosestEnemy()
    {
        transform.LookAt(selfEnemyController.closestTarget);
    }
}
