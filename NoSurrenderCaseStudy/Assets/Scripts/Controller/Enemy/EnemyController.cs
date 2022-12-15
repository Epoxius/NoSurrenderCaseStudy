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
   }

   public void Update()
   {
      GetClosestEnemy();
      AIMove();
   }

   public Vector3 GetClosestEnemy()
   {
      float closestDistanceSqr = Mathf.Infinity;
      Vector3 currentPosition = transform.position;
      foreach (Transform potentialTarget in GameManager.Instance.aiTargetList)
      {
         Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
         float dSqrToTarget = directionToTarget.sqrMagnitude;
         if (dSqrToTarget < closestDistanceSqr)
         {
            closestDistanceSqr = dSqrToTarget;
            if (dSqrToTarget < closestDistanceSqr)
            {
               closestDistanceSqr = dSqrToTarget;
               closestTarget = potentialTarget;
            }
         }
        
      }

      return closestTarget.transform.position;
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
