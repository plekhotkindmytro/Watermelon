using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;


    [Header("Configs")]
    public float animationSpeed = 0.1f;
    public Color activeColor;
    public Color passiveColor;

    [Header("Game")]
    public Button settingsButton;
    public Button homeButton;
    public GameObject bottomArrow;

    // Panels
    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject shopPanel;
    public GameObject gameOverPanel;

    private Transform settingsPanelContent;
    private Transform shopPanelContent;
    private Transform gameOverPanelContent;


    [Header("Settings Panel")]
    
    public Button closeSettingsButton;
    public Button toggleMusicButton;
    public Button toggleSoundButton;
    public Button toggleVibrateButton;
    public Button replayButton;
    public Button leaderboard1Button;
    

    [Header("Game Over Panel")]
    public Button saveToPhotosButton;
    public Button restartButton;
    public Button leaderboardButton;
    public Button gameOverHomeButton;

    [Header("Tutorial Panel")]
    public Tutorial firstTutorialPanel;


    [Header("Boosts")]
    public Button fishButton;
    public Button mouseButton;
    public Button beeButton;
    public Button flyButton;
    public Button mosquitoButton;

    public TMP_Text fishCountText;
    public TMP_Text mouseCountText;
    public TMP_Text beeCountText;

    [Header("Shop Panel")]
    public Button buyMouseButton;
    public Button buyBeeButton;
    public Button buyFishButton;
    public Button closeShopButton;
    public TMP_Text mousePriceText;
    public TMP_Text beePriceText;
    public TMP_Text fishPriceText;
    

    


    private bool isMusicOn = true;
    private bool isSoundOn = true;
    private bool isVibrateOn = true;

    private readonly static int ON = 1;
    private readonly static int OFF = 0;
    private readonly static string MUSIC_ENABLED_KEY = "MusicEnabled";
    private readonly static string SOUND_ENABLED_KEY = "SoundEnabled";
    private readonly static string VIBRATE_ENABLED_KEY = "VibrateEnabled";
    


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        // Load saved settings and initialize toggles
        isMusicOn = PlayerPrefs.GetInt(MUSIC_ENABLED_KEY, ON) == ON;
        isSoundOn = PlayerPrefs.GetInt(SOUND_ENABLED_KEY, ON) == ON;
        isVibrateOn = PlayerPrefs.GetInt(VIBRATE_ENABLED_KEY, ON) == ON;

        ApplyState(toggleMusicButton, isMusicOn);
        ApplyState(toggleSoundButton, isSoundOn);
        ApplyState(toggleVibrateButton, isVibrateOn);


        settingsButton.onClick.AddListener(OpenSettingsPanel);
        closeSettingsButton.onClick.AddListener(CloseSettingsPanel);
        toggleMusicButton.onClick.AddListener(ToggleMusic);
        toggleSoundButton.onClick.AddListener(ToggleSound);
        toggleVibrateButton.onClick.AddListener(ToggleVibrate);
        replayButton.onClick.AddListener(Replay);
        leaderboard1Button.onClick.AddListener(Leaderboard);
        homeButton.onClick.AddListener(Home);

        // game over panel
        saveToPhotosButton.onClick.AddListener(ScreenshotManager.Instance.SaveToPhotos);
        restartButton.onClick.AddListener(Replay);
        leaderboardButton.onClick.AddListener(Leaderboard);
        gameOverHomeButton.onClick.AddListener(Home);

        fishButton.onClick.AddListener(SpawnFish);
        mouseButton.onClick.AddListener(SpawnMouse);
        beeButton.onClick.AddListener(SpawnBee);
        //flyButton.onClick.AddListener(SpawnFly);
        //mosquitoButton.onClick.AddListener(SpawnMosquito);


        buyMouseButton.onClick.AddListener(IAPManager.Instance.BuyMouse);
        buyBeeButton.onClick.AddListener(IAPManager.Instance.BuyBee);
        buyFishButton.onClick.AddListener(IAPManager.Instance.BuyFish);
        closeShopButton.onClick.AddListener(CloseShopPanel);

        HidePanels();
        

    }

    private void HidePanels()
    {
        settingsPanelContent = settingsPanel.transform.GetChild(0);
        shopPanelContent = shopPanel.transform.GetChild(0);
        gameOverPanelContent = gameOverPanel.transform.GetChild(0);

        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        firstTutorialPanel.HideOrShow();
    }

    private void ApplyState(Button button, bool isActive)
    {
        button.image.color = isActive ?  activeColor: passiveColor;
        //Transform textObject = button.transform.GetChild(0);
        //TMP_Text buttonText = textObject.GetComponent<TMP_Text>();

        //FontStyles activeFontStyle = FontStyles.UpperCase;
        //FontStyles disabledFontStyle = (FontStyles.UpperCase | FontStyles.Strikethrough);
        //buttonText.fontStyle = isActive? activeFontStyle : disabledFontStyle;
    }

    private void Home()
    {
        SaveManager.Instance.SaveData();
        SceneManager.LoadScene("MenuScene");
    }


    private void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        settingsPanelContent.localScale = Vector3.zero;

        settingsPanelContent.DOScale(1, animationSpeed).OnComplete(() =>
        {
            
        });
        
    }

    private void CloseSettingsPanel()
    {
        settingsPanelContent.DOScale(0, animationSpeed).OnComplete(() =>
        {
            settingsPanel.SetActive(false);
        });
    }

    public void OpenShopPanel()
    {
        shopPanel.SetActive(true);
        shopPanelContent.localScale = Vector3.zero;
        shopPanelContent.DOScale(1, animationSpeed).OnComplete(() =>
        {
            
        });
    }

    public void CloseShopPanel()
    {
        shopPanelContent.DOScale(0, animationSpeed).OnComplete(() =>
        {
            shopPanel.SetActive(false);
        });
    }

    public void OpenGameOverPanel(TMP_Text gameOverScoreText, int score)
    {
        gameOverPanel.SetActive(true);
        ScreenshotManager.Instance.CaptureBucketScreenshot();
        gameOverScoreText.text = "Score: " + score;
        gameOverPanel.GetComponent<GameOverPanel>().Show(); 
        
        
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        // Enable or disable music based on the toggle state
        if (isMusicOn)
        {
            // Play music or unmute audio source
            AudioManager.Instance.EnableMusic();
        }
        else
        {
            // Pause music or mute audio source
            AudioManager.Instance.DisableMusic();
        }

        // Save preference
        PlayerPrefs.SetInt(MUSIC_ENABLED_KEY, isMusicOn ? ON : OFF);
        ApplyState(toggleMusicButton, isMusicOn);
    }

    // Sound Toggle
    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        // Enable or disable sound effects based on the toggle state
        if (isSoundOn)
        {
            AudioManager.Instance.EnableSoundEffects();
        }
        else
        {
            AudioManager.Instance.DisableSoundEffects();
        }

        // Save preference
        PlayerPrefs.SetInt(SOUND_ENABLED_KEY, isSoundOn ? ON : OFF);
        ApplyState(toggleSoundButton, isSoundOn);
    }

    // Vibrate Toggle
    public void ToggleVibrate()
    {
        isVibrateOn = !isVibrateOn;
        // Enable or disable vibration functionality
        if (isVibrateOn)
        {
            // Enable vibration for specific actions
            VibrationManager.Instance.EnableVibration();
        }
        else
        {
            // Disable vibration
            VibrationManager.Instance.DisableVibration();
        }

        // Save preference
        PlayerPrefs.SetInt(VIBRATE_ENABLED_KEY, isVibrateOn ? ON : OFF);
        ApplyState(toggleVibrateButton, isVibrateOn);
    }

    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GemScene()
    {
        SceneManager.LoadScene("GemScene");
    }

    private void Leaderboard()
    {
        GameCenterManager.Instance.ShowLeaderboard();
    }

    private void SpawnFish()
    {
        BoostManager.Instance.SpawnFish();
    }
    private void SpawnMouse()
    {
        BoostManager.Instance.SpawnMouse();
    }
    private void SpawnBee()
    {
        BoostManager.Instance.SpawnBee();
    }
    //private void SpawnFly()
    //{
    //    BoostManager.Instance.SpawnFly();
    //}
    //private void SpawnMosquito()
    //{
    //    BoostManager.Instance.SpawnMosquito();
    //}

    public void RevealFruitByLevel(int level, SpriteRenderer spriteRenderer)
    {
        if(level < 7)
        {
            return;
        }

        Transform wrapper = bottomArrow.transform.GetChild(0);
        Transform fruit = wrapper.GetChild(level - 1);
        
        Image image = fruit.GetComponent<Image>();

        image.sprite = spriteRenderer.sprite;
        image.color = spriteRenderer.color;

        Transform questionMark = fruit.GetChild(0);
        if(questionMark.gameObject.activeSelf)
        {
            questionMark.gameObject.SetActive(false);
            fruit.DOScale(1.5f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }
    }

    

}
