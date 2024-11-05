using UnityEngine;
using DG.Tweening;

public class ScaleDownOnEnable : MonoBehaviour
{
    public float from = 1.2f;
    public float to = 1f;
    public float duration = 1f;
    public float contentDelay = 0.2f;
    
    public AudioSource scaleSound;
    private GameObject content;
    

    private void OnEnable()
    {
        content = transform.GetChild(0).gameObject;
        content.SetActive(false);

        Invoke(nameof(ShowContent), contentDelay);
        
        
    }

    private void ShowContent()
    {
        content.SetActive(true);
        float delay = duration - scaleSound.clip.length;
        scaleSound.PlayDelayed(delay > 0 ? delay : 0);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one * from;
        rectTransform.DOScale(to, duration);
    }
}
