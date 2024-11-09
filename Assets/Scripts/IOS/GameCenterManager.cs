using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameCenterManager : MonoBehaviour
{
    public static GameCenterManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        AuthenticateUser();
    }

    private void AuthenticateUser()
    {
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Game Center: Authenticated");
            }

            else
            {
                Debug.Log("Game Center: Authentication failed");
            }
                
        });
    }

    public void ReportScore(long score, string leaderboardID)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboardID, success => {
                if (success)
                {
                    Debug.Log("Score reported successfully");
                }  
                else
                {
                    Debug.Log("Failed to report score");
                }   
            });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("User not authenticated");
        }
    }

}
