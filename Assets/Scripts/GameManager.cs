using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverScoreText;
    public MaxTopBorderTrigger maxTopBorderTrigger;
    public float delayBeforeGameOverPanel = 1.5f;
    public string leaderboardId = "main";
    public Transform boxBottomCollider;


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

    public float GetBoxBottomY()
    {
        float bottomY = boxBottomCollider.position.y + boxBottomCollider.transform.lossyScale.y / 2;
        return bottomY;
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
        UiManager.Instance.leaderboard1Button.transform.DOComplete(); // Completes any active tweens on the score text
        UiManager.Instance.leaderboard1Button.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }

    internal void SetMinSpawnX(float value)
    {
        minSpawnX = value;
    }

    internal void SetMaxSpawnX(float value)
    {
        maxSpawnX = value;
    }

    internal float RandomSpawnX()
    {
        return UnityEngine.Random.Range(minSpawnX, maxSpawnX);
    }

    internal void Warn()
    {
        maxTopBorderTrigger.Warn();
    }

    internal void CancelWarn()
    {
        maxTopBorderTrigger.CancelWarn();
    }



    internal float ClampSpawnX(float x, float absoluteOffset)
    {
        return Mathf.Clamp(x, minSpawnX + absoluteOffset, maxSpawnX - absoluteOffset); ;
    }

   

    public void GameOver()
    {
        if(!gameOver)
        {       
            gameOver = true;
            GameCenterManager.Instance.ReportScore(bestScore, leaderboardId);
            Invoke(nameof(OpenGameOverPanel), delayBeforeGameOverPanel);
        }
        
    }

    private void OpenGameOverPanel()
    {
        ScreenshotManager.Instance.CaptureBucketScreenshot();
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "Score: " + score;
    }

    public void ResetGame()
    {
        score = 0;
        scoreText.text = "0";
        gameOverPanel.SetActive(false);
        gameOver = false;
    }

    


}
