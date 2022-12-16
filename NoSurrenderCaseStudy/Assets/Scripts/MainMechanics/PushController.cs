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
    public int maxScale;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            var pushedStrength = other.gameObject.GetComponent<PushController>();
            transform.DOMove(transform.position -directionTransform.forward * pushedStrength.push, .5f);
        }

        if (other.gameObject.CompareTag("Item"))
        {
            var transformUp = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            var particlePool = GameManager.Instance.particlePool;
         FxStateController particle =   particlePool.GetPooledObject(1);
         particle.transform.position = transformUp;
            push += .3f;

            if (transform.localScale.y <= maxScale)
            {
                transform.DOScale(transform.localScale * 1.08f, .5f);
            }
           
        }
    }
}