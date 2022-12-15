using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public IEnumerator Start()
    {
        var pool = GameManager.Instance.itemPool;
        while (true)
        {
           
            if (transform.childCount == 0)
            {
                yield return new WaitForSeconds(3f);
                ItemController item = pool.GetPooledObject(0);
                item.transform.position = transform.position;
                item.transform.SetParent(transform);
            }
        }
    }
}
