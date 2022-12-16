using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; set; }

   [Header("Booleans")] 
   public bool isStart;
   public bool isFail;
   public bool isFinish;
   
   [Space]
   [Header("Lists")] 
   public List<Transform> aiTargetList;
   public List<EnemyController> enemyList;
   public List<Transform> gamePlayerList;


   [Space] 
   
   [Header("Pool")]
   public ItemPool itemPool;
   public EnemyPool enemyPool;
   public ParticlePool particlePool;

   [Header("Scripts")] 
   public PlayerMoveController playerMoveController;
   public SpawnManager spawnManager;

   public void Awake()
   {
      Instance = this;
   }

   public void GameStart()
   {
      spawnManager.SpawnItemAtStart();
      isStart = true;
   }
   
}
