using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class OutlineScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.DOScale(1f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
