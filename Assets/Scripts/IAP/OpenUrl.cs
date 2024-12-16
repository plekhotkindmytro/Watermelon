using UnityEngine;
using UnityEngine.UI;

public class OpenUrl : MonoBehaviour
{
    public string url = "";

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OpenURL);
        }
    }

    void OpenURL()
    {
        Application.OpenURL(url);
    }
}
