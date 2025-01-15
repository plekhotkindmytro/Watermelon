using UnityEngine;

public class ChooseCategoryAudio : MonoBehaviour
{
    public float delay = 4f;
    public float repeatRate = 6f; 
    private AudioSource audioSource;

    public AudioClip[] chooseCategoryAudioClip;

   
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        InvokeRepeating(nameof(PlayAudio), delay, repeatRate);   
    }

    private void PlayAudio()
    {
        audioSource.PlayOneShot(chooseCategoryAudioClip[Random.Range(0, chooseCategoryAudioClip.Length)]);
    }
}
