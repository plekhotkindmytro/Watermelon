
using DG.Tweening;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float repeatRate = 3f;
    public float duration = 1;

    private RectTransform rectTransform;
    private float currentY; 
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentY = rectTransform.anchoredPosition.y;
        InvokeRepeating(nameof(AnimatePointer), 0f, repeatRate);
    }

    private void AnimatePointer()
    {
        
        rectTransform.anchoredPosition = Vector3.up * -100;
        rectTransform.DOAnchorPosY(currentY + 100, duration);
    }
}
