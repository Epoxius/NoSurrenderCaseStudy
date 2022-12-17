using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float speed;
    public bool isGrounded;
    public DynamicJoystick dynamicJoystick;
    public Rigidbody rb;

    private void Start()
    {
      GameManager.Instance.gamePlayerList.Add(transform);
      GameManager.Instance.aiTargetList.Add(transform);
    }

    public void FixedUpdate()
    {
        if (GameManager.Instance.isStart && !GameManager.Instance.isFinish)
        {
            if (isGrounded)
            {
                PlayerMove();
            }
           
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
        
    }

    // Player move with Rigidbody
    public void PlayerMove()
    {
        Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;

        rb.velocity = transform.forward * (speed * Time.fixedDeltaTime);
        
        if (direction != Vector3.zero)
        {
            var lookPos = Camera.main.transform.TransformDirection(direction);
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }
    
    // Check player is On Ground and DeadZone and SetPool
    public void OnTriggerEnter(Collider other)
    {
        var scoreManager = GameManager.Instance.scoreManager;
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.useGravity = true;
           
        }

        if (other.gameObject.CompareTag("DeadZone"))
        {
           
            var particlePool = GameManager.Instance.particlePool;
            FxStateController deathFx = particlePool.GetPooledObject(0);
            deathFx.transform.position = transform.position;
            Destroy(gameObject);
           
        }

        if (other.gameObject.CompareTag("Item"))
        {
            scoreManager.TextAnim();
            scoreManager.score += scoreManager.scorePoint;
            scoreManager.selfText.text = "+" + scoreManager.scorePoint;
            scoreManager.scoreText.text = "Score : " + scoreManager.score;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(Vector3.down*6);
           

            
        }
    }
    
}