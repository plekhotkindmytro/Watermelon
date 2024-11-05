using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;


    public Sprite activeButtonSprite;
    public Sprite disabledButtonSprite;

    public GameObject settingsPanel;

    public Button settingsButton;
    public Button closeSettingsButton;
    public Button toggleMusicButton;
    public Button toggleSoundButton;
    public Button toggleVibrateButton;
    public Button replayButton;
    public Button leaderboard1Button;

    // game over
    public Button saveToPhotosButton;
    public Button restartButton;
    public Button leaderboardButton;


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

        // game over panel
        saveToPhotosButton.onClick.AddListener(ScreenshotManager.Instance.SaveToPhotos);
        restartButton.onClick.AddListener(Replay);
        leaderboardButton.onClick.AddListener(Leaderboard);

    }

    private void ApplyState(Button button, bool isActive)
    {
        button.image.sprite = isActive ? activeButtonSprite : disabledButtonSprite;
    }


    private void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    private void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
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

    private void Leaderboard()
    {
        // TODO
    }

}
