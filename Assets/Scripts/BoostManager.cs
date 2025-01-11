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

    public int mouseCount;
    public int beeCount;
    public int fishCount;

    public int defaultFishCount = 15;
    public int defaultBeeCount = 10;
    public int defaultMouseCount = 5;
    public readonly static string  FISH_COUNT_KEY = "fishCount";
    public readonly static string  BEE_COUNT_KEY = "beeCount";
    public readonly static string  MOUSE_COUNT_KEY = "mouseCount";
   

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
        fishCount = PlayerPrefs.GetInt(FISH_COUNT_KEY, defaultFishCount);
        beeCount = PlayerPrefs.GetInt(BEE_COUNT_KEY, defaultBeeCount);
        mouseCount = PlayerPrefs.GetInt(MOUSE_COUNT_KEY, defaultMouseCount);

        UiManager.Instance.fishCountText.text = fishCount.ToString();
        UiManager.Instance.beeCountText.text = beeCount.ToString();
        UiManager.Instance.mouseCountText.text = mouseCount.ToString();

    }

    public void SpawnFish()
    {
        if(fishCount == 0) 
        {
            // Show IAP panel
            // buyFishPanel.SetActive(true)
            UiManager.Instance.OpenShopPanel();
            return;
        }

        Vector2 pos = new Vector2(-mainCamera.orthographicSize * mainCamera.aspect - fishPrefab.transform.localScale.x / 2,
            ThemeManager.Instance.GetBoxBottomY() + 0.5f);
        GameObject fish = Instantiate(fishPrefab, pos, Quaternion.identity, transform);
        fish.SetActive(true);

        
        fishCount--;
        PlayerPrefs.SetInt(FISH_COUNT_KEY, fishCount);
        UiManager.Instance.fishCountText.text = fishCount.ToString();
    }

    public void SpawnMouse()
    {
        if(mouseCount == 0) 
        {
            // Show IAP panel
            // buyFishPanel.SetActive(true)
            UiManager.Instance.OpenShopPanel();
            return;
        }

        float x = GameManager.Instance.RandomSpawnX();
        Vector2 pos = new Vector2(x,
            -mainCamera.orthographicSize - mousePrefab.transform.localScale.y/2);
        GameObject mouse = Instantiate(mousePrefab, pos, Quaternion.identity, transform);
        mouse.SetActive(true);

        mouseCount--;
        PlayerPrefs.SetInt(MOUSE_COUNT_KEY, mouseCount);
        UiManager.Instance.mouseCountText.text = mouseCount.ToString();
    }
    public void SpawnBee()
    {
        if(beeCount == 0) 
        {
            // Show IAP panel
            // buyFishPanel.SetActive(true)
            UiManager.Instance.OpenShopPanel();
            return;
        }

        float x = GameManager.Instance.RandomSpawnX();
        Vector2 pos = new Vector2(x, mainCamera.orthographicSize + beePrefab.transform.localScale.y / 2);
        GameObject bee = Instantiate(beePrefab, pos, Quaternion.identity, transform);
        bee.SetActive(true);

        beeCount--;
        PlayerPrefs.SetInt(BEE_COUNT_KEY, beeCount);
        UiManager.Instance.beeCountText.text = beeCount.ToString();
    }
    

    public void TryAddBoost(int level)
    {
        if(level == 9) {
            AddFish(1);
        } else if(level == 10) {
            AddBee(1);
        } else if(level == 11) {
            AddMouse(1);
        }
    }

    public void AddMouse(int amount)
    {
        mouseCount += amount;
        PlayerPrefs.SetInt(MOUSE_COUNT_KEY, mouseCount);
        UiManager.Instance.mouseCountText.text = mouseCount.ToString();
    }

    public void AddBee(int amount)
    {
        beeCount += amount;
        PlayerPrefs.SetInt(BEE_COUNT_KEY, beeCount);
        UiManager.Instance.beeCountText.text = beeCount.ToString();
    }

    public void AddFish(int amount)
    {
        fishCount += amount;
        PlayerPrefs.SetInt(FISH_COUNT_KEY, fishCount);
        UiManager.Instance.fishCountText.text = fishCount.ToString();
    }
}
