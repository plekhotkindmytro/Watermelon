using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
public class Spawner : MonoBehaviour
{
    public Image nextFruitImage;
    public List<GameObject> fruitPrefabs;  // List of all 11 fruit prefabs
    public float minSecondsBetweenSpawns = 1f;
    public float spawnOffsetY = 10;


    private Fruit controlledFruit = null;
    private Fruit nextFruit = null;



    private Camera cameraMain;


    private float timeElapsed = 0;


    public void Start()
    {
        cameraMain = Camera.main;
        timeElapsed = minSecondsBetweenSpawns;
        
        //CameraManager.Instance.Follow(controlledFruit.transform);
    }

    private void Update()
    {

        if (UiManager.Instance.settingsPanel.activeSelf)
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

            if (touch.position.y > Screen.height * 0.8)
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
        float absOffset = controlledFruit.gameObject.transform.localScale.x / 2;
        float x = GameManager.Instance.ClampSpawnX(touchWorldPosition.x, absOffset);

        controlledFruit.transform.position = new Vector2(x, controlledFruit.transform.position.y);
        LineRenderer line = controlledFruit.GetComponent<LineRenderer>();
        line.SetPosition(0, controlledFruit.transform.position);
        line.SetPosition(1, new Vector3(controlledFruit.transform.position.x, GameManager.Instance.GetBoxBottomY()));
        
    }

    public void CreateFruit()
    {
        // Randomly select a fruit prefab and instantiate it at the spawner's position
        int randomIndex;
        Vector3 spawnPos = transform.position + Vector3.up * spawnOffsetY;
        if (nextFruit == null)
        {
            randomIndex = Random.Range(0, fruitPrefabs.Count);
            
            GameObject fruitGameObject = Instantiate(fruitPrefabs[randomIndex], spawnPos , Quaternion.identity);

            controlledFruit = fruitGameObject.GetComponent<Fruit>();
        }
        else
        {
            controlledFruit = nextFruit;
            controlledFruit.gameObject.SetActive(true);
        }
        controlledFruit.GetComponent<FruitIdleShake>().enabled = true;
        controlledFruit.transform.localScale = Vector3.zero;

        controlledFruit.transform.DOScale(controlledFruit.GetTargetScale(), 0.5f);
        controlledFruit.transform.GetChild(1).gameObject.SetActive(true);

        // next
        randomIndex = Random.Range(0, fruitPrefabs.Count);
        GameObject nextGameObject = Instantiate(fruitPrefabs[randomIndex], spawnPos, Quaternion.identity);
        nextGameObject.SetActive(false);
        nextFruit = nextGameObject.GetComponent<Fruit>();
        nextFruitImage.sprite = nextGameObject.GetComponent<SpriteRenderer>().sprite;
        
    }
}