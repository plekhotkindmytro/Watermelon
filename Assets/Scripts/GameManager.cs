using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public Text scoreText;
    public GameObject gameOverPanel;
    public int maxFruitLevel = 2;
    //private int DEFAULT_MAX_FRUIT_LEVEL = 6;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
     
    public void AddScore(int points)
    {
        
        score += points;

        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned");
            return;

        }
        scoreText.text = "Score: " + score;
    }

    public void CheckGameOver()
    {
        //if (/* Add game over condition here, e.g., fruits reach the top of the screen */)
        //{
        //    gameOverPanel.SetActive(true);
        //    Time.timeScale = 0;  // Pause the game
        //}
    }

    public void SetMaxFruitLevel(int maxLevel)
    {
        
        maxFruitLevel = System.Math.Max(maxLevel, GameManager.Instance.maxFruitLevel);
        

    }
    public void ResetGame()
    {
        score = 0;
        scoreText.text = "Score: 0";
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    


}
