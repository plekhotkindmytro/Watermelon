using UnityEngine;
using UnityEngine.UI;
public class MaxTopBorderTrigger : MonoBehaviour
{

    private GameObject child;
    
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);
    }


    public void Warn()
    {
        child.SetActive(true);
    }

    public void CancelWarn()
    {
        child.SetActive(false);
    }

    //private void SetImageOpacity(float opacity)
    //{
    //    Color color = warningPanel.color;
    //    color.a = Mathf.Clamp01(opacity); // Ensure the opacity is within the range 0-1
    //    color.a /= 2;
    //    warningPanel.color = color;
    //}

    //public void Update()
    //{
    //    bool showWarningBorder = counter > 0 && timeElapsed >= overflowTime;
    //    child.SetActive(showWarningBorder);

    //    float warningOpacity = timeElapsed / overflowTime > 0.8f ? timeElapsed / overflowTime : 0;
    //    SetImageOpacity(warningOpacity);

    //    bool showGameOver = counter > 0 && timeElapsed >= gameOverTime;
    //    if(showGameOver)
    //    {
    //        child.SetActive(false);
    //        GameManager.Instance.GameOver();
    //        gameObject.SetActive(false);
    //        return;
    //    } 


    //    if (counter == 0)
    //    {
    //        timeElapsed = 0;
    //    } else
    //    {
    //        timeElapsed += Time.deltaTime;
    //    }

    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    counter++;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    counter--;
    //}
}
