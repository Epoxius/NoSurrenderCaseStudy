using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; set; }
   public float gameTime;
   public GameObject joystickObject;
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
   public PlayerAnimationController playerAnimationController;
   public SpawnManager spawnManager;
   public UIManager uiManager;

   public void Awake()
   {
      Instance = this;
   }

   public void GameStart()
   {
      uiManager.pauseBtn.SetActive(true);
      uiManager.countDownText.text ="Time : " + Convert.ToInt32(gameTime);
      uiManager.startBtn.SetActive(false);
      uiManager.countDownText.gameObject.SetActive(true);
      spawnManager.SpawnItemAtStart();
      playerAnimationController.CheckPlayerMovement();
      isStart = true;
      
   }

   public void SceneRestart()
   {
      SceneManager.LoadScene(0);
   }

   public void GamePause()
   {
      uiManager.pausedPanel.SetActive(true);
      Time.timeScale = 0;
   }

   public void GameResume()
   {
      uiManager.pausedPanel.SetActive(false);
      Time.timeScale = 1;
   }

   private void Update()
   {
      if (isStart && uiManager.countDownText != null)
      {
         CountDownAndFinishControl();
      }
   }

   public void CountDownAndFinishControl()
   {
      if (gameTime >0 && isStart )
      {
         gameTime -= 1 * Time.deltaTime;
         uiManager.countDownText.text ="Time : " + Convert.ToInt32(gameTime);
      }

      if (gameTime <= 0 )
      {
       
         isStart = false;
         isFinish = true;
         uiManager.nextLevelBtn.SetActive(true);
      }

      if (gamePlayerList.Count == 1)
      {
         uiManager.nextLevelBtn.SetActive(true);
         isStart = false;
         isFinish = true;
      }
   }
   
}
