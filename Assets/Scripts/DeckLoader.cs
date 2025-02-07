
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckLoader : MonoBehaviour
{

    private static readonly string THEME_KEY = "theme";

    public Button randomButton;
    public Button vegetablesButton;
    public Button fruitsButton;
    public Button numbersButton;
    public Button gemsButton;
    public Button kenneyAnimalsButton;
    public Button ballsButton;
    public Button kenneyLetters;
    public Button foodStickers;
    public Button instrumentsButton;

    public SceneTransitionPanel sceneTransitionPanel;
    public SceneScaleTransitionPanel sceneScaleTransitionPanel;

    private void Start()
    {

        //PlayerPrefs.SetInt(CardAvailable.LEVEL_REACHED_KEY, 8);

        int levelReached = PlayerPrefs.GetInt(CardAvailable.LEVEL_REACHED_KEY);
        randomButton.onClick.AddListener(() =>
                    LoadDeck(Random.Range(0, levelReached + 1)));
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
        instrumentsButton.onClick.AddListener(() =>
                    LoadDeck(8));

    }

    private void LoadDeck(int themeIndex)
    {
        PlayerPrefs.SetInt(THEME_KEY, themeIndex);
        LoadSceneByIndex(1);
    }

    private void LoadSceneByIndex(int index) 
    {
        sceneTransitionPanel.FadeIn(() => {SceneManager.LoadScene(index);});
        //sceneScaleTransitionPanel.SceneLeaveTransition(() => {SceneManager.LoadScene(index);});
    }
   
}
