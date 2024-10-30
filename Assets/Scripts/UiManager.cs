using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;


    public GameObject settingsPanel;

    public Button settingsButton;
    public Button closeSettingsButton;
    public Button toggleMusicButton;
    public Button toggleSoundButton;
    public Button toggleVibrateButton;
    public Button replayButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        settingsButton.onClick.AddListener(OpenSettingsPanel);
        closeSettingsButton.onClick.AddListener(CloseSettingsPanel);
        toggleMusicButton.onClick.AddListener(ToggleMusic);
        toggleSoundButton.onClick.AddListener(ToggleSound);
        toggleVibrateButton.onClick.AddListener(ToggleVibrate);
        replayButton.onClick.AddListener(Replay);

    }


    private void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    private void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    private void ToggleMusic()
    {
        // TODO
    }

    private void ToggleSound()
    {
        // TODO
    }

    private void ToggleVibrate()
    {
        // TODO
    }

    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
