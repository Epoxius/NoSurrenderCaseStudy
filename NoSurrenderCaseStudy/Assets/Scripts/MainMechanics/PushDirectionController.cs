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
      
       
        
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Calculate the rotation required to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Extract the y-axis rotation from the look rotation
            float yRotation = lookRotation.eulerAngles.y;

            // Create a new Quaternion with the y-axis rotation
            Quaternion targetRotation = Quaternion.Euler(0, yRotation, 0);

            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);

        }
        
    }
}
