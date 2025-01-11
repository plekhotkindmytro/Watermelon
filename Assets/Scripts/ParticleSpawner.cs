using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner Instance;

    public GameObject generalParticles;
    public GameObject[] tapBursts;

    public GameObject[] awesomeBursts;
    public string[] awesomeTexts;
    public AudioClip[] awesomeSpeeches;

    public GameObject[] fruitParticles;
    public GameObject squareParticlePrefab;
    private Camera mainCamera;

    // Add more as needed, or use a dictionary if you have many fruit types
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;   
    }
    private void Update()
    {
        if (UiManager.Instance.settingsPanel.activeSelf)
        {
            return;
        }

        if (GameManager.Instance.gameOver)
        {
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // TODO remove raycast. Spawn only general
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Check if the tapped object is a fruit
                Fruit fruit = hit.collider.GetComponent<Fruit>();

                if (fruit != null)
                {
                    // Spawn specific particles based on fruit ID
                    SpawnParticles(generalParticles, touchPosition);
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

                foreach (var effect in tapBursts)
                {
                    SpawnParticles(effect, touchPosition);
                }
                
            }
        }
    }

    public void SpawnFruitParticles(Fruit fruit)
    {
        GameObject particlesToSpawn = fruitParticles[0];
        particlesToSpawn.transform.localScale = fruit.GetTargetScale();
        MainModule mainParticle = particlesToSpawn.GetComponent<ParticleSystem>().main;
        mainParticle.startColor = fruit.color;

        Instantiate(particlesToSpawn, fruit.transform.position, Quaternion.identity);
    }

    public void SpawnSquareParticle(Fruit fruit)
    {

        //MainModule mainParticle = squareParticlePrefab.GetComponent<ParticleSystem>().main;
        //   mainParticle.startColor = fruit.color;
        //   squareParticlePrefab.transform.localScale = Vector3.one * 2.5f;
        Vector3 newPos = Vector3.up*2f; // Vector3.zero
        Vector3 newScreenPos = mainCamera.WorldToScreenPoint(newPos);
        Instantiate(squareParticlePrefab, newPos, Quaternion.identity);
        //Vector3 screenPos = mainCamera.WorldToScreenPoint(Vector3.zero);
        FloatingTextManager.Instance.SpawnFloatingText(newScreenPos, awesomeTexts[Random.Range(0, awesomeTexts.Length)], true);
        foreach (var effect in awesomeBursts)
        {
            SpawnParticles(effect, newPos);
        }
    }

    private void SpawnParticles(GameObject particlePrefab, Vector2 position)
    {
        Instantiate(particlePrefab, position, Quaternion.identity);
    }
}
