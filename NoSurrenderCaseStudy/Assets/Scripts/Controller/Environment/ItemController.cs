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
    public ItemParticleController selfParticleController;

    private void Start()
    {
       
       selfParticleController.SpawnFx();
    }
    

    void Update()
    {
       
        ItemRotate();
    }


 
    public void ItemRotate()
    {
        transform.Rotate(Vector3.up * (Time.deltaTime * itemRotateSpeed));
    }

    // When triggered Player SetPool this item.
    public void OnTriggerEnter(Collider other)
    {
        
        var itemPool = GameManager.Instance.itemPool;
        if (other.gameObject.CompareTag("Push"))
        {
            GameManager.Instance.aiTargetList.Remove(transform);

            itemPool.SetPooledObject(this);
            
        }
    }
}