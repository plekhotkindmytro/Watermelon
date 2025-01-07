using UnityEngine;
public class Tutorial : MonoBehaviour
{

    public GameObject handWrapper;
    void Start()
    {
        if(GameManager.Instance.IsTutorial())
        {
            Invoke(nameof(Show), 1.7f);
        } else
        {
            gameObject.SetActive(false);
        }
        
    }

    private void Show()
    {
        handWrapper.SetActive(true);
    }    
}
