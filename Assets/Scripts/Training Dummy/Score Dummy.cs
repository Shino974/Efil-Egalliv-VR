using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using VHierarchy.Libs;

public class ScoreDummy : MonoBehaviour
{
    /*---------SCORE--------*/
    private int maxScore = 30;
    private int scoreValue = 0;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    
    /*---------TIME--------*/
    public TimeSpan elapseTime = new TimeSpan(0, 0, 60);    
    public TimeSpan maxTime = new TimeSpan(0, 0, 60);
    public TMP_Text timerText;
    public TMP_Text highScoreTime;
    private bool isTime = false;
    
    /*---------SCRIPTS--------*/
    private DummyTrigger dummyTriggerScript;
    
    /*---------STATE--------*/
    public TMP_Text winText;
    public TMP_Text loseText;
    
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        winText.enabled = false;
        loseText.enabled = false;
        dummyTriggerScript = FindObjectOfType<DummyTrigger>();
        scoreText.text = "Score: " + scoreValue;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreDummy", 0);
        timerText.text = "Time Left: " + elapseTime.ToString();
        highScoreTime.text = "Best Time: " + PlayerPrefs.GetString("HighScoreTimeDummy", ("00:00:00"));
    }

    private void Update()
    {
        /*---------IF PLAYER START THE MINIGAME--------*/
        if (isTime)
        {
            /*---------IF TIMER IS NOT 0s--------*/
            if (elapseTime.TotalSeconds > 0) 
            {
                /*---------TIMER--------*/
                elapseTime = elapseTime.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
                timerText.text = "Time Left: " + elapseTime.ToString("mm':'ss':'ff");
                /*---------IF ALL DUMMIES ARE DEAD--------*/
                if (dummyTriggerScript.dummyCount == 0)
                {
                    isTime = false;
                    /*---------TIME--------*/
                    elapseTime = maxTime - elapseTime;
                    if (elapseTime < TimeSpan.Parse(PlayerPrefs.GetString("HighScoreTimeDummy", maxTime.ToString())))
                    {
                        PlayerPrefs.SetString("HighScoreTimeDummy", elapseTime.ToString("mm':'ss':'ff"));
                        highScoreTime.text = "Best Time: " + PlayerPrefs.GetString("HighScoreTimeDummy", "00:00:00");
                    }
                    /*---------SCORE--------*/
                    if (scoreValue > PlayerPrefs.GetInt("HighScoreDummy", 0))
                    {
                        PlayerPrefs.SetInt("HighScoreDummy", scoreValue);
                        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreDummy", 0);
                    }
                    /*---------ENABLE_SCRIPT--------*/
                    winText.enabled = true;
                    enabled = false;
                } 
            }
            /*---------IF TIMER REACH 0s--------*/
            else
            {
                /*---------TIME--------*/
                elapseTime = maxTime - elapseTime;
                if (elapseTime < TimeSpan.Parse(PlayerPrefs.GetString("HighScoreTimeDummy", maxTime.ToString())))
                {
                    PlayerPrefs.SetString("HighScoreTimeDummy", elapseTime.ToString("mm':'ss':'ff"));
                    highScoreTime.text = "Best Time: " + PlayerPrefs.GetString("HighScoreTimeDummy", "00:00:00");
                }
                /*---------SCORE--------*/
                if (scoreValue > PlayerPrefs.GetInt("HighScoreDummy", 0))
                {
                    PlayerPrefs.SetInt("HighScoreDummy", scoreValue);
                    highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreDummy", 0);
                }
                loseText.enabled = true;
                enabled = false;
            }

        }
    }
    
    public void AddScore()
    {
        scoreValue += 1;
        scoreText.text = "Score: " + scoreValue;
    }
    
    public void StartDummyGame()
    {
        isTime = true;
    }
    
    public void ResetScore()
    {
        scoreValue = 0;
        isTime = false;
        scoreText.text = "Score: " + scoreValue;
        timerText.text = "Time: " + elapseTime.ToString("mm':'ss':'ff");
    }

    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreDummy", 0);
        highScoreTime.text = "Best Time: " + PlayerPrefs.GetString("HighScoreTimeDummy", "00:00:00");
    }
}
