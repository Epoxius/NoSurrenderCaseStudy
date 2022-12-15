using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float speed;
    public DynamicJoystick dynamicJoystick;
    public Rigidbody rb;

    private void Start()
    {
        GameManager.Instance.aiTargetList.Add(transform);
    }

    public void FixedUpdate()
    {
        PlayerMove();
    }

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
    
}