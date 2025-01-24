using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnlockButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOScale(1.2f, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
