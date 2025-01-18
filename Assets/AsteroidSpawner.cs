using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject player;
    public float spawnTime;

    public GameObject asteroidPrefab;

    public float minBorder = 10;
    public float maxBorder = 25;
    public float spawnOffset = 20f;

    private float timeElapsed;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        bool playerInCircle =
            ((player.transform.position.x > minBorder && player.transform.position.x < maxBorder) &&
            (player.transform.position.y > minBorder && player.transform.position.y < maxBorder))
            ||
            ((player.transform.position.x < minBorder * -1 && player.transform.position.x > maxBorder * -1) &&
            (player.transform.position.y < minBorder * -1 && player.transform.position.y > maxBorder * -1));


        if (playerInCircle && timeElapsed >= spawnTime)
        {
            GameObject asteroid = Instantiate(asteroidPrefab, null, true);
            float x = Random.Range(-spawnOffset, spawnOffset);
            float y = Random.Range(-spawnOffset, spawnOffset);
            asteroid.transform.position = player.transform.position + new Vector3(x,y, 0);
            timeElapsed = 0;
           // asteroid.GetComponent<Asteroid>().SetTarget(player);
        }
    }
}
