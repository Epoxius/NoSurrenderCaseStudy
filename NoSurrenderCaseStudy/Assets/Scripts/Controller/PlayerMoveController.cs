using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float speed;
    public DynamicJoystick dynamicJoystick;
    public Rigidbody rb;

    public void Update()
    {
        Move();
    }

    public void Move()
    {
      //  Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
       rb.velocity = transform.forward * Time.deltaTime;
    }
}
