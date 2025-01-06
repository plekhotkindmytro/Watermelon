using UnityEngine;
using DG.Tweening;
using System;

public class CardAnimation : MonoBehaviour
{
    public float delay;
    private RectTransform rectTransform;
    public void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        // pulsating Efect;
        Invoke(nameof(PulsatingOneTime), delay);
    }



    private void PulsatingOneTime()
    {
        rectTransform.DOScale(new Vector3(0.7f, 0.7f, 1), 0.3f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    private void PulsatingEffect()
    {
        rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    //public void AnimateMe(int index, int totalCount)
    //{
    //    float defaultDuration = 4f;
    //    Invoke(nameof(PulsatingOneTime), 1f * index * defaultDuration / totalCount);
    //}


    //private void Move()
    //{
    //    rectTransform.DOAnchorPos(new Vector2(0, 0), 1.0f).From(new Vector2(0, -Screen.height));
    //}


    //public void ShakeCanvas()
    //{
    //    rectTransform.DOShakeAnchorPos(0.5f, 50, 10, 90, false, true);
    //}

}
