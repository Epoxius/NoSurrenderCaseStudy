using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; set; }

   [Header("Lists")] 
   public List<Transform> aiTargetList;

   [Space] 
   
   [Header("Pool")]
   public ItemPool itemPool;

   public void Awake()
   {
      Instance = this;
   }
   
}
