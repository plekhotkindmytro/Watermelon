using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public List<Sprite> spriteList;
    public GameObject cloudPrefab;
    public int tileCount = 100;
    public int step = 5;

    void Awake()
    {
        for (int i = -tileCount; i < tileCount; i+=step)
        {
            for (int j = -tileCount; j < tileCount; j+= step)
            {

                GameObject cloud = Instantiate(cloudPrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
                cloud.GetComponent<SpriteRenderer>().sprite = spriteList[Random.Range(0, spriteList.Count)];
            }

        }
        
    }

    
}
