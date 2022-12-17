using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class GameData 
{
    public static string highScore = "highScore";

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(highScore, 0);
    }

    public static void SetHighScore(int value)
    {
         PlayerPrefs.SetInt(highScore, value);
    }
}
