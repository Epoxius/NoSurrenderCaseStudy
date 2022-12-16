using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<EnemyController> pooledObjects;
        public EnemyController objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;
    
    
    
    

    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<EnemyController>();

            for (int i = 0; i <  pools[j].poolSize; i++)
            {
                EnemyController obj = Instantiate( pools[j].objectPrefab, transform, true);
                obj.gameObject.SetActive(false);

                pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }

    public EnemyController GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        
        EnemyController obj =  pools[objectType].pooledObjects.Dequeue();
        
        obj.gameObject.SetActive(true);
        
        pools[objectType].pooledObjects.Enqueue(obj);

        return obj;
    }

    public EnemyController SetPooledObject(EnemyController obj)
    {
        pools[0].pooledObjects.Enqueue(obj);
        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);

        return obj;
    }
}
