using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScaler : MonoBehaviour
{

    public GameObject left;
    public GameObject right;
    private Camera mainCamera;
    private float worldWidth;
    private float worldHeight;

    void Start()
    {
        mainCamera = Camera.main;
        worldWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
        worldHeight = mainCamera.orthographicSize * 2;

        float nonBoxWorldSize = 4;
        float maxBoxWidth = worldHeight - nonBoxWorldSize;
        float scaleFactor = worldWidth < maxBoxWidth? worldWidth : maxBoxWidth;
        Vector3 localScale = new Vector3(scaleFactor, scaleFactor, 1);
        transform.localScale = localScale;

        float boxTableOffset = 3;
        transform.position = new Vector3(0,  transform.localScale.y / 2 - boxTableOffset, 0);


        float minX = left.transform.position.x + left.transform.lossyScale.x / 2;
        float maxX = right.transform.position.x - right.transform.lossyScale.x / 2;
        GameManager.Instance.SetMinSpawnX(minX);
        GameManager.Instance.SetMaxSpawnX(maxX);
    }

}
