using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; set; }

   [Header("Lists")] 
   public List<Transform> aiTargetList;
   public List<EnemyController> enemyList;

  
   [Space] 
   
   [Header("Pool")]
   public ItemPool itemPool;
   public EnemyPool enemyPool;

   [Header("Scripts")] 
   public PlayerMoveController playerMoveController;

   public void Awake()
   {
      Instance = this;
   }
   
}
