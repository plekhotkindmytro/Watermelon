using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public float minRotationSpeed = 60;
    public float maxRotationSpeed = 360;
    private int direction;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minRotationSpeed, maxRotationSpeed);
        direction = Random.Range(0,1) == 0? -1 : 1;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, direction*speed*Time.deltaTime);
    }
}
