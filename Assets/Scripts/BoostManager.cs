using System;
using TMPro;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public static BoostManager Instance;

    public GameObject fishPrefab;


    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        mainCamera = Camera.main;
    }




    public void SpawnFish()
    {
        Vector2 pos = new Vector2(-mainCamera.orthographicSize * mainCamera.aspect - fishPrefab.transform.localScale.x / 2,
            GameManager.Instance.boxBottomCollider.position.y+ GameManager.Instance.boxBottomCollider.lossyScale.y/2+1);
        GameObject fish = Instantiate(fishPrefab, pos, Quaternion.identity, transform);
        fish.SetActive(true);
    }

}
