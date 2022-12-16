using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<FxStateController> pooledObjects;
        public FxStateController objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;
    
    
    

    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<FxStateController>();

            for (int i = 0; i <  pools[j].poolSize; i++)
            {
                FxStateController obj = Instantiate( pools[j].objectPrefab, transform, true);
                obj.gameObject.SetActive(false);

                pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }

    public FxStateController GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            Debug.Log("LAN");
            return null;
            
        }
        
        FxStateController obj =  pools[objectType].pooledObjects.Dequeue();
        
        obj.gameObject.SetActive(true);
        pools[objectType].pooledObjects.Enqueue(obj);

        return obj;
    }

    public FxStateController SetPooledObject(FxStateController itemObj)
    {
        pools[0].pooledObjects.Enqueue(itemObj);
        itemObj.transform.SetParent(transform);
        itemObj.gameObject.SetActive(false);

        return itemObj;
    }
}
