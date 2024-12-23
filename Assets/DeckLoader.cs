
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckLoader : MonoBehaviour
{

    private static readonly string THEME_KEY = "theme";

    public Button[] deckButtons;
    

    void Start()
    {
        for (int i = 0; i < deckButtons.Length; i++)
        {
            deckButtons[i].onClick.AddListener(delegate {
                LoadDeck(i);
            });
        } 
    }

    private void LoadDeck(int themeIndex)
    {
        PlayerPrefs.GetInt(THEME_KEY, themeIndex);
        SceneManager.LoadScene("SampleScene");
    }
   
}
