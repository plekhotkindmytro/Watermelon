using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeAudio : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 3f;
    private AudioSource audioSource;

    public AudioClip firstTimeWelcomeClip;
    public AudioClip[] welcomeBackClips;

    private static readonly string FIRST_TIME_PLAYER_KEY = "firstTimePlaying";
    private bool isFirstTimePlaying = true;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        isFirstTimePlaying = PlayerPrefs.GetInt(FIRST_TIME_PLAYER_KEY, 0) == 0;


        Invoke(nameof(SayWelcome), Random.Range(minDelay, maxDelay));
        PlayerPrefs.SetInt(FIRST_TIME_PLAYER_KEY, 1);
    }


    private void SayWelcome()
    {
        audioSource.PlayOneShot(isFirstTimePlaying ? firstTimeWelcomeClip : welcomeBackClips[Random.Range(0, welcomeBackClips.Length)]);
    }
}
