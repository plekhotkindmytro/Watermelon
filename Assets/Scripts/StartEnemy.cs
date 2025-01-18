using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemy : MonoBehaviour
{
    public float speed = 5f;

    public GameObject nextToShow;

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        transform.position += Vector3.down * speed/3 * Time.deltaTime;
        transform.localScale -= Vector3.one * speed/7 * Time.deltaTime;
        if(transform.position.x >= 1.5)
        {
            nextToShow.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
