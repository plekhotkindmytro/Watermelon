using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int pointValue;
    public int fruitLevel;
    public GameObject nextFruitPrefab;  // Prefab for the next-level fruit after merging

    private bool hasMerged = false;     // Flag to prevent chain reactions


    public void Awake()
    {
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

   

   
    public void ActivateMe()
    {
        
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged)
        {
            return;
        }


        if (nextFruitPrefab == null)
        {
            return;
        }

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if (otherFruit != null && otherFruit.fruitLevel == fruitLevel && !otherFruit.hasMerged)
        {
            hasMerged = true;
            otherFruit.hasMerged = true;
            Merge(otherFruit);
        }
    }

    private void Merge(Fruit otherFruit)
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        otherFruit.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        // Spawn the next level fruit at the merge position
        Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2;
        GameObject fruitGameObject = Instantiate(nextFruitPrefab, mergePosition, Quaternion.identity);
        Fruit fruit = fruitGameObject.GetComponent<Fruit>();
        Destroy(otherFruit.gameObject);
        GameManager.Instance.AddScore(pointValue);
        fruit.ActivateMe();
        
        Destroy(gameObject);
    }
}
