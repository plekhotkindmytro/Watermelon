using UnityEngine;
using UnityEngine.UI; // For accessing the Image component
using DG.Tweening;
using System;
using UnityEngine.Audio;    // For using DOTween

public class SceneTransitionPanel : MonoBehaviour
{
    
    public float fadeDuration = 1f;  // Duration of the fade-out effect

    public AudioMixer audioMixer;
    
    public string volumeParameter = "MasterVolume";
    private float minVolume = -80f; // Minimum volume in dB
    private float maxVolume = 0f;  // Maximum volume in dB

    private Image panelImage;  // Reference to the panel's Image component
    void Start()

    {
        panelImage = GetComponent<Image>();
        FadeOut();
       
    }

    private void FadeOut() 
    {
        FadeInAudio();
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 1f);

        // Fade out the panel's alpha
        panelImage.DOFade(0f, fadeDuration)
        //.SetEase(Ease.InOutQuad)
        .OnComplete(() =>
        {
            // Optional: Disable the panel after fade-out
            panelImage.gameObject.SetActive(false);
        });
    }

    public void FadeIn(Action callback) 
    {
        FadeOutAudio();
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0f);
        panelImage.gameObject.SetActive(true);
        // Fade out the panel's alpha
        panelImage.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            // Optional: Disable the panel after fade-out
            callback?.Invoke();
        });
    }

     public void FadeOutAudio()
    {
        float currentVolume;
        audioMixer.GetFloat(volumeParameter, out currentVolume);

        // Tween the volume to the minimum value
        DOTween.To(() => currentVolume, x => audioMixer.SetFloat(volumeParameter, x), minVolume, fadeDuration);
    }

    public void FadeInAudio()
    {
        float currentVolume;
        audioMixer.GetFloat(volumeParameter, out currentVolume);

        // Tween the volume to the maximum value
        DOTween.To(() => currentVolume, x => audioMixer.SetFloat(volumeParameter, x), maxVolume, fadeDuration);
    }
}
