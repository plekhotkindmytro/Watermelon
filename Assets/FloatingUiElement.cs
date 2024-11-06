using UnityEngine;
using DG.Tweening;

public class FloatingUiElement : MonoBehaviour
{
    public float floatDistance = 10f; // Distance to move up and down
    public float floatDuration = 1f;

    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Start a Yoyo loop animation
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - floatDistance, floatDuration)
                     .SetEase(Ease.InOutSine)
                     .SetLoops(-1, LoopType.Yoyo);
    }

}
