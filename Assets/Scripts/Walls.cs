using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    
    private Camera mainCamera;
    private float worldWidth;
    private float worldHeight;
    void Start()
    {

        
        UpdatePosition();
       
    }

    public void UpdatePosition() 
    {
        mainCamera = Camera.main;
        worldWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
        worldHeight = mainCamera.orthographicSize * 2;


        GameObject left = transform.GetChild(0).gameObject;
        GameObject right = transform.GetChild(1).gameObject;
        GameObject top = transform.GetChild(2).gameObject;
        GameObject bottom = transform.GetChild(3).gameObject;

        Vector3 localScale = new Vector3(worldWidth, worldHeight, 1);
        left.transform.localScale = localScale;
        right.transform.localScale = localScale;
        top.transform.localScale = localScale;
        bottom.transform.localScale = localScale;

        left.transform.position = new Vector3(-(worldWidth + localScale.x)/2, 0, 0);
        right.transform.position = new Vector3((worldWidth + localScale.x)/2, 0, 0);
        //top.transform.position = new Vector3(0, (worldHeight + localScale.y)/2, 0);
        top.transform.position = new Vector3(0, 100, 0);
        bottom.transform.position = new Vector3(0, -(worldHeight + localScale.y)/2, 0);

    }
    
}
