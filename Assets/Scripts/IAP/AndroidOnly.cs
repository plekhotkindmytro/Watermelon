using UnityEngine;

public class AndroidOnly : MonoBehaviour
{
    void Start()
    {   
        gameObject.SetActive(Application.platform == RuntimePlatform.Android);
    }
}
 