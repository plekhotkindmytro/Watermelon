using UnityEngine;

public class LissajousCurveMovement : MonoBehaviour
{
    public Transform center;
    public float A = 5f;
    public float B = 5f;
    public float a = 3f;
    public float b = 2f;
    public float delta = Mathf.PI / 2; // Phase difference
    public float speed = 1f;

    private float t = 0f;

    void Update()
    { 
        t += speed * Time.deltaTime;

        float x = center.position.x + A * Mathf.Sin(a * t + delta);
        float y = center.position.y + B * Mathf.Sin(b * t);

        transform.position = new Vector2(x, y);
    }
}
