using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HomePlanetExplosion : MonoBehaviour
{

    public GameObject[] nextToShow;
    public GameObject[] nextToHide;

    public GameObject wavePrefab;
    
    public GameObject homePlanet;
    public float boomScale = 5f;
    public float boomTime = 3f;
    private bool isBoom = false;

    private float timeElapsed = 0;
    public float timerInSeconds = 1f;
    public AudioSource startExplosion;

    // Start is called before the first frame update
    void Awake()
    {


        transform.localScale = Vector3.zero;


    }

    private void Boom()

    {
        Instantiate(wavePrefab, this.transform, true);
        startExplosion.Play();
        transform.DOScale(boomScale, boomTime).SetEase(Ease.Linear).OnComplete(() => {
            
            homePlanet.GetComponent<SpriteRenderer>().color = new Color32(15, 15, 15, 255);
            for (int i = 0; i < homePlanet.transform.childCount; i++)
            {
                homePlanet.transform.GetChild(i).gameObject.SetActive(true);
            }
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
            transform.gameObject.SetActive(false);

        });
    }

    private void Update()
    {
        if(isBoom)
        {
            return;
        }

        timeElapsed += Time.deltaTime;

        if((timeElapsed > timerInSeconds) || (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            Boom();
            isBoom = true;
        }
    }

}
