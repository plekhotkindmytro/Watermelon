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

    private float minSpawnX;
    private float maxSpawnX;

    private int bestScore;
    private static readonly string BEST_SCORE_KEY = "bestScore";

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

    internal float ClampSpawnX(float x)
    {

        return Mathf.Clamp(x, minSpawnX, maxSpawnX); ;
    }



    public void CheckGameOver()
    {
        //if (/* Add game over condition here, e.g., fruits reach the top of the screen */)
        //{
        //    gameOverPanel.SetActive(true);
        //    Time.timeScale = 0;  // Pause the game
        //}
    }

    public void ResetGame()
    {
        score = 0;
        scoreText.text = "Score: 0";
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    


}
