using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PushController : MonoBehaviour
{
    public float push;
    public Transform directionTransform;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            transform.DOLocalMove(directionTransform.position + (-directionTransform.forward * push), .5f);
        }

        if (other.gameObject.CompareTag("Item"))
        {
            push -= .3f;
            transform.DOScale(transform.localScale * 1.04f, .5f);
            Destroy(other.gameObject);
        }
        
    }
}