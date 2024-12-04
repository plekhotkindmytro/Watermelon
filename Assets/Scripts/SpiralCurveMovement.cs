using System;
using UnityEngine;

public class SpiralCurveMovement : MonoBehaviour
{
    public Transform center;
    public float a = 0.1f; // Controls the spacing of the spiral
    public float speed = 1f;

    private float angle = 0f;
    public bool grow = true;
    private float maxX; 

    private Camera cameraMain;
    private void Start() {
        cameraMain = Camera.main; 
        maxX = cameraMain.aspect * cameraMain.orthographicSize - 1;
    }

    void Update()
    {

        if(grow) {
            angle += speed * Time.deltaTime;
        } else {
            angle -= speed * Time.deltaTime;
        }
        


        float r = a * angle;
        

        
        float x = center.position.x + r * Mathf.Cos(angle);
        float y = center.position.y + r * Mathf.Sin(angle);
        if(grow && r > maxX) {
            grow = false;
        }
        
        if(!grow && r <= 0) {
            grow = true;
        }
        transform.position = new Vector2(x, y);
    }
}
 