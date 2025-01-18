using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public AudioSource blackHoleAudio;
    public GameObject twinBlackHole;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.PlayerTag))
        {

            blackHoleAudio.Play();
            collision.gameObject.transform.position = twinBlackHole.transform.position;
            this.gameObject.SetActive(false);
            twinBlackHole.SetActive(false);

            twinBlackHole.GetComponent<BlackHole>().Invoke("RestoreMe", 5);
            Invoke("RestoreMe", 5);
        }
    }

    private void RestoreMe()
    {
        this.gameObject.SetActive(true);
    }

}