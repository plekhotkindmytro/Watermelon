using UnityEngine;
using DG.Tweening;
using System;

public class BoxScaler : MonoBehaviour
{

    public GameObject left;
    public GameObject right;
    public GameObject bottom;
    public GameObject frame;
    public MaxTopBorderTrigger maxTopBorderTrigger;



   // public float from = 20f;
    public float duration = 1f;
    public float contentDelay = 0.2f;
    private AudioSource sound;


    private Camera mainCamera;
    private float worldWidth;
    private float worldHeight;
    private float boxBottomY;

    

    public void ScaleBox()
    {
        mainCamera = Camera.main;
        worldWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
        worldHeight = mainCamera.orthographicSize * 2;

        float nonBoxWorldSize = 4;
        float maxBoxWidth = worldHeight - nonBoxWorldSize;
        float scaleFactor = worldWidth < maxBoxWidth? worldWidth : maxBoxWidth;
        Vector3 localScale = new Vector3(scaleFactor, scaleFactor, 1);
        transform.localScale = localScale;

        float boxTableOffset = 4;

        float aspectRatio = (float)Screen.width / Screen.height;

        var pos = new Vector3(0, transform.localScale.y / 1.48f - boxTableOffset, 0);

        // Check if the aspect ratio is close to an iPad's aspect ratio (typically ~4:3)
        if (aspectRatio < 0.625f)
        {
            pos += Vector3.up;
        }

        transform.position = pos;


        ScreenshotManager.Instance.screenYOffset = (Screen.height / (mainCamera.orthographicSize * 2)) * (mainCamera.orthographicSize + transform.position.y - transform.localScale.y / 2);
        ScreenshotManager.Instance.boxWidth = Mathf.RoundToInt((Screen.width / worldWidth) * (scaleFactor));

        float minX = left.transform.position.x + left.transform.lossyScale.x / 2;
        float maxX = right.transform.position.x - right.transform.lossyScale.x / 2;
        GameManager.Instance.SetMinSpawnX(minX);
        GameManager.Instance.SetMaxSpawnX(maxX);
        boxBottomY = bottom.transform.position.y + bottom.transform.lossyScale.y / 2;
        BoxAppearAnimation();

    }


    private void BoxAppearAnimation()
    {
        sound = GetComponent<AudioSource>();
        float delay = duration - sound.clip.length;
        sound.PlayDelayed(delay > 0 ? delay : 0);

        float targetPosY = transform.position.y;
        float startPosY = 30f;

        transform.position += Vector3.up*startPosY;
        transform.DOMoveY(targetPosY, duration).OnComplete(() => {
            SaveManager.Instance.LoadData();
            ThemeManager.Instance.spawner.CreateFruit();
        });
    }

    internal float GetBoxBottomY()
    {
        return boxBottomY;
    }
}
