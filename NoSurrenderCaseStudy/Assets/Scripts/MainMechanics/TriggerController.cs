using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public float push;
    public int maxScale;
    public ParticleSystem backHitFx;
    public Transform objectSpawnPos;
    public Transform directionTransform;

    private Tween t;

    private bool isPushedBack;

    // When triggered, I subtract the transform forward in the PushDirectionController script from my own position and multiply it by the thrust of the object we are triggered.
    public void OnTriggerEnter(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        if (other.gameObject.CompareTag("Push"))
        {
            var pushedStrength = other.gameObject.GetComponent<TriggerController>().push;
            if (Vector3.Dot(transform.forward, direction) < 0)
            {
                isPushedBack = true;
                if (isPushedBack)
                {
                    print("Back");
                    t = transform.DOMove(transform.position + (-directionTransform.forward * pushedStrength*2), .5f);
                    backHitFx.Play();
                }
            }
            else
            {
                isPushedBack = false;
                t = transform.DOMove(transform.position + (-directionTransform.forward * pushedStrength), .5f);
            }

           
           
        }

        //I increased the scale growth and push power when the item is taken. I call the effect of getting the item from the particle pool.
        if (other.gameObject.CompareTag("Item"))
        {
            var transformUp = objectSpawnPos.position;
            var particlePool = GameManager.Instance.particlePool;
            FxStateController particle = particlePool.GetPooledObject(1);
            particle.transform.position = transformUp;
            push += .4f;

            if (transform.localScale.y <= maxScale)
            {
                transform.DOScale(transform.localScale * 1.08f, .5f);
            }
        }
    }
}