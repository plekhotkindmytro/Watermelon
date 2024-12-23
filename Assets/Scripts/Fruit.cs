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
    private Color baseColor;

    private Sprite[] sprites;


    public void Awake()
    {
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Collider2D>().enabled = false;
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

        //if (this.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
        //{
        //    ActivateMe();
        //    return;
        //}
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, new Vector3(transform.position.x, GameManager.Instance.GetBoxBottomY()));
        baseColor = spriteRenderer.color;
    }

    public Vector3 GetTargetScale()
    {
        return targetScale;
    }

    public void ActivateMe()
    {

        this.transform.DOKill();
        this.transform.localScale = targetScale;
        
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<Collider2D>().enabled = true;
        this.GetComponent<FruitSquashEffect>().enabled = true;

        this.GetComponent<LineRenderer>().enabled = false;
        FruitIdleShake shake = this.GetComponent<FruitIdleShake>();
        if(shake.enabled)
        {
            shake.StopShaking();
            shake.enabled = false;
        }
        
        ParticleSpawner.Instance.SpawnFruitParticles(GetComponent<Fruit>());
        this.transform.GetChild(1).gameObject.SetActive(false);
        //this.transform.DOScale(0.1f, 0.1f).OnComplete(() => { this.transform.DOScale(targetScale, 0.1f); });
        CameraManager.Instance.Follow(this.transform);

        UiManager.Instance.RevealFruitByLevel(this.fruitLevel);
    }



    private void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            if (!sleep && !angry)
            {
                sleep = true;
                SetSleepSprite();
            }


            return;
        }

        if (inWarningZone)
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
        if(spriteRenderer == null)
        {
            return;
        }
        spriteRenderer.sprite = baseSprite;
        spriteRenderer.color = baseColor;
    }

    internal Sprite GetBaseSprite()
    {
        return baseSprite;
    }

    internal void SetSleepSprite()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (sleepSprite == null)
        {
            return;
        }
        spriteRenderer.sprite = sleepSprite;
        spriteRenderer.color = baseColor;
    }

    internal void SetAngrySprite()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (angrySprite == null)
        {
            return;
        }

        spriteRenderer.sprite = angrySprite;
        spriteRenderer.color = Color.red;
    }

    internal void SetRandomSprite()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        var sprite = sprites[Random.Range(0, sprites.Length)];
        if(sprite == null) { return; }

        spriteRenderer.sprite = sprite;
        
    }


    private void Merge(Fruit otherFruit)
    {
        if (nextFruitPrefab == null)
        {
            return;
        }

        AudioManager.Instance.Splat();
        gameObject.GetComponent<Collider2D>().enabled = false;
        otherFruit.gameObject.GetComponent<Collider2D>().enabled = false;
        // Spawn the next level fruit at the merge position
        Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2;

        
        GameObject fruitGameObject = Instantiate(nextFruitPrefab, mergePosition, Quaternion.identity);
        Fruit fruit = fruitGameObject.GetComponent<Fruit>();

        

        Vector3 targetScale = fruit.GetTargetScale();

        fruitGameObject.transform.localScale = Vector3.zero;
        fruitGameObject.transform.GetChild(0).gameObject.SetActive(true);
        fruitGameObject.transform.DOScale(targetScale, 0.4f).OnComplete(() => {
            fruit.ActivateMe();
            
        });
        
        if(fruit.fruitLevel >= 7)
        {
            BoostManager.Instance.TryAddBoost(fruit.fruitLevel);
            if (fruit.fruitLevel <= 10)
            {
                VibrationManager.Instance.VibrateShort();
            } else
            {
                VibrationManager.Instance.VibrateLong();
            }
            
        }
        ParticleSpawner.Instance.SpawnFruitParticles(fruit);

       // 
        
        GameManager.Instance.AddScore(pointValue);

        //
        FruitSquashEffect otherSquash = otherFruit.GetComponent<FruitSquashEffect>();
        if (otherSquash != null && otherSquash.enabled)
        {
            otherSquash.DOKill();
            otherSquash.StopAllCoroutines();
            otherSquash.CancelInvoke();
        }
        otherFruit.CancelInvoke();
        otherFruit.DOKill();
        Destroy(otherFruit.gameObject);

        FruitSquashEffect thisSquash = this.GetComponent<FruitSquashEffect>();
        if (thisSquash != null && thisSquash.enabled)
        {
            thisSquash.DOKill();
            thisSquash.StopAllCoroutines();
            thisSquash.CancelInvoke();   
        }

        this.CancelInvoke();
        this.DOKill();
        Destroy(gameObject);
    }

    public void BeeMerge()
    {
        if(nextFruitPrefab == null)
        {
            return;
        }

        AudioManager.Instance.Splat();
        gameObject.GetComponent<Collider2D>().enabled = false;
        
        
        GameObject fruitGameObject = Instantiate(nextFruitPrefab, transform.position, Quaternion.identity);
        Fruit fruit = fruitGameObject.GetComponent<Fruit>();
        Vector3 targetScale = fruit.GetTargetScale();

        fruitGameObject.transform.localScale = Vector3.zero;
        fruitGameObject.transform.GetChild(0).gameObject.SetActive(true);
        fruitGameObject.transform.DOScale(targetScale, 0.4f).OnComplete(() => {
            fruit.ActivateMe();

        });

        if (fruit.fruitLevel >= 6)
        {
            if (fruit.fruitLevel <= 10)
            {
                VibrationManager.Instance.VibrateShort();
            }
            else
            {
                VibrationManager.Instance.VibrateLong();
            }

        }
        ParticleSpawner.Instance.SpawnFruitParticles(fruit);

        // 

        GameManager.Instance.AddScore(pointValue);

       

        FruitSquashEffect thisSquash = this.GetComponent<FruitSquashEffect>();
        if (thisSquash != null && thisSquash.enabled)
        {
            thisSquash.DOKill();
            thisSquash.StopAllCoroutines();
            thisSquash.CancelInvoke();
        }

        this.CancelInvoke();
        this.DOKill();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
