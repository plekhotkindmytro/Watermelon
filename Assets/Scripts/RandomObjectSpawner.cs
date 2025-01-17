using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{

    public GameObject mousePrefab;
    public GameObject fishPrefab;
    public GameObject beePrefab;


    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = new Vector2(mainCamera.orthographicSize * mainCamera.aspect, mainCamera.orthographicSize);
        InvokeRepeating(nameof(SpawnMouse), Random.Range(1,4), Random.Range(6, 10));
        InvokeRepeating(nameof(SpawnBee), Random.Range(3, 5), Random.Range(4, 9));
        InvokeRepeating(nameof(SpawnFish), Random.Range(5, 8), Random.Range(6, 12));

       
    }
    

    public void SpawnFish()
    {
        
       // float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomY = Random.Range(-screenBounds.y + fishPrefab.transform.localScale.y / 2, screenBounds.y - fishPrefab.transform.localScale.y / 2);
        
        Vector2 pos = new Vector2(-mainCamera.orthographicSize * mainCamera.aspect - fishPrefab.transform.localScale.x,
            randomY);
        GameObject fish = Instantiate(fishPrefab, pos, Quaternion.identity, transform);
        fish.SetActive(true);
    }

    public void SpawnMouse()
    {
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        //float randomY = Random.Range(-screenBounds.y, screenBounds.y);
        Vector2 pos = new Vector2(randomX,
            -mainCamera.orthographicSize - mousePrefab.transform.localScale.y);
        GameObject mouse = Instantiate(mousePrefab, pos, Quaternion.identity, transform);
        mouse.SetActive(true);
    }
    public void SpawnBee()
    {
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        Vector2 pos = new Vector2(randomX, mainCamera.orthographicSize + beePrefab.transform.localScale.y);
        GameObject bee = Instantiate(beePrefab, pos, Quaternion.identity, transform);
        bee.SetActive(true);
    }

   
}
