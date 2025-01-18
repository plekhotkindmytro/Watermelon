using UnityEngine;
using UnityEngine.UI; // For accessing the Image component
using DG.Tweening;
using System;    // For using DOTween

public class SceneTransitionPanel : MonoBehaviour
{
    
    public float fadeDuration = 1f;  // Duration of the fade-out effect
    private Image panelImage;  // Reference to the panel's Image component
    void Start()

    {
        panelImage = GetComponent<Image>();
        FadeOut();
       
    }

    private void FadeOut() 
    {
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
        
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0f);
        panelImage.gameObject.SetActive(true);
        // Fade out the panel's alpha
        panelImage.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            // Optional: Disable the panel after fade-out
            callback?.Invoke();
        });
    }
}
