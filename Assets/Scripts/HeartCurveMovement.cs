using UnityEngine;

public class HeartCurveMovement : MonoBehaviour
{
    public Transform center; // Center of the curve
    public float scale = 1f; // Scale of the curve
    public float speed = 1f; // Speed of movement

    private float t = 0f;

    void Update()
    {
        t += speed * Time.deltaTime;

        float x = center.position.x + scale * 16 * Mathf.Pow(Mathf.Sin(t), 3);
        float y = center.position.y + scale * (13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t));

      
        transform.position = new Vector2(x, y);

        if (t > Mathf.PI * 2)
        {
            t -= Mathf.PI * 2;
        }
    }
}
