using UnityEngine;

public class StarCurveMovement : MonoBehaviour
{
    public Transform center; // Center of the curve
    public float radius = 5f; // Radius of the star
    public float speed = 1f; // Speed of movement
    public int numPoints = 5; // Number of points of the star

    private float angle = 0f;

    void Update()
    {
        // Calculate the position along the star curve
        angle += speed * Time.deltaTime; // Increment the angle over time
        float r = Mathf.Cos(numPoints * angle) * radius;
        float x = center.position.x + r * Mathf.Cos(angle);
        float y = center.position.y + r * Mathf.Sin(angle);

        // Update the object's position
        transform.position = new Vector2(x, y);

        // Ensure the angle stays within 0 to 2*PI
        if (angle > Mathf.PI * 2)
        {
            angle -= Mathf.PI * 2; 
        }
    }
}
