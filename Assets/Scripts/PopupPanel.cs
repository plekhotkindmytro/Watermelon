
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopupPanel : MonoBehaviour
{

    public float scaleDuration = 0.2f;
    public float showDuration = 2f;
    public AudioSource sound;

    public TMP_Text textCompoment;

    
    public void Activate(string value)
    {
        textCompoment.text = value;
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, scaleDuration);
        if(sound != null) {sound.PlayDelayed(0.1f);}
        
        Invoke(nameof(Disable), showDuration);
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }

}
