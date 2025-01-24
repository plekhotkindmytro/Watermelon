using UnityEngine;

public class Hand2 : MonoBehaviour
{

    public float speed = 5;
    private float maxX;
    private float minX;

    private int direction = 1;

    
    private void Start()
    {
        maxX = Screen.width / 2 + Screen.width / 8;
        minX = Screen.width / 2 - Screen.width / 8;
    }
    private void Update()
    {
        
        transform.position += Vector3.right * Time.deltaTime * speed * direction;

        if(transform.position.x >= maxX)
        {
            direction = -1;
        }

        if (transform.position.x <= minX)
        {
            direction = 1;
        }
    }
}
