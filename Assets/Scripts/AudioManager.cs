
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    public AudioSource musicSource;         // AudioSource for background music
    public AudioSource soundEffectsSource;  // AudioSource for sound effects

    private bool isMusicEnabled = true;
    private bool isSoundEffectsEnabled = true;


    private readonly static string MUSIC_ENABLED_KEY = "MusicEnabled";
    private readonly static string SOUND_ENABLED_KEY = "SoundEnabled";
    private readonly static int ENABLED = 1;
    private readonly static int DISABLED = 0;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        // Load preferences for music and sound effects
        isMusicEnabled = PlayerPrefs.GetInt(MUSIC_ENABLED_KEY, ENABLED) == ENABLED;
        isSoundEffectsEnabled = PlayerPrefs.GetInt(SOUND_ENABLED_KEY, ENABLED) == ENABLED;

        // Set initial states based on preferences
        UpdateMusicState();
        UpdateSoundEffectsState();
    }

    // Enable Music
    public void EnableMusic()
    {
        isMusicEnabled = true;
        UpdateMusicState();
        PlayerPrefs.SetInt(MUSIC_ENABLED_KEY, ENABLED);
    }

    // Disable Music
    public void DisableMusic()
    {
        isMusicEnabled = false;
        UpdateMusicState();
        PlayerPrefs.SetInt(MUSIC_ENABLED_KEY, DISABLED);
    }

    private void UpdateMusicState()
    {
        musicSource.mute = !isMusicEnabled;
        if (isMusicEnabled && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
        else if (!isMusicEnabled && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    // Enable Sound Effects
    public void EnableSoundEffects()
    {
        isSoundEffectsEnabled = true;
        UpdateSoundEffectsState();
        PlayerPrefs.SetInt(SOUND_ENABLED_KEY, ENABLED);
    }

    // Disable Sound Effects
    public void DisableSoundEffects()
    {
        isSoundEffectsEnabled = false;
        UpdateSoundEffectsState();
        PlayerPrefs.SetInt(SOUND_ENABLED_KEY, DISABLED);
    }

    private void UpdateSoundEffectsState()
    {
        soundEffectsSource.mute = !isSoundEffectsEnabled;
    }

    // Method to play sound effects
    public void PlaySoundEffect()
    {
        if (isSoundEffectsEnabled)
        {
            soundEffectsSource.pitch = Random.Range(0.4f, 1.2f);
            soundEffectsSource.PlayOneShot(soundEffectsSource.clip);
        }
    }
}
