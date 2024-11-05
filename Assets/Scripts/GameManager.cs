using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverScoreText;


    private static readonly string BEST_SCORE_KEY = "bestScore";


    private float minSpawnX;
    private float maxSpawnX;
    private int bestScore;
    public bool gameOver;
  


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        bestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY);
        if (scoreText != null)
        {
            scoreText.text = "0";
        }

        if(bestScoreText != null)
        {
            bestScoreText.text = bestScore.ToString();
        }
    }

    public void AddScore(int points)
    {
        
        score += points;

        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(BEST_SCORE_KEY, bestScore);
            bestScoreText.text = bestScore.ToString();
        }

        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned");
            return;

        }
        scoreText.text = score.ToString();
    }

    internal void SetMinSpawnX(float value)
    {
        minSpawnX = value;
    }

    internal void SetMaxSpawnX(float value)
    {
        maxSpawnX = value;
    }

    //internal float GetMinSpawnX(float value)
    //{
    //    return minSpawnX;
    //}

    //internal float GetMaxSpawnX(float value)
    //{
    //    return maxSpawnX;
    //}

    internal float ClampSpawnX(float x, float absoluteOffset)
    {
        return Mathf.Clamp(x, minSpawnX + absoluteOffset, maxSpawnX - absoluteOffset); ;
    }

   


    public void GameOver()
    {
        if(!gameOver)
        {
            gameOver = true;
            ScreenshotManager.Instance.CaptureBucketScreenshot();
            gameOverPanel.SetActive(true);
            gameOverScoreText.text = "Score: " + score;
        }
        
       // Time.timeScale = 0;  
    }

    public void ResetGame()
    {
        score = 0;
        scoreText.text = "0";
        gameOverPanel.SetActive(false);
        gameOver = false;
    }

    


}
