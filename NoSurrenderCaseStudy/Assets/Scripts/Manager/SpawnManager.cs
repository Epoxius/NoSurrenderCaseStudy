using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> itemSpawnPosList;
    public List<Transform> enemySpawnPosList;
    public float itemSpawnRoutine;
    private double lastInterval;
    private int frames;
    private float fps;
    
    
    public void Start()
    {

        SpawnEnemy();
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

    }

    private void Update()
    {
        ItemSpawnCheckRoutine();
    }

    public void ItemSpawnCheckRoutine()
    {
        var itemPool = GameManager.Instance.itemPool;
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + itemSpawnRoutine)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;

            for (int i = 0; i < itemSpawnPosList.Count; i++)
            {
                if (itemSpawnPosList[i].transform.childCount == 0)
                {
                    ItemController item = itemPool.GetPooledObject(0);
                    item.transform.position = itemSpawnPosList[i].position;
                    item.transform.SetParent(itemSpawnPosList[i]);

                }
            }
        }
    }

    public void SpawnEnemy()
    {
        var enemyPool = GameManager.Instance.enemyPool;
        for (int i = 0; i < enemySpawnPosList.Count; i++)
        {
            EnemyController enemy = enemyPool.GetPooledObject(0);
            enemy.transform.position = enemySpawnPosList[i].position;
            enemy.transform.SetParent(enemySpawnPosList[i]);
        }
    }
}
