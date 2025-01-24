using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEventManager : MonoBehaviour
{
    
    public float repeatRate;
    public GameObject[] catPrefabs;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCat), repeatRate, repeatRate);       
    }

    public void SpawnCat() 
    {

        if (GameManager.Instance.IsGamePaused())
        {
            return;
        }

        int index = Random.Range(0, catPrefabs.Length);
        Camera cam = MainCameraReference.MainCamera;
        float maxX = cam.orthographicSize*cam.aspect;
        float x = Random.Range(-maxX, maxX);
        float y = cam.orthographicSize + catPrefabs[index].transform.localScale.y;

        Vector2 spawnPos =  new Vector2(x, y);
        Instantiate(catPrefabs[index], spawnPos, Quaternion.identity);

    }
}
