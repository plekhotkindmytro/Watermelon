using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
public class Spawner : MonoBehaviour
{
    public Image nextFruitImage;
    public GameObject[] fruitPrefabs;  // List of all 11 fruit prefabs
    public float minSecondsBetweenSpawns = 1f;
    public float spawnOffsetY = 10;
    public float clickableScreenPercentage = 0.85f;
    public float bottomClickableScreenPercentage = 0.15f;

    private Fruit controlledFruit = null;
    private Fruit nextFruit = null;

    private bool tutorialFruitCreated = false;

    private Camera cameraMain;


    private float timeElapsed = 0;
    private Vector2 lastDropPosition;


    public void Start()
    {
        cameraMain = Camera.main;
        timeElapsed = minSecondsBetweenSpawns;
        
        //CameraManager.Instance.Follow(controlledFruit.transform);
    }

    private void Update()
    {
        if(fruitPrefabs == null || fruitPrefabs.Length < 0)
        {
            return;
        }

        if (UiManager.Instance.settingsPanel.activeSelf)
        {
            return;
        }

        if (UiManager.Instance.shopPanel.activeSelf)
        {
            return;
        }

        if (controlledFruit == null)
        {
            return;
        }

        if (GameManager.Instance.gameOver)
        {
            return;
        }

        timeElapsed += Time.deltaTime;

        

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.y > Screen.height * clickableScreenPercentage || touch.position.y < Screen.height * bottomClickableScreenPercentage)
            {
                return;
            }

            Vector2 touchWorldPos = cameraMain.ScreenToWorldPoint(touch.position);
            

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
            {
                UpdateFruitPosition(touchWorldPos);
            }
            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                DropFruit();
            }
        }


    }

    private bool CanDrop()
    {
        bool canDrop = timeElapsed >= minSecondsBetweenSpawns;
        if (canDrop)
        {
            timeElapsed = 0;
        }

        return canDrop;
    }

    private void DropFruit()
    {
        if (!CanDrop())
        {
            return;
        }

        AudioManager.Instance.Drop();
        controlledFruit.ActivateMe();
        CreateFruit();
        //Invoke(nameof(CreateFruit), minSecondsBetweenSpawns/2);
    }

    private void UpdateFruitPosition(Vector2 touchWorldPosition)
    {
        if(GameManager.Instance.IsTutorial())
        {
            return;
        }

        float absOffset = controlledFruit.gameObject.transform.localScale.x / 2;
        float x = GameManager.Instance.ClampSpawnX(touchWorldPosition.x, absOffset);

        controlledFruit.transform.position = new Vector2(x, controlledFruit.transform.position.y);
        LineRenderer line = controlledFruit.GetComponent<LineRenderer>();
        line.SetPosition(0, controlledFruit.transform.position);
        line.SetPosition(1, new Vector3(controlledFruit.transform.position.x, ThemeManager.Instance.GetBoxBottomY()));
        lastDropPosition = controlledFruit.transform.position;
    }

    public void CreateFruit()
    {
        // Randomly select a fruit prefab and instantiate it at the spawner's position
        int randomIndex = Random.Range(0, fruitPrefabs.Length);
        float spawnX = lastDropPosition == null ? transform.position.x : lastDropPosition.x;

        Vector3 spawnPos = new Vector2(spawnX, transform.position.y + spawnOffsetY);
        if (nextFruit == null)
        {   
            GameObject fruitGameObject = Instantiate(fruitPrefabs[randomIndex], spawnPos , Quaternion.identity);
            controlledFruit = fruitGameObject.GetComponent<Fruit>();
        }
        else
        {
            controlledFruit = nextFruit;
            controlledFruit.gameObject.SetActive(true);
            controlledFruit.transform.position = spawnPos;
        }
        controlledFruit.GetComponent<FruitIdleShake>().enabled = true;
        controlledFruit.transform.localScale = Vector3.zero;

        controlledFruit.transform.DOScale(controlledFruit.GetTargetScale(), 0.5f);
        controlledFruit.transform.GetChild(1).gameObject.SetActive(true);
        controlledFruit.transform.parent = transform;

        if(!tutorialFruitCreated)
        {
            tutorialFruitCreated = true;
            bool isTutorial = GameManager.Instance.IsTutorial();
            if (isTutorial)
            {
                GameObject fruitGameObject = Instantiate(fruitPrefabs[randomIndex], transform.position, Quaternion.identity);
                fruitGameObject.GetComponent<Fruit>().ActivateMe();
                AudioManager.Instance.Drop();
            }
        }
        
        randomIndex = Random.Range(0, fruitPrefabs.Length);
        
        GameObject nextGameObject = Instantiate(fruitPrefabs[randomIndex], spawnPos, Quaternion.identity);
        nextGameObject.SetActive(false);
        nextFruit = nextGameObject.GetComponent<Fruit>();
        nextFruitImage.sprite = nextGameObject.GetComponent<SpriteRenderer>().sprite;
        nextFruitImage.color = nextGameObject.GetComponent<SpriteRenderer>().color;

        
    }
}