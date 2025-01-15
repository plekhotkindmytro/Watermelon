using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwesomeAudioSpeechManager : MonoBehaviour
{
    public static AwesomeAudioSpeechManager Instance;
    private AudioSource audioSource; // Assign your AudioSource in the inspector
    private List<AudioClip> audioClips; // Assign your audio clips in the inspector or dynamically
    private Queue<AudioClip> clipQueue = new Queue<AudioClip>();
    private bool isPlaying = false;

 private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start() 
    {
        audioClips = new List<AudioClip>();
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsPlaying() {
        return isPlaying;
    }
    
    public void PlayNextClip(AudioClip newClip)
    {
       clipQueue.Enqueue(newClip);

        if (!isPlaying)
        {
            StartCoroutine(PlayClipsFromQueue());
        }
    }

    private IEnumerator PlayClipsFromQueue()
    {
        isPlaying = true;

        while (clipQueue.Count > 0)
        {
            AudioClip clipToPlay = clipQueue.Dequeue();
            audioSource.clip = clipToPlay;
            audioSource.Play();

            yield return new WaitForSeconds(clipToPlay.length); // Wait until the clip is finished
        }

        isPlaying = false;
    }
}
