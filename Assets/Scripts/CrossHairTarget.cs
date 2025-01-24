using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CrossHairTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(0.80f, 0.9f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
