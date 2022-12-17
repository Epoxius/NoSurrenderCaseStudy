using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI selfText;
    public TextMeshProUGUI scoreText;
    private Tween t;
    public int score;
    public int scorePoint;
    public int killScorePoint;
    public int highScore;

     [Header("End Result")] 
    public TextMeshProUGUI endGameResultLineText;
    public TextMeshProUGUI endGameScoreText;
    public TextMeshProUGUI highScoreText;

    private void Update()
    {
        selfText.transform.LookAt(selfText.transform.position + Camera.main.transform.forward);
    }

    //  Text scale anim when item taken or enemy killed.
    public void TextAnim()
    {
        t.Kill();
        var bigScale = new Vector3(0.03f, 0.03f, 0.03f);
        var smallScale = Vector3.zero;
      t =   selfText.transform.DOScale(bigScale, .5f).OnComplete(() =>
        {
            selfText.transform.DOScale(smallScale, .5f);
        });
    }
    
   
}
