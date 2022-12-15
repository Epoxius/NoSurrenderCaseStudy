using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PushController : MonoBehaviour
{
    public float push;
    public Transform directionTransform;
    public BoxCollider selfCollider;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            if (selfCollider != null)
            {
                var pushedStrength = other.gameObject.GetComponent<PushController>();
                transform.DOLocalMove(directionTransform.position + (-directionTransform.forward * pushedStrength.push), .5f);
            }
            
        }

        if (other.gameObject.CompareTag("Item"))
        {
            push += .3f;
            transform.DOScale(transform.localScale * 1.06f, .5f);
            
        }
        
    }
}