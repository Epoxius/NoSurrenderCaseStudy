using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ItemController : MonoBehaviour
{

     public int itemRotateSpeed;

     private IEnumerator Start()
     {
        var randomTime = Random.Range(0, 6);
         yield return new WaitForSeconds(randomTime);
         ItemScale();
     }

     void Update()
    {
       ItemRotate();
    }


     public void ItemScale()
     {
         transform.DOScale(Vector3.one, 1).OnComplete(() =>
         {
             GameManager.Instance.aiTargetList.Add(transform);
         });
     }

    public void ItemRotate()
    {
        transform.Rotate(Vector3.up * (Time.deltaTime * itemRotateSpeed));
    }

    public void OnTriggerEnter(Collider other)
    {
        var itemPool = GameManager.Instance.itemPool;
        if (other.gameObject.CompareTag("Push"))
        {
            GameManager.Instance.aiTargetList.Remove(transform);
            transform.localScale = Vector3.zero;
            itemPool.SetPooledObject(this);
            
        }
    }
}
