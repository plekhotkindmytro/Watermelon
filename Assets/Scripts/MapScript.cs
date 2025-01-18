using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject mapCamera;
    public GameObject player;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mapCamera.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
            {
           //     Time.timeScale = 1;
                mapCamera.SetActive(false);
                gameCamera.SetActive(true);
                player.SetActive(true);
            }
        }
         else
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                gameCamera.SetActive(false);
                mapCamera.SetActive(true);
                player.SetActive(false);
                //     Time.timeScale = 0;
            }
        }  
    }
}
