using UnityEngine;
using UnityEngine.UI; // For accessing the Image component
using DG.Tweening;
using System;    // For using DOTween

public class SceneScaleTransitionPanel : MonoBehaviour
{
    
    public float duration = 1f;  // Duration of the fade-out effect
    private float maxScale = 4f;
    void Start()
    {
        SceneEnterTransition();
    }

    private void SceneEnterTransition() 
    {
        transform.localScale = Vector3.one * maxScale;
        transform.DOScale(0, duration);
    }

    public void SceneLeaveTransition(Action sceneLoadAction) 
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(maxScale, duration).OnComplete(() =>
        {
            // Optional: Disable the panel after fade-out
            sceneLoadAction?.Invoke();
        });
    }
}
