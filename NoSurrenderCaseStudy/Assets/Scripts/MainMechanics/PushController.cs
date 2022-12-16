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
    public Transform objectSpawnPos;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            var pushedStrength = other.gameObject.GetComponent<PushController>().push;
            transform.DOMove(transform.position +(-directionTransform.forward * pushedStrength), .5f);
           
            
        }

        if (other.gameObject.CompareTag("Item"))
        {
            var transformUp = objectSpawnPos.position;
            var particlePool = GameManager.Instance.particlePool;
         FxStateController particle =   particlePool.GetPooledObject(1);
         particle.transform.position = transformUp;
            push += .4f;

            if (transform.localScale.y <= maxScale)
            {
                transform.DOScale(transform.localScale * 1.08f, .5f);
            }
           
        }
    }
}