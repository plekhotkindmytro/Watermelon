using UnityEngine;

public class MenuParticleSpawner : MonoBehaviour
{

    public GameObject[] effects;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // TODO remove raycast. Spawn only general
           
            foreach(Touch touch in Input.touches)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                foreach (var effect in effects)
                {
                    SpawnParticles(effect, touchPosition);
                }
            }

        }

    }
        private void SpawnParticles(GameObject particlePrefab, Vector2 position)
        {
            Instantiate(particlePrefab, position, Quaternion.identity);
        }
    
}
