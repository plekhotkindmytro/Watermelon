using UnityEngine;
using UnityEngine.UI;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager Instance;
    public Camera bucketCamera;            // Assign a camera that sees only the bucket
    public Image gameOverImage;
    public PopupPanel popupPanel;

    private RenderTexture renderTexture;
    private Texture2D screenshotTexture;
    public float screenYOffset;
    public int boxWidth;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CaptureBucketScreenshot()
    {
        // Set up RenderTexture with the bucket camera's size
        renderTexture = new RenderTexture(boxWidth, Screen.height, 24);
        bucketCamera.targetTexture = renderTexture;

        // Render the bucket camera view to the RenderTexture
        bucketCamera.Render();

        // Copy the RenderTexture data into a Texture2D
        RenderTexture.active = renderTexture;
        screenshotTexture = new Texture2D(renderTexture.width, renderTexture.width, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, screenYOffset, renderTexture.width, renderTexture.width), 0, 0);
        screenshotTexture.Apply();

        
        // Cleanup RenderTexture
        bucketCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Set the screenshot to the game-over panel's image
        gameOverImage.sprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), new Vector2(0.5f, 0.5f));
       
       
    }

    public void SaveToPhotos()
    {
        byte[] rawTextureData = screenshotTexture.GetRawTextureData();
        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(rawTextureData, "Fruit Merge - Watermelon", "fruit_merge_watermelon.png", (success, path) => {


            if (success)
            {
                popupPanel.Activate("Saved to Photos");
                //ParticleSystem particle = Instantiate(particleSystemPrefab);
                //if (soundEffect.isPlaying)
                //{
                //    soundEffect.Stop();
                //}
                //soundEffect.Play();
                //sun.SetActive(true);
            }
            else
            {
                popupPanel.Activate("Error");
            }
           ;
        });

        if (permission == NativeGallery.Permission.Denied)
        {
            popupPanel.Activate("Permission Denied");
        }

        Debug.Log("Permission result: " + permission);
    }

}
