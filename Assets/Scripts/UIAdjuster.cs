using UnityEngine;

public class UIAdjuster : MonoBehaviour
{
    
    public float iPadYOffset = 100f; // Offset to apply on iPads

    private void Start()
    {
        AdjustUIForDevice();
    }

    private void AdjustUIForDevice()
    {
        RectTransform uiElement = GetComponent<RectTransform>();
        float aspectRatio = (float)Screen.width / Screen.height;

        // Check if the aspect ratio is close to an iPad's aspect ratio (typically ~4:3)
        if (aspectRatio >= 0.75f)
        {
            // Apply the offset to position it higher on iPad
            Vector2 position = uiElement.anchoredPosition;
            position.y += iPadYOffset;
            uiElement.anchoredPosition = position;
        }
    }
}
