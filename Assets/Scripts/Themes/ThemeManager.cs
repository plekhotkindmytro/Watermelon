using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    public Image backgroundImage;
    public float backgroundImageScale;
    public Image backgroundImageForScreenshot;
    public SpriteRenderer boxSpriteRenderer;
    public SpriteRenderer boxFrameSpriteRenderer;
    public GameObject bottomArrowFruitsWrapper;
    public Theme[] themes;
    public Spawner spawner;

    private Theme activeTheme;

    private static readonly string THEME_KEY = "theme";

    void Start()
    {
        activeTheme = themes[PlayerPrefs.GetInt(THEME_KEY, 0)];


        float aspectRatio = activeTheme.backgroundSprite.rect.width / activeTheme.backgroundSprite.rect.height;
        backgroundImage.sprite = activeTheme.backgroundSprite;
        backgroundImage.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
        

        backgroundImageForScreenshot.sprite = activeTheme.backgroundSprite;
        backgroundImageForScreenshot.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
        

        float screenAspectRatio = (float)Screen.width / Screen.height;
        // Check if the aspect ratio is close to an iPad's aspect ratio (typically ~4:3)
        if (screenAspectRatio >= 0.625f)
        {
            backgroundImage.transform.localScale = Vector3.one * backgroundImageScale;
            backgroundImageForScreenshot.transform.localScale = Vector3.one * backgroundImageScale;
        }

        boxSpriteRenderer.sprite = activeTheme.boxSprite;
        boxFrameSpriteRenderer.sprite = activeTheme.boxFrameSprite;

        Image[] fruitSpriteRenderersInArrow = bottomArrowFruitsWrapper.GetComponentsInChildren<Image>();
        for(int i = 0; i<fruitSpriteRenderersInArrow.Length; i++)
        {
            fruitSpriteRenderersInArrow[i].sprite = activeTheme.fruits[i].GetComponent<SpriteRenderer>().sprite;
        }

        Array.Copy(activeTheme.fruits, spawner.fruitPrefabs, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
