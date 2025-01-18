using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed = 5f;
  

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * speed*Time.deltaTime;
        if(transform.localScale.x > 500)
        {
            Destroy(this.gameObject);
        }
    }
}
