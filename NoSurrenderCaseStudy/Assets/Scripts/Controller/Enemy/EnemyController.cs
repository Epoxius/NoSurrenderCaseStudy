using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform closestTarget;
    public bool isGrounded;
    public int enSpeed;
    public Rigidbody enRb;
    private void Start()
    {
       // GameManager.Instance.aiTargetList.Add(transform);
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

        if (isGrounded)
        {
            // Calculate the direction to the target
            Vector3 direction = (closestTarget.position - transform.position).normalized;

            // Calculate the rotation required to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Extract the y-axis rotation from the look rotation
            float yRotation = lookRotation.eulerAngles.y;

            // Create a new Quaternion with the y-axis rotation
            Quaternion targetRotation = Quaternion.Euler(0, yRotation, 0);

            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    
  
            transform.position = Vector3.MoveTowards(transform.position, closestTarget.transform.position, enSpeed * Time.deltaTime);
            
        }
        
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            enRb.constraints = RigidbodyConstraints.FreezePositionY;
            enRb.useGravity = false;
        }
      
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            enRb.useGravity = true;
            enRb.constraints = RigidbodyConstraints.None;
            enRb.AddForce(-transform.forward* 5);
        }
    }
}