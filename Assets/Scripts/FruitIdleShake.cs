using DG.Tweening;
using UnityEngine;

public class FruitIdleShake : MonoBehaviour
{
    public float shakeDistance = 0.1f;     // Distance to move left and right
    public float shakeDuration = 0.25f;    // Duration for each shake movement
    public float idleTimeThreshold = 3f;   // Time before shaking starts when idle

    private Vector3 originalPosition;
    private float idleTimer;
    private Fruit fruit;
    private Sequence shakeSequence;

    void Start()
    {
        originalPosition = transform.position;
        fruit = GetComponent<Fruit>();
    }

    void Update()
    {

        // Increment idle timer if no touch input is detected
        if (Input.touchCount == 0)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            // Reset timer if there is any touch input
            idleTimer = 0f;
           
            StopShaking();
            return;
        }

        // Start shaking if the idle time threshold is reached
        if (idleTimer >= idleTimeThreshold)
        {
            idleTimer = 0f;
            fruit.SetSleepSprite();
            Invoke(nameof(StartShaking), 0.3f);
        }
    }

    private void StartShaking()
    {
        if(fruit == null)
        {
            return;
        }
        fruit.SetSleepSprite();
        shakeSequence = DOTween.Sequence();

        // Shake left and right in a loop
        shakeSequence.Append(transform.DOMoveX(originalPosition.x - shakeDistance, shakeDuration)
            .SetEase(Ease.InOutSine));
        shakeSequence.Append(transform.DOMoveX(originalPosition.x + shakeDistance, shakeDuration)
            .SetEase(Ease.InOutSine));
        shakeSequence.SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            if (fruit == null)
            {
                return;
            }
            fruit.SetBaseSprite();
        });
         // Loop indefinitely until stopped

    }

    public void StopShaking()
    {

        this.CancelInvoke();
        shakeSequence.Kill(); // Stops any active tweens
        //transform.position = originalPosition; // Reset to original position
        if(fruit != null)
        {
            fruit.SetBaseSprite();
        }
        
        //this.enabled = false;
    }

    private void OnDestroy()
    {
        transform.DOKill();
        this.CancelInvoke();
        StopAllCoroutines();
    }
}
