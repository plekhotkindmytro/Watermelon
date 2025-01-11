using UnityEngine;
public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;
    private Vector2 offset;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset = new Vector2(Time.time * scrollSpeed, 0);
        material.mainTextureOffset = offset;
    }
}
