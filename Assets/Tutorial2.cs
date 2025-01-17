using UnityEngine;

public class Tutorial2 : MonoBehaviour
{

    public void HideOrShow()
    {
        gameObject.SetActive(GameManager.Instance.IsTutorial2());
    }
}
