    using System;
    using System.Collections;
using System.Collections.Generic;
    using DG.Tweening;
    using UnityEngine;

public class PushController : MonoBehaviour
{
    public int pushStrength;
    public void OnTriggerEnter(Collider other)
    {
        transform.DOLocalMove(transform.position+ (-transform.forward* pushStrength), 1);

        

    }

   
    
}
