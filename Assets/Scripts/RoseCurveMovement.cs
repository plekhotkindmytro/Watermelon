using UnityEngine;

public class RoseCurveMovement : MonoBehaviour
{
    public Transform center;
    public float radius = 5f;
    public float speed = 1f;
    public int k = 4; // Number of petals

    private float angle = 0f;

    void Update()
    {
        angle += speed * Time.deltaTime;

        float r = radius * Mathf.Cos(k * angle);
        float x = center.position.x + r * Mathf.Cos(angle);
        float y = center.position.y + r * Mathf.Sin(angle);

        transform.position = new Vector2(x, y);

        if (angle > Mathf.PI * 2)
        {
            angle -= Mathf.PI * 2;
        }
    }
}
