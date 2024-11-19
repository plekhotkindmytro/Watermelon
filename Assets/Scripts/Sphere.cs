using UnityEngine;

using DG.Tweening;
public class Sphere : MonoBehaviour
{
    public float delay = 0.3f;
    public float fadeTime = 0.3f;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke(nameof(Fade), delay);
        
    }

    private void Fade()
    {
        if(spriteRenderer == null)
        {
            return;
        }
        spriteRenderer.DOFade(0, fadeTime).OnComplete(() => {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        transform.DOKill();
        this.CancelInvoke();
        StopAllCoroutines();
    }
}
