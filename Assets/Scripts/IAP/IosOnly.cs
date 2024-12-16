using UnityEngine;
using UnityEngine.UI;

public class IosOnly : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer);
    }
}
 