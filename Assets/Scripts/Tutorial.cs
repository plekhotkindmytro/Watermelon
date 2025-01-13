using UnityEngine;
public class Tutorial : MonoBehaviour
{

    public GameObject handWrapper;

    public void HideOrShow()
    {
        gameObject.SetActive(GameManager.Instance.IsTutorial());
    }    
}
