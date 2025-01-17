using UnityEngine;
public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;

    private float repositionPoint;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        repositionPoint = 18.764f/2f;
    }

    void Update()
    {
        transform.position -= Vector3.up * scrollSpeed * Time.deltaTime;
        if(transform.position.y <= -repositionPoint)
        {
            transform.position = new Vector2(0, 18.764f);
            spriteRenderer.sortingOrder = -3;
        }

        if (transform.position.y <= repositionPoint)
        {
            spriteRenderer.sortingOrder = -2;
        }

        if (transform.position.y <= 0)
        {
            spriteRenderer.sortingOrder = -1;
        }
    }
}
