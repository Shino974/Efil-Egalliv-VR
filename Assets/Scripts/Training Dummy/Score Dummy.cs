using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDummy : MonoBehaviour
{
    /*---------SCORE--------*/
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    private int maxScore = 15;
    private int scoreValue = 0;

    /*---------TIME--------*/
    public TMP_Text timerText;
    public TMP_Text highScoreTimeText;
    private float elapsedTime = 60f;
    private float maxTime = 60f;
    private float highScoreTime;
    private bool isTime = false;

    /*---------SCRIPTS--------*/
    private DummyTrigger dummyTriggerScript;

    /*---------STATE--------*/
    public TMP_Text winText;
    public TMP_Text loseText;

    private void Awake()
    {
        winText.enabled = false;
        loseText.enabled = false;
        dummyTriggerScript = FindObjectOfType<DummyTrigger>();
        if (dummyTriggerScript == null)
        {
            Debug.LogError("DummyTrigger component not found in the scene. Please ensure it is present.");
            return;
        }
        scoreText.text = "Score: " + scoreValue;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreDummy", 0);
        highScoreTime = PlayerPrefs.GetFloat("HighScoreTimeDummy", maxTime);
        highScoreTimeText.text = "Best Time: " + highScoreTime;
        timerText.text = "Time Left: " + elapsedTime;
    }

    private void Update()
    {
        /*---------IF PLAYER START THE MINIGAME--------*/
        if (isTime)
        {
            /*---------IF TIMER IS NOT 0s--------*/
            if (elapsedTime > 0)
            {
                /*---------TIMER--------*/
                elapsedTime -= Time.deltaTime;
                timerText.text = "Time Left: " + elapsedTime.ToString("0.00");
                /*---------IF ALL DUMMIES ARE DEAD--------*/
                if (dummyTriggerScript.dummyCount == 0)
                {
                    isTime = false;
                    /*---------TIME--------*/
                    elapsedTime = maxTime - elapsedTime;
                    if (elapsedTime < highScoreTime)
                    {
                        PlayerPrefs.SetFloat("HighScoreTimeDummy", elapsedTime);
                        highScoreTimeText.text = "Best Time: " + PlayerPrefs.GetFloat("HighScoreTimeDummy", maxTime).ToString();
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
                elapsedTime = maxTime - elapsedTime;
                if (elapsedTime < highScoreTime)
                {
                    PlayerPrefs.SetFloat("HighScoreTimeDummy", elapsedTime);
                    highScoreTimeText.text = "Best Time: " + PlayerPrefs.GetFloat("HighScoreTimeDummy", maxTime).ToString();
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
        timerText.text = "Time: " + elapsedTime.ToString("0.00");
    }

    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreDummy", 0);
        highScoreTimeText.text = "Best Time: " + PlayerPrefs.GetFloat("HighScoreTimeDummy", maxTime).ToString();
    }
}