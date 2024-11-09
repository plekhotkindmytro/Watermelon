using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Image nextFruitImage;
    public List<GameObject> fruitPrefabs;  // List of all 11 fruit prefabs
    public float minSecondsBetweenSpawns = 1f;


    private Fruit controlledFruit = null;
    private Fruit nextFruit = null;

    private Camera cameraMain;


    private float timeElapsed = 0;


    public void Start()
    {
        cameraMain = Camera.main;
        timeElapsed = minSecondsBetweenSpawns; // first fruit can be dropped instantly.
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

        if (controlledFruit == null)
        {
            CreateFruit();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            Vector2 touchWorldPos = cameraMain.ScreenToWorldPoint(touch.position);
            if (touchWorldPos.y > 4)
            {
                return;
            }

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
        if(!CanDrop())
        {
            return;
        }

        AudioManager.Instance.Drop();
        controlledFruit.ActivateMe();
        controlledFruit = null;
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

    private void CreateFruit()
    {
        // Randomly select a fruit prefab and instantiate it at the spawner's position
        int randomIndex;

        if (nextFruit == null)
        {
            randomIndex = Random.Range(0, fruitPrefabs.Count);
            GameObject fruitGameObject = Instantiate(fruitPrefabs[randomIndex], transform.position, Quaternion.identity);

            controlledFruit = fruitGameObject.GetComponent<Fruit>();
        }
        else
        {
            controlledFruit = nextFruit;
            controlledFruit.gameObject.SetActive(true);
        }
        controlledFruit.GetComponent<FruitIdleShake>().enabled = true;

        // next
        randomIndex = Random.Range(0, fruitPrefabs.Count);
        GameObject nextGameObject = Instantiate(fruitPrefabs[randomIndex], transform.position, Quaternion.identity);
        nextGameObject.SetActive(false);
        nextFruit = nextGameObject.GetComponent<Fruit>();
        nextFruitImage.sprite = nextGameObject.GetComponent<SpriteRenderer>().sprite;

    }
}