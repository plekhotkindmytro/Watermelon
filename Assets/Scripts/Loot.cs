using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameObject[] types;

    public Sprite[] stars;
    public float offsetRange;
    public GameObject starPrefab;
    public int maxStars = 5;

    public bool isBodyPart;
    public bool isHealth;
    public bool isFuel;
    public bool isMoney;

    public float lootDistance = 0.5f;

    private int lootIndex;

    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        // loot
        
            lootIndex = Random.Range(0, types.Length);
            SpriteRenderer spriteRenderer = types[lootIndex].GetComponent<SpriteRenderer>();
            this.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            this.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
            
      
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;

        int starsCount = Random.Range(0, maxStars);
        // stars
        for (int i = 0; i < maxStars; i++)
        {
            int starIndex = Random.Range(0, stars.Length);
            Sprite starSprite = stars[starIndex];
            GameObject star = Instantiate(starPrefab, null, true);
            star.GetComponent<SpriteRenderer>().sprite = starSprite;
            float randomOffsetX = Random.Range(-offsetRange, offsetRange);
            float randomOffsetY = Random.Range(-offsetRange, offsetRange);
            star.transform.position = new Vector2(x + randomOffsetX, y + randomOffsetY);
            star.transform.localScale = Vector3.one*Random.Range(0.25f, 0.5f);
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            // move to player;
        }
        
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Constants.PlayerTag))
        {
            player = collision.gameObject;
            float distance = Vector2.Distance(player.transform.position, this.gameObject.transform.position);

            if(distance < lootDistance)
            {
                GameController.Instance.AddScore(lootIndex);

                Destroy(this.gameObject);
            }
            // TODO: move to player;
            //if distance less then lootDistance
         //   this.gameObject.SetActive(false);
            // game controller add loot
            // if(BodyPart)
            // change look
            // if Health change something else;
        }

        if (collision.gameObject.CompareTag(Constants.PlanetTag))
        {
            
            Destroy(this.gameObject);
        }
    }
}
