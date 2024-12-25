using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    public Image backgroundImage;
    public Image backgroundImageForScreenshot;
    public SpriteRenderer boxSpriteRenderer;
    public SpriteRenderer boxFrameSpriteRenderer;
    public GameObject bottomArrowFruitsWrapper;
    public Theme[] themes;
    public Spawner spawner;
    public BoxScaler boxScaler;

    private Theme activeTheme;

    public static readonly string THEME_KEY = "theme";

    void Start()
    {

        int themeIndex = PlayerPrefs.GetInt(THEME_KEY, 0);
        print("Active scene index: " + themeIndex);
        activeTheme = themes[themeIndex];
        

        float aspectRatio = activeTheme.backgroundSprite.rect.width / activeTheme.backgroundSprite.rect.height;
        backgroundImage.sprite = activeTheme.backgroundSprite;
        backgroundImage.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
        

        backgroundImageForScreenshot.sprite = activeTheme.backgroundSprite;
        backgroundImageForScreenshot.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
        

        float screenAspectRatio = (float)Screen.width / Screen.height;
        // Check if the aspect ratio is close to an iPad's aspect ratio (typically ~4:3)
        if (screenAspectRatio >= 0.625f)
        {
            backgroundImage.transform.localScale = Vector3.one * activeTheme.backgroundImageScale;
            backgroundImageForScreenshot.transform.localScale = Vector3.one * activeTheme.backgroundImageScale;
        }

        boxSpriteRenderer.sprite = activeTheme.boxSprite;
        boxFrameSpriteRenderer.sprite = activeTheme.boxFrameSprite;

        Image[] fruitSpriteRenderersInArrow = bottomArrowFruitsWrapper.GetComponentsInChildren<Image>();
        for(int i = 0; i<fruitSpriteRenderersInArrow.Length; i++)
        {
            fruitSpriteRenderersInArrow[i].sprite = activeTheme.fruits[i].GetComponent<SpriteRenderer>().sprite;
        }

        Array.Copy(activeTheme.fruits, spawner.fruitPrefabs, 6);

        boxScaler.ScaleBox();
        spawner.CreateFruit();
    }

   
}
