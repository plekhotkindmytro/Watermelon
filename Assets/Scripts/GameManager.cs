using TMPro;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverScoreText;
    
    public float delayBeforeGameOverPanel = 1.5f;
    public string leaderboardId = "main";

    private static readonly string BEST_SCORE_KEY = "bestScore";
    private static readonly string TUTORIAL_KEY = "tutorial";
    private static readonly string TUTORIAL2_KEY = "tutorial2";


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


    public bool IsGamePaused() {

        bool pause = false;

        if (UiManager.Instance.settingsPanel.activeSelf)
        {
            pause = true;
        } else if (UiManager.Instance.shopPanel.activeSelf)
        {
            pause = true;
        } else if (UiManager.Instance.afterTutorialPanel.activeSelf)
        {
            pause = true;
        }
        else if (UiManager.Instance.newLevelPanel.activeSelf)
        {
            pause = true;
        }
        else if (GameManager.Instance.gameOver)
        {
            pause = true;
        }

        return pause;
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
        //UiManager.Instance.leaderboard1Button.transform.DOComplete(); // Completes any active tweens on the score text
        UiManager.Instance.leaderboard1Button.transform.DOScale(1.1f, 0.1f).SetDelay(1f).SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => {
                UiManager.Instance.leaderboard1Button.transform.localScale = Vector3.one;
            });
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
        UiManager.Instance.OpenGameOverPanel(gameOverScoreText, score);
    }


    public bool IsTutorial()
    {
        return PlayerPrefs.GetInt(TUTORIAL_KEY, 0) == 0;
    }

    public bool IsTutorial2()
    {
        return !IsTutorial() && PlayerPrefs.GetInt(TUTORIAL2_KEY, 0) == 0;
    }



    public void FinishTutorial()
    {
        UiManager.Instance.firstTutorialPanel.gameObject.SetActive(false);
        PlayerPrefs.SetInt(TUTORIAL_KEY, 1);
        UiManager.Instance.secondTutorialPanel.gameObject.SetActive(true);

        // Hide Tutorial panel
    }

    public void FinishTutorial2()
    {
        UiManager.Instance.secondTutorialPanel.gameObject.SetActive(false);
        PlayerPrefs.SetInt(TUTORIAL2_KEY, 1);
       // UiManager.Instance.OpenHowToPlayPanel();
    }

    //public void ResetGame()
    //{
    //    score = 0;
    //    scoreText.text = "0";
    //    gameOverPanel.SetActive(false);
    //    gameOver = false;
    //}




}
