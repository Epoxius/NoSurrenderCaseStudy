using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDirectionController : MonoBehaviour
{
    public EnemyController closestTarget;
    public EnemyController selfEnemyController;
    public PlayerType playerType;

    
    // Checking player enum for direction look.
    private void Update()
    {
        RangeCalculate();
        CheckPlayerEnum();
       
    }

    public void CheckPlayerEnum()
    {
        if (playerType == PlayerType.enemy)
        {
            EnemyLook();
        } 
        if (playerType == PlayerType.player)
        {
            PlayerLook();
        }
    }

    public enum PlayerType
    {
        player,
        enemy,
    }

    public void RangeCalculate()
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (EnemyController potentialTarget in GameManager.Instance.enemyList)
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

    public void EnemyLook()
    {
        var player = GameManager.Instance.playerMoveController;


        if (selfEnemyController.closestTarget != null)
        {
            Vector3 direction = (selfEnemyController.closestTarget.transform.position - transform.position).normalized;

            // Calculate the rotation required to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Extract the y-axis rotation from the look rotation
            float yRotation = lookRotation.eulerAngles.y;

            // Create a new Quaternion with the y-axis rotation
            Quaternion targetRotation = Quaternion.Euler(0, yRotation, 0);

            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
        }
    }

    public void PlayerLook()
    {
        if (closestTarget !=null)
        {
            Vector3 direction = (closestTarget.transform.position - transform.position).normalized;

            // Calculate the rotation required to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Extract the y-axis rotation from the look rotation
            float yRotation = lookRotation.eulerAngles.y;

            // Create a new Quaternion with the y-axis rotation
            Quaternion targetRotation = Quaternion.Euler(0, yRotation, 0);

            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime); 
        }
       
    }
}