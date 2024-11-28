using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorizeGems : MonoBehaviour
{


    public GameObject gems;
    private SpriteRenderer[] spriteRenderers;

    void Start()
    {
        spriteRenderers = gems.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Colorize()
    {
        Color newColor = Random.ColorHSV(0,1, 0, 1, 1, 1);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = newColor;
        }
    }

    public void MainScene() {

        SceneManager.LoadScene("SampleScene");
    }
}
