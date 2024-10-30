using System.Runtime.InteropServices;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance;
    private bool isVibrationEnabled = true;

    // Import the iOS haptic feedback functions
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void PlayShortHaptic();

    [DllImport("__Internal")]
    private static extern void PlayLongHaptic();
#endif

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
    }

    public void EnableVibration()
    {
        isVibrationEnabled = true;
        PlayerPrefs.SetInt("VibrationEnabled", 1);
    }

    public void DisableVibration()
    {
        isVibrationEnabled = false;
        PlayerPrefs.SetInt("VibrationEnabled", 0);
    }

    public void VibrateShort()
    {
        if (isVibrationEnabled)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                VibrateAndroid(100); // Short vibration
#elif UNITY_IOS && !UNITY_EDITOR
                PlayShortHaptic();   // Short haptic on iOS
#endif
        }
    }

    public void VibrateLong()
    {
        if (isVibrationEnabled)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                VibrateAndroid(500); // Long vibration
#elif UNITY_IOS && !UNITY_EDITOR
                PlayLongHaptic();    // Long haptic on iOS
#endif
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void VibrateAndroid(long milliseconds)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

        if (vibrator.Call<bool>("hasVibrator"))
        {
            vibrator.Call("vibrate", milliseconds);
        }
    }
#endif
}
