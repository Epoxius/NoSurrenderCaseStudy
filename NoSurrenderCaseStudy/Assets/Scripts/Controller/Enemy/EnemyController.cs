using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    
  
   
    public bool isGrounded;
    public int enSpeed;
    public int rotateSpeed;
    public Rigidbody enRb;
    public Transform closestTarget;
    public GameObject lastHitGameobject;

    
     // Adding enemy to Gamemanager lists.
    private void Start()
    {
        
        GameManager.Instance.enemyList.Add(this);
        GameManager.Instance.gamePlayerList.Add(transform);
    }

    // Check game state for movement;
    public void Update()
    {
        
        if (GameManager.Instance.isStart && !GameManager.Instance.isFinish)
        {
            if (isGrounded)
            {
                RangeCalculate();
                AIMove();
            }
            
        }

        if (GameManager.Instance.isFinish)
        {
            enSpeed = 0;
            enRb.isKinematic = true;
        }
    }


    // Calculate range With aiTargetList
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


    // Move and Rotate with MoveTowards
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        var target = new Vector3(closestTarget.position.x,-0.6f, closestTarget.position.z);

        transform.position =
            Vector3.MoveTowards(transform.position, target, enSpeed * Time.deltaTime);
        
    }


    // Check enemy is On Ground and DeadZone and SetPool
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            enRb.constraints = RigidbodyConstraints.FreezePositionY;
            enRb.useGravity = false;
        }

        if (other.gameObject.CompareTag("DeadZone"))
        {
            if (lastHitGameobject != null)
            {
                var scoreManager = GameManager.Instance.scoreManager;
                scoreManager.TextAnim();
                scoreManager.score += scoreManager.killScorePoint;
                scoreManager.selfText.text = "+" + scoreManager.killScorePoint;
                scoreManager.scoreText.text = scoreManager.score.ToString();

            }
            GameManager.Instance.gamePlayerList.Remove(transform);
            var enemyPool = GameManager.Instance.enemyPool;
            var particlePool = GameManager.Instance.particlePool;
            FxStateController deathFx = particlePool.GetPooledObject(0);
            deathFx.transform.position = transform.position;
                enemyPool.SetPooledObject(this);
        }

        if (other.gameObject.name =="Player")
        {
            lastHitGameobject = other.gameObject;
        }
        else
        {
            lastHitGameobject = null;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = false;
            enRb.useGravity = true;
            enRb.constraints = RigidbodyConstraints.None;
            enRb.AddForce(-transform.forward * 20);
        }
    }
}