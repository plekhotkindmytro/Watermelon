using System;
using TMPro;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public static BoostManager Instance;

    public GameObject fishPrefab;
    public GameObject mousePrefab;
    public GameObject flyPrefab;
    public GameObject beePrefab;
    public GameObject mosquitePrefab;


    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        mainCamera = Camera.main;
    }




    public void SpawnFish()
    {
        Vector2 pos = new Vector2(-mainCamera.orthographicSize * mainCamera.aspect - fishPrefab.transform.localScale.x / 2,
            GameManager.Instance.boxBottomCollider.position.y+ GameManager.Instance.boxBottomCollider.lossyScale.y/2+0.5f);
        GameObject fish = Instantiate(fishPrefab, pos, Quaternion.identity, transform);
        fish.SetActive(true);
    }

    public void SpawnMouse()
    {
        // float offset = 0.5f;
       
        float x = GameManager.Instance.RandomSpawnX();
        Vector2 pos = new Vector2(x,
            -mainCamera.orthographicSize - mousePrefab.transform.localScale.y/2);
        GameObject mouse = Instantiate(mousePrefab, pos, Quaternion.identity, transform);
        mouse.SetActive(true);
    }
    public void SpawnBee()
    {

        float x = GameManager.Instance.RandomSpawnX();
        Vector2 pos = new Vector2(x, mainCamera.orthographicSize + beePrefab.transform.localScale.y / 2);
        GameObject bee = Instantiate(beePrefab, pos, Quaternion.identity, transform);
        bee.SetActive(true);
    }
    public void SpawnFly()
    {
        
    }
    public void SpawnMosquito() 
    {
       
    }

}
