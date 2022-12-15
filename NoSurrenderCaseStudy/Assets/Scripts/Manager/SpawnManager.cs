using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<Transform> spawnPosList;
    public float updateInterval;
    private double lastInterval;
    private int frames;
    private float fps;
    
    
    public void Start()
    {


        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

    }

    private void Update()
    {
        SpawnCheckRoutine();
    }

    public void SpawnCheckRoutine()
    {
        var pool = GameManager.Instance.itemPool;
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;

            for (int i = 0; i < spawnPosList.Count; i++)
            {
                if (spawnPosList[i].transform.childCount == 0)
                {
                    ItemController item = pool.GetPooledObject(0);
                    item.transform.position = spawnPosList[i].position;
                    item.transform.SetParent(spawnPosList[i]);

                }
            }
        }
    }
}
