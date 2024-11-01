using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject generalParticles; // Assign general particles prefab
    public GameObject[] fruitParticles;

    // Add more as needed, or use a dictionary if you have many fruit types

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Check if the tapped object is a fruit
                Fruit fruit = hit.collider.GetComponent<Fruit>();

                if (fruit != null)
                {
                    // Spawn specific particles based on fruit ID
                    SpawnFruitParticles(fruit.fruitLevel, touchPosition);
                }
                else
                {
                    // Spawn general particles if not a fruit
                    SpawnParticles(generalParticles, touchPosition);
                }
            }
            else
            {
                // Spawn general particles if tap is on empty space
                SpawnParticles(generalParticles, touchPosition);
            }
        }
    }

    private void SpawnFruitParticles(int level, Vector2 position)
    {
        GameObject particlesToSpawn = fruitParticles[level - 1];
        Instantiate(particlesToSpawn, position, Quaternion.identity);
    }

    private void SpawnParticles(GameObject particlePrefab, Vector2 position)
    {
        Instantiate(particlePrefab, position, Quaternion.identity);
    }
}
