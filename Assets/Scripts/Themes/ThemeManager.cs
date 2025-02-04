using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance;

    [Header("Canvas Objects")]
    public Image backgroundImage;
    public Image backgroundImageForScreenshot;
    public GameObject bottomArrowFruitsWrapper;

    [Header("Themes")]
    public Theme[] themes;

    [Header("Spawner")]
    public Spawner spawner;

    

    private Theme activeTheme;

    public static readonly string THEME_KEY = "theme";

    private BoxScaler boxScaler;
    //private float boxBottomY;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ApplyTheme();
    }
    private void ApplyTheme()
    {

        int themeIndex = PlayerPrefs.GetInt(THEME_KEY);
        activeTheme = themes[themeIndex];


        SetBackground();
        SetBox();

        SetSpawner();

        
    }

    private void SetBackground()
    {
        float aspectRatio = activeTheme.backgroundSprite.rect.width / activeTheme.backgroundSprite.rect.height;
        backgroundImage.sprite = activeTheme.backgroundSprite;
        backgroundImage.color = activeTheme.bgSpriteColor;
        backgroundImage.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;


        backgroundImageForScreenshot.sprite = activeTheme.backgroundSprite;
        backgroundImageForScreenshot.color = activeTheme.bgSpriteColor;
        backgroundImageForScreenshot.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;

        float screenAspectRatio = (float)Screen.width / Screen.height;
        // Check if the aspect ratio is close to an iPad's aspect ratio (typically ~4:3)
        if (screenAspectRatio >= 0.625f)
        {
            backgroundImage.transform.localScale = Vector3.one * activeTheme.backgroundImageScale;
            backgroundImageForScreenshot.transform.localScale = Vector3.one * activeTheme.backgroundImageScale;
        }
    }

    private void SetBox()
    {
        GameObject box = Instantiate(activeTheme.boxPrefab);
        boxScaler = box.GetComponent<BoxScaler>();
        boxScaler.ScaleBox();
        
        //boxBottomY = boxScaler.bottom.transform.position.y + boxScaler.bottom.transform.lossyScale.y / 2;

    }

    private void SetSpawner()
    {
        Image[] fruitSpriteRenderersInArrow = bottomArrowFruitsWrapper.GetComponentsInChildren<Image>();
        for (int i = 0; i < 6; i++)
        {
            SpriteRenderer fruitSpriteRenderer = activeTheme.fruits[i].GetComponent<SpriteRenderer>();
            fruitSpriteRenderersInArrow[i].sprite = fruitSpriteRenderer.sprite;
            fruitSpriteRenderersInArrow[i].color = fruitSpriteRenderer.color;
        }

        Array.Copy(activeTheme.fruits, spawner.fruitPrefabs, 6);

        spawner.transform.parent = boxScaler.transform;
        spawner.transform.localPosition = Vector3.up * activeTheme.spawnerLocalPosY;
    }

    public float GetBoxBottomY()
    {
        return boxScaler.GetBoxBottomY();
        //return boxBottomY;
    }

    internal void Warn()
    {
        boxScaler.maxTopBorderTrigger.Warn();
    }

    internal void CancelWarn()
    {
        boxScaler.maxTopBorderTrigger.CancelWarn();
    }
}
