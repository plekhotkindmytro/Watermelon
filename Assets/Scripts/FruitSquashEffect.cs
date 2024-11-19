using UnityEngine;
using DG.Tweening;
using System.Collections;

public class FruitSquashEffect : MonoBehaviour
{
    [SerializeField] private float squashAmount = 0.8f;    // Squash intensity
    [SerializeField] private float squashDuration = 0.1f;  // Duration of squash effect
    [SerializeField] private float squashShift = 0.05f;    // Small positional shift for realism
    [SerializeField] private float minVelocityForSquash = 2f; // Minimum velocity for squash trigger
    [SerializeField] private float squashCooldown = 0.2f;  // Cooldown between squash triggers

    private Vector3 originalScale;
    private bool canSquash = true;  // Cooldown control
    private Fruit fruit;
    private Rigidbody2D rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fruit = GetComponent<Fruit>();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collision is with the ground or wall and velocity is high enough
        if (canSquash)
        {
            if (rb != null && rb.velocity.magnitude > minVelocityForSquash)
            {
                Vector3 impactNormal = collision.contacts[0].normal;
                Squash(impactNormal);
                StartCoroutine(SquashCooldown());
            }
        }
    }

    private void Squash(Vector3 impactNormal)
    {

        originalScale = transform.localScale;
        Vector3 squashScale = transform.localScale;
        
        if (Mathf.Abs(impactNormal.y) > Mathf.Abs(impactNormal.x)) // Vertical impact
        {
            squashScale.y *= squashAmount;          // Squash on Y-axis
            squashScale.x *= 1.1f;                  // Stretch on X-axis
        }
        else // Horizontal impact
        {
            squashScale.x *= squashAmount;          // Squash on X-axis
            squashScale.y *= 1.1f;                  // Stretch on Y-axis
        }

        // Adjust position slightly in the direction of the impact to make squash look more natural
        Vector3 shiftPosition = transform.position + (impactNormal * squashShift);
        if(fruit == null)
        {
            return;
        }

        fruit.SetRandomSprite();

        // Animate squash with DOTween
        transform.DOMove(shiftPosition, squashDuration * 0.5f); // Small shift for realism
        transform.DOScale(squashScale, squashDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => {

                if(transform == null)
                {
                    return;
                }

                transform.DOScale(originalScale, squashDuration).SetEase(Ease.OutBounce).OnComplete(()=> {
                    
                });

            });
    }

    private IEnumerator SquashCooldown()
    {
        canSquash = false;
        yield return new WaitForSeconds(squashCooldown);
        canSquash = true;
    }

    private void OnDestroy()
    {
        transform.DOKill();
        this.CancelInvoke();
        StopAllCoroutines();
    }
}
