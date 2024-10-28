using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<GameObject> fruitPrefabs;  // List of all 11 fruit prefabs


    private Fruit controlledFruit = null;
    private bool dropped = false;
    private Camera cameraMain;

    public void Start()
    {
        cameraMain = Camera.main;
    }

    private void Update()
    {
        if(controlledFruit == null)
        {
            SpawnFruit();
        } else
        {
             if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                    {
                        controlledFruit.transform.position =
                        new Vector2(cameraMain.ScreenToWorldPoint(touch.position).x, controlledFruit.transform.position.y);
                    }
                    else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                    {
                        controlledFruit.ActivateMe();
                        controlledFruit = null;
                    }
                }
            
        }
    }

    private void SpawnFruit()
    {
        // Randomly select a fruit prefab and instantiate it at the spawner's position
        int randomIndex = Random.Range(0, GameManager.Instance.maxFruitLevel);
        GameObject fruitGameObject = Instantiate(fruitPrefabs[randomIndex], transform.position, Quaternion.identity);
        controlledFruit = fruitGameObject.GetComponent<Fruit>();

        
    }
} 