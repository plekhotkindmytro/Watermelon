using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WaveUi : MonoBehaviour
{
    public float repeatRate = 3f;
    public float duration = 1;

    private RectTransform rectTransform;
    private Image image;
    private float currentY;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        currentY = rectTransform.anchoredPosition.y;
        InvokeRepeating(nameof(AnimatePointer), 0f, repeatRate);
    }

    private void AnimatePointer()
    {
        rectTransform.localScale = Vector3.one;
        rectTransform.DOScale(200f, duration).SetDelay(1F);
        image.color = Color.white;
        image.DOFade(0, duration).SetDelay(1F);
    }

  
}
