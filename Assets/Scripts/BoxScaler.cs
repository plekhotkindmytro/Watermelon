using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScaler : MonoBehaviour
{
    
    private Camera mainCamera;
    private float worldWidth;
    private float worldHeight;

    void Start()
    {
        mainCamera = Camera.main;
        worldWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
        worldHeight = mainCamera.orthographicSize * 2;

        float scaleFactor = worldWidth < mainCamera.orthographicSize? worldWidth : mainCamera.orthographicSize;
        Vector3 localScale = new Vector3(scaleFactor, worldHeight/2, 1);
        transform.localScale = localScale;

        transform.position = new Vector3(0, 0, 0);
    }

}
