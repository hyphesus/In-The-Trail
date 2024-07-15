using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip gameMusic; // Assign your game music audio clip in the inspector
    public float loopStartTime = 10f; // Start time of the loop segment in seconds
    public float loopEndTime = 20f; // End time of the loop segment in seconds

    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component to the GameObject this script is attached to
        audioSource = gameObject.AddComponent<AudioSource>();

        // Assign the AudioClip to the AudioSource
        audioSource.clip = gameMusic;

        // Play the audio
        audioSource.Play();
    }

    void Update()
    {
        // Check if the audio source has reached the loop end time
        if (audioSource.time >= loopEndTime)
        {
            // Set the audio source time to the loop start time
            audioSource.time = loopStartTime;
        }
    }
}
