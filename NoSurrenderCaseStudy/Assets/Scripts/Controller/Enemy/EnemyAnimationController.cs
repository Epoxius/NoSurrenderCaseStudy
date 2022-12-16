using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {
        CheckEnemyMovement();
    }

    public void CheckEnemyMovement()
    {
        if (GameManager.Instance.isStart &&!GameManager.Instance.isFinish)
        {
            anim.SetBool("isRunning", true);
            
        }
        else
        {
            anim.SetBool("isRunning", false);
          
        }

        if (GameManager.Instance.isFinish)
        {
            anim.SetTrigger("Win");
        }
        
    }
}
