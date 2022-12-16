using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<ItemController> pooledObjects;
        public ItemController objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;
    
    
    

    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<ItemController>();

            for (int i = 0; i <  pools[j].poolSize; i++)
            {
                ItemController obj = Instantiate( pools[j].objectPrefab, transform, true);
                obj.gameObject.SetActive(false);

                pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }

    public ItemController GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            Debug.Log("LAN");
            return null;
            
        }
        
        ItemController obj =  pools[objectType].pooledObjects.Dequeue();
        
        obj.gameObject.SetActive(true);
        pools[objectType].pooledObjects.Enqueue(obj);

        return obj;
    }

    public ItemController SetPooledObject(ItemController itemObj)
    {
        pools[0].pooledObjects.Enqueue(itemObj);
        itemObj.transform.SetParent(transform);
        itemObj.gameObject.SetActive(false);

        return itemObj;
    }
}
