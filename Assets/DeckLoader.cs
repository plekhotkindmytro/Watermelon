
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
            int index = i;
            deckButtons[index].onClick.AddListener(delegate {
                LoadDeck(index);
            });
        } 
    }

    private void LoadDeck(int themeIndex)
    {
        PlayerPrefs.SetInt(THEME_KEY, themeIndex);
        SceneManager.LoadScene("SampleScene");
    }
   
}
