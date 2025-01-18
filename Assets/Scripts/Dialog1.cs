using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog1 : MonoBehaviour
{
    public GameObject[] parts;

    public GameObject[] nextToShow;
    public GameObject[] nextToHide;
    private int index = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            parts[index].SetActive(false);
            index++;

            if(index >= parts.Length)
            {
                if (nextToHide != null)
                {
                    for (int i = 0; i < nextToHide.Length; i++)
                    {
                        nextToHide[i].SetActive(false);
                    }
                }

                if (nextToShow != null)
                {
                    for (int i = 0; i < nextToShow.Length; i++)
                    {
                        nextToShow[i].SetActive(true);
                    }
                }

                this.gameObject.SetActive(false);
            } else
            {
                parts[index].SetActive(true);
            }
        }
    }
}
