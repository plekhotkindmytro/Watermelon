using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip[] musicTracks; // Array of music tracks
    public AudioSource audioSource; // AudioSource for playing the music

    private int[] shuffleOrder; // Stores the shuffled order of tracks
    private int currentTrackIndex = -1; // Index of the current track

    void Start()
    {
        if (musicTracks.Length == 0 || audioSource == null)
        {
            Debug.LogError("Music tracks or AudioSource not assigned!");
            return;
        }

        ShuffleTracks();
        PlayNextTrack();
    }

    void Update()
    {
        // Check if the current track has finished playing
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            PlayNextTrack();
        }
    }

    void ShuffleTracks()
    {
        // Create a shuffled order of track indices
        shuffleOrder = new int[musicTracks.Length];
        for (int i = 0; i < shuffleOrder.Length; i++)
        {
            shuffleOrder[i] = i;
        }

        // Fisher-Yates shuffle algorithm
        for (int i = shuffleOrder.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = shuffleOrder[i];
            shuffleOrder[i] = shuffleOrder[randomIndex];
            shuffleOrder[randomIndex] = temp;
        }

        currentTrackIndex = -1; // Reset the index for a new shuffle cycle
    }

    void PlayNextTrack()
    {
        currentTrackIndex++;

        // If we've gone through all tracks, shuffle again and start over
        if (currentTrackIndex >= shuffleOrder.Length)
        {
            ShuffleTracks();
            currentTrackIndex = 0;
        }

        // Play the next track
        int trackIndex = shuffleOrder[currentTrackIndex];
        audioSource.clip = musicTracks[trackIndex];
        audioSource.Play();
    }
}
