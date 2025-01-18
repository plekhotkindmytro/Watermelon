using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainPlanetScript : MonoBehaviour
{
    public int starsNeeded;
    public int keysNeeded;
    public int coinsNeeded;
    public int diamondsNeeded;

    public GameObject resourcePanel;
    public GameObject unlockButton;

    public GameObject beforeUnlock;
    public GameObject afterUnlock;

    public TMP_Text starsText;
    public TMP_Text keysText;
    public TMP_Text coinsText;
    public TMP_Text diamondsText;

    private bool unlocked = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag(Constants.PlayerTag))
        {
            starsText.text = starsNeeded.ToString();
            keysText.text = keysNeeded.ToString();
            coinsText.text = coinsNeeded.ToString();
            diamondsText.text = diamondsNeeded.ToString();
            resourcePanel.SetActive(true);
            if (CanUnlock())
            {
                unlockButton.SetActive(true);
            }
        }

        
    }

    public void UnlockPlanet()
    {
        if(unlocked)
        {
            return;
        }
        GameController.Instance.EnterPlanet(starsNeeded, keysNeeded, coinsNeeded, diamondsNeeded);
        starsNeeded = keysNeeded = coinsNeeded = diamondsNeeded = 0;
        unlocked = true;
        beforeUnlock.SetActive(false);
        afterUnlock.SetActive(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        this.GetComponent<SpriteRenderer>().color = Color.white;

    }


    private bool CanUnlock()
    {
        if(unlocked)
        {
            return false;
        }

        int stars = GameController.Instance.stars;
        int keys = GameController.Instance.keys;
        int coins = GameController.Instance.coins;
        int diamonds = GameController.Instance.diamonds;

        if(starsNeeded > stars || keysNeeded > keys ||
            coinsNeeded > coins || diamondsNeeded > diamonds)
        {
            return false;
        }

        return true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PlayerTag))
        {
            resourcePanel.SetActive(false);
        }
    }

}
