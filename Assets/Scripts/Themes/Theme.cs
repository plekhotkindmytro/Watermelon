using UnityEngine;

[System.Serializable]
public class Theme
{

    public Sprite backgroundSprite;
    public Color bgSpriteColor;

    public GameObject[] fruits;
    
    public float backgroundImageScale;

    public GameObject boxPrefab;
    public float spawnerLocalPosY = 0.6f;

}
