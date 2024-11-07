using UnityEngine;
using DG.Tweening;
using System;

public class Fruit : MonoBehaviour
{
    public int pointValue;
    public int fruitLevel;
    public float scaleFactor = 0.15f;
    public float baseScale = 0.3f;
    public Color color;
    public GameObject nextFruitPrefab;  // Prefab for the next-level fruit after merging
    public Sprite sleepSprite;
    public Sprite angrySprite;

    private bool hasMerged = false;     // Flag to prevent chain reactions
    private Vector3 targetScale;
    private SpriteRenderer spriteRenderer;
    private Sprite baseSprite;
    


    public void Awake()
    {
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        targetScale = Vector3.one * (baseScale + scaleFactor * fruitLevel);
        transform.localScale = targetScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSprite = spriteRenderer.sprite;
    }

    public Vector3 GetTargetScale()
    {
        return targetScale;
    }

    public void ActivateMe()
    {
        
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged)
        {
            return;
        }


        if (nextFruitPrefab == null)
        {
            return;
        }

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if (otherFruit != null && otherFruit.fruitLevel == fruitLevel && !otherFruit.hasMerged)
        {
            hasMerged = true;
            otherFruit.hasMerged = true;
            Merge(otherFruit);
        }
    }

    internal void SetBaseSprite()
    {
        spriteRenderer.sprite = baseSprite;
    }

    internal void SetSleepSprite()
    {
        spriteRenderer.sprite = sleepSprite;
    }

    internal void SetAngrySprite()
    {
        spriteRenderer.sprite = angrySprite;
    }


    private void Merge(Fruit otherFruit)
    {
        
        AudioManager.Instance.Splat();
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        otherFruit.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        // Spawn the next level fruit at the merge position
        Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2;

        
        GameObject fruitGameObject = Instantiate(nextFruitPrefab, mergePosition, Quaternion.identity);
        Fruit fruit = fruitGameObject.GetComponent<Fruit>();
        Vector3 targetScale = fruit.GetTargetScale();

        fruitGameObject.transform.localScale = Vector3.zero;
        fruitGameObject.transform.GetChild(0).gameObject.SetActive(true);
        fruitGameObject.transform.DOScale(targetScale, 0.5f).OnComplete(() => {
            fruit.ActivateMe();
        });
        
        if(fruit.fruitLevel >= 6)
        {
            if(fruit.fruitLevel <= 10)
            {
                VibrationManager.Instance.VibrateShort();
            } else
            {
                VibrationManager.Instance.VibrateLong();
            }
            
        }
        ParticleSpawner.Instance.SpawnFruitParticles(fruit);

        Destroy(otherFruit.gameObject);
        GameManager.Instance.AddScore(pointValue);
        
        
        Destroy(gameObject);
    }
}
