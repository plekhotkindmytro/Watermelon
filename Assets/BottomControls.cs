using DG.Tweening;
using UnityEngine;

public class BottomControls : MonoBehaviour
{
    public float duration = 1f;
    public float contentDelay = 2f;
    private AudioSource sound;
    private RectTransform rectTransform;
    private float targetPosY;

    bool activated = false;

    void Start()
    {
        float startPosY = -500f;
        rectTransform = GetComponent<RectTransform>();
        targetPosY = rectTransform.anchoredPosition.y;
        rectTransform.anchoredPosition += Vector2.up * startPosY;

        
    }

    private void Update()
    {
        if(Input.touchCount > 0 && !activated)
        {
            activated = true;
            Invoke(nameof(AppearAnimation), contentDelay);
        }
    }

    private void AppearAnimation()
    {
        sound = GetComponent<AudioSource>();
        float delay = duration - sound.clip.length;

        //sound.PlayDelayed(delay > 0 ? delay : 0);        
        sound.Play();
        // Start a Yoyo loop animation
        rectTransform.DOAnchorPosY(targetPosY, duration).SetEase(Ease.OutElastic).OnComplete(()=> {

            this.enabled = false;
        });
    }
}
