
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckLoader : MonoBehaviour
{

    private static readonly string THEME_KEY = "theme";

    public Button vegetablesButton;
    public Button fruitsButton;
    public Button numbersButton;
    public Button gemsButton;
    public Button kenneyAnimalsButton;
    public Button ballsButton;
    public Button kenneyLetters;
    public Button foodStickers;


    void Start()
    {
        vegetablesButton.onClick.AddListener(() =>
                    LoadDeck(0));
        fruitsButton.onClick.AddListener(() =>
                    LoadDeck(1));
        numbersButton.onClick.AddListener(() =>
                    LoadDeck(2));
        gemsButton.onClick.AddListener(() =>
                    LoadDeck(3));
        kenneyAnimalsButton.onClick.AddListener(() =>
                    LoadDeck(4));
        ballsButton.onClick.AddListener(() =>
                    LoadDeck(5));
        kenneyLetters.onClick.AddListener(() =>
                    LoadDeck(6));
        foodStickers.onClick.AddListener(() =>
                    LoadDeck(7));

    }

    private void LoadDeck(int themeIndex)
    {
        //print("theme index clicked: " + themeIndex);
        PlayerPrefs.SetInt(THEME_KEY, themeIndex);
        SceneManager.LoadScene("SampleScene");
    }
   
}
