using UnityEngine;
using DG.Tweening;

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
    private bool angry = false;
    private bool sleep = false;
    private bool inWarningZone = false;


    // warning
    public float timeOutsideBeforeWarning = 2f;
    public float timeOutsideBeforeBorderWarning = 2f;
    public float timeOutsideBeforeGameOver = 5.5f;
    private float timeInWarningZoneElapsed = 0;

    private Sprite[] sprites;


    public void Awake()
    {
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<FruitSquashEffect>().enabled = false;
        targetScale = Vector3.one * (baseScale + scaleFactor * fruitLevel);
        transform.localScale = targetScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSprite = spriteRenderer.sprite;
    }

    public void Start()
    {
        sprites = new Sprite[]{
            baseSprite, angrySprite, sleepSprite
        };
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, new Vector3(transform.position.x, GameManager.Instance.GetBoxBottomY()));
    }

    public Vector3 GetTargetScale()
    {
        return targetScale;
    }

    public void ActivateMe()
    {
        
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        this.GetComponent<LineRenderer>().enabled = false;
        this.GetComponent<FruitSquashEffect>().enabled = true;

    }

    private void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            if(!sleep && !angry)
            {
                sleep = true;
                SetSleepSprite();
            }
            

            return;
        }

        if(inWarningZone)
        {

            if (timeInWarningZoneElapsed >= timeOutsideBeforeBorderWarning)
            {
                GameManager.Instance.Warn();
            }

            if (timeInWarningZoneElapsed >= timeOutsideBeforeWarning)
            {
                if (!angry)
                {
                    angry = true;
                    SetAngrySprite();
                }

                
                if (timeInWarningZoneElapsed >= timeOutsideBeforeGameOver)
                {
                    GameManager.Instance.GameOver();
                }
            }
            timeInWarningZoneElapsed += Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        inWarningZone = true;

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        inWarningZone = false;
        timeInWarningZoneElapsed = 0;
        angry = false;
        SetBaseSprite();
        GameManager.Instance.CancelWarn();
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
        spriteRenderer.color = Color.white;
    }

    internal void SetSleepSprite()
    {
        spriteRenderer.sprite = sleepSprite;
        spriteRenderer.color = Color.white;
    }

    internal void SetAngrySprite()
    {
        spriteRenderer.sprite = angrySprite;
        spriteRenderer.color = Color.red;
    }

    internal void SetRandomSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
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
