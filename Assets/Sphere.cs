using UnityEngine;

using DG.Tweening;
public class Sphere : MonoBehaviour
{
    public float delay = 0.3f;
    public float fadeTime = 0.3f;
    // Start is called before the first frame update
    void OnEnable()
    {

        Invoke(nameof(Fade), delay);
        
    }

    private void Fade()
    {
        GetComponent<SpriteRenderer>().DOFade(0, fadeTime).OnComplete(() => {
            Destroy(gameObject);
        });
    }

    
}
