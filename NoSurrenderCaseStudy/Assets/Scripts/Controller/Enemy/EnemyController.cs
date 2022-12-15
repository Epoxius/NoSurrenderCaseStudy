using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent ai;

    public Transform closestTarget;

    private void Start()
    {
        GameManager.Instance.aiTargetList.Add(transform);
        GameManager.Instance.enemyList.Add(this);
    }

    public void Update()
    {
        RangeCalculate();
        AIMove();
    }

    public void RangeCalculate()
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in GameManager.Instance.aiTargetList)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                if (dSqrToTarget > .5f)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestTarget = potentialTarget;
                }
            }
        }
    }


    public void AIMove()
    {
        if (closestTarget == null)
        {
            Debug.Log("There Is No Enemy");
            return;
        }
        
        ai.SetDestination(closestTarget.position);
    }
}