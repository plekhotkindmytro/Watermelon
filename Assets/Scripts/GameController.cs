using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public AudioClip starLoot;
    public AudioClip keyLoot;
    public AudioClip coinLoot;
    public AudioClip diamondLoot;
    public int stars;
    public int keys;
    public int coins;
    public int diamonds;
    public TMP_Text currentPlanetsText;
    public int planetCount = 7;

    private int planetsRestored = 0;


    public TMP_Text starsText;
    public TMP_Text keysText;
    public TMP_Text coinsText;
    public TMP_Text diamondsText;

    public static GameController Instance = null;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if(Instance != this)
        {
            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);
        
    }

    public void AddScore(int lootType)
    {
       if(lootType == 0)
       {
            AudioSource.PlayClipAtPoint(starLoot, player.transform.position);
            stars++;
            
        }
        else if (lootType == 1)
        {
            AudioSource.PlayClipAtPoint(keyLoot, player.transform.position);
            keys++;
        }
        else if (lootType == 2)
        {
            AudioSource.PlayClipAtPoint(coinLoot, player.transform.position);
            coins++;
        }
        else if (lootType == 3)
        {
            AudioSource.PlayClipAtPoint(diamondLoot, player.transform.position);
            diamonds++;
        }


        UpdateUI();
    }

    internal void EnterPlanet(int starsNeeded, int keysNeeded, int coinsNeeded, int diamondsNeeded)
    {
        stars -= starsNeeded;
        keys -= keysNeeded;
        coins -= coinsNeeded;
        diamonds -= diamondsNeeded;
        UpdateUI();
        planetsRestored++;
        currentPlanetsText.text = (planetsRestored).ToString();
        if(planetsRestored >= planetCount)
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void UpdateUI()
    {
        starsText.text = stars.ToString();
        keysText.text = keys.ToString();
        coinsText.text = coins.ToString();
        diamondsText.text = diamonds.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
