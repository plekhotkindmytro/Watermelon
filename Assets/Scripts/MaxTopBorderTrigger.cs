using UnityEngine;

public class MaxTopBorderTrigger : MonoBehaviour
{

    public float overflowTime = 2f;
    public float gameOverTime = 5f;

    private float timeElapsed = 0f;
    private GameObject child;
    private int counter = 0;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    public void Update()
    {
        bool showWarningBorder = counter > 0 && timeElapsed >= overflowTime;
        child.SetActive(showWarningBorder);

        bool showGameOver = counter > 0 && timeElapsed >= gameOverTime;
        if(showGameOver)
        {
            child.SetActive(false);
            GameManager.Instance.GameOver();
            return;
        } 
        

        if (counter == 0)
        {
            timeElapsed = 0;
        } else
        {
            timeElapsed += Time.deltaTime;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        counter++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        counter--;
    }
}
