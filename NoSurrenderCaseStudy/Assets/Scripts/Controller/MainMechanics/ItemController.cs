using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemController : MonoBehaviour
{

     public int itemRotateSpeed;

     private void Start()
     {
         GameManager.Instance.aiTargetList.Add(transform);
         transform.DOScale(Vector3.one, 1);
     }

     void Update()
    {
       ItemRotate();
    }

    public void ItemRotate()
    {
        transform.Rotate(Vector3.up * (Time.deltaTime * itemRotateSpeed));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            GameManager.Instance.aiTargetList.Remove(transform);
        }
    }
}
