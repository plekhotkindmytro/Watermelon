
using UnityEngine;

public class Cat : MonoBehaviour
{
    
    public int points;
    public AudioClip catSound;

    private Camera cam;

    void Start()
    {
        cam = MainCameraReference.MainCamera;
        AudioManager.Instance.PlayClipWithPitch(catSound, 1);
    }

    void Update()
    {
         if (transform.position.y < -cam.orthographicSize - transform.localScale.y)
         {
            Destroy(gameObject);
         }

        if (GameManager.Instance.IsGamePaused())
        {
            return;
        }

        if (Input.touchCount > 0) // For touch or mouse
        {
            DetectTap(Input.GetTouch(0).position);
        }
    }


    private void DetectTap(Vector2 screenPosition)
    {
        // Convert the screen position to world position
        Vector2 worldPosition = cam.ScreenToWorldPoint(screenPosition);

        // Check if this object's Collider2D was tapped
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            OnTapped(screenPosition);
        }
    }

    private void OnTapped(Vector2 screenPosition)
    {
        FloatingTextManager.Instance.SpawnFloatingText(screenPosition, "+" + points.ToString(), false, 0);
        GameManager.Instance.AddScore(points);
        AudioManager.Instance.CatTap();
        Destroy(gameObject);
    }
}
