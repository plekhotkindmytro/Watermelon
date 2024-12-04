using UnityEngine;

public class InfinityCurveMovement : MonoBehaviour
{
    public Transform center; // Center of the curve
    public float scale = 5f; // Scale of the curve
    public float speed = 1f; // Speed of movement

    private float t = 0f;

    void Update()
    {
        // Increment the parameter over time
        t += speed * Time.deltaTime;

        // Calculate the position along the infinity curve
        float x = center.position.x + scale * Mathf.Sin(t);
        float y = center.position.y + scale * Mathf.Sin(t) * Mathf.Cos(t);

        // Update the object's position
        transform.position = new Vector2(x, y);

        // Ensure the parameter t stays within bounds
        if (t > Mathf.PI * 2)
        {
            t -= Mathf.PI * 2;
        }
    }
}
 