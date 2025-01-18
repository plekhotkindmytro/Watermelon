using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Cloud : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float fadeSpeed = 2f;
    public float scaleSpeed = 5f;
    public GameObject lootPrefab;
    public float offsetRange = 1f;
    private bool isSpawned = false;
    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.PlayerTag))
        {
            spriteRenderer.DOFade(0, fadeSpeed).SetEase(Ease.Linear).OnComplete(()=> this.gameObject.SetActive(false));
            this.transform.DOScale(0, scaleSpeed).SetEase(Ease.Linear);

            SpawnLoot();
        }
    }

    private void SpawnLoot()
    {
        if(isSpawned)
        {
            return;
        }

        if (Random.Range(0, 2) == 0)
        {
            float x = this.gameObject.transform.position.x;
            float y = this.gameObject.transform.position.y;

            GameObject loot = Instantiate(lootPrefab, null, true);
            float randomOffsetX = Random.Range(-offsetRange, offsetRange);
            float randomOffsetY = Random.Range(-offsetRange, offsetRange);
            loot.transform.position = new Vector2(x + randomOffsetX, y + randomOffsetY);

            isSpawned = true;
        }
    }
}
