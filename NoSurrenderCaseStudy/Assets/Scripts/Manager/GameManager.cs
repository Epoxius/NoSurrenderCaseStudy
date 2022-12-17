using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
   public ScoreManager scoreManager;

   public void Awake()
   {
      Instance = this;
      scoreManager.highScore = GameData.GetHighScore();
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

   public void GameEnd()
   {
      scoreManager.endGameScoreText.gameObject.SetActive(true);
      scoreManager.endGameResultLineText.gameObject.SetActive(true);
      scoreManager.highScoreText.gameObject.SetActive(true);
      scoreManager.endGameScoreText.text = "Score : " + scoreManager.score;
      scoreManager.endGameResultLineText.text = " You are :#" + gamePlayerList.Count;
      GameData.SetHighScore(scoreManager.score);
      if (scoreManager.score < scoreManager.highScore)
      {
         scoreManager.highScoreText.text = "High Score : " + scoreManager.highScore;
      }
      else
      {
         scoreManager.highScoreText.text = "High Score : " + scoreManager.score;
      }
      
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
         GameEnd();
       
         isStart = false;
         isFinish = true;
         uiManager.nextLevelBtn.SetActive(true);
         
      }

      if (gamePlayerList.Count == 1)
      {
         GameEnd();
         uiManager.nextLevelBtn.SetActive(true);
        
         isStart = false;
         isFinish = true;
      }

      if (playerMoveController == null)
      {
         
         isStart = false;
         isFinish = true;
         GameEnd();
         uiManager.restartBtn.SetActive(true);
      }
   }
   
}
