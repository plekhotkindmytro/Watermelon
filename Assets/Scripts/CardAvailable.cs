using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAvailable : MonoBehaviour
{
    public static readonly string LEVEL_REACHED_KEY = "levelReached";
    // Start is called before the first frame update
    void Start()
    {

        int siblingIndex = transform.GetSiblingIndex() - 1; // because first is random
        int levelReached = PlayerPrefs.GetInt(LEVEL_REACHED_KEY);
        
        bool isActiveCard = siblingIndex <= levelReached;


        // enable/disable components 
        transform.GetChild(2).gameObject.SetActive(isActiveCard);
        GetComponent<CardAnimation>().enabled = isActiveCard;
        GetComponent<Button>().enabled = isActiveCard;
    }

}
