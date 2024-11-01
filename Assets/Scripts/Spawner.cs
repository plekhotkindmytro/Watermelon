using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Image nextFruitImage;
    public List<GameObject> fruitPrefabs;  // List of all 11 fruit prefabs

    private Fruit controlledFruit = null;
    private Fruit nextFruit = null;
    
    private Camera cameraMain;


    public void Start()
    {
        cameraMain = Camera.main;
    }

    private void Update()
    {
        if(UiManager.Instance.settingsPanel.activeSelf)
        {
            return;
        }

        if(controlledFruit == null)
        {
            SpawnFruit();
        } else
        {
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
                        float absOffset = controlledFruit.gameObject.transform.localScale.x / 2;
                        float x = GameManager.Instance.ClampSpawnX(touchWorldPos.x, absOffset);
                        
                        controlledFruit.transform.position = new Vector2(x, controlledFruit.transform.position.y);
                    }
                    else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                    {
                        AudioManager.Instance.Drop();
                        controlledFruit.ActivateMe();
                        controlledFruit = null;
                    }
                }
            
        }
    }

    private void SpawnFruit()
    {
        // Randomly select a fruit prefab and instantiate it at the spawner's position
        int randomIndex;

        if(nextFruit == null)
        {
            randomIndex = Random.Range(0, fruitPrefabs.Count);
            GameObject fruitGameObject = Instantiate(fruitPrefabs[randomIndex], transform.position, Quaternion.identity);

            controlledFruit = fruitGameObject.GetComponent<Fruit>();
        } else
        {
            controlledFruit = nextFruit;
            controlledFruit.gameObject.SetActive(true);
        }
        

        // next
        randomIndex = Random.Range(0, fruitPrefabs.Count);
        GameObject nextGameObject = Instantiate(fruitPrefabs[randomIndex], transform.position, Quaternion.identity);
        nextGameObject.SetActive(false);
        nextFruit = nextGameObject.GetComponent<Fruit>();
        nextFruitImage.sprite = nextGameObject.GetComponent<SpriteRenderer>().sprite;

    }
} 