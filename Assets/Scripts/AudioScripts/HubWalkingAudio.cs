using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWalking : MonoBehaviour
{
    public AudioClip walkingAudio; // Reference to the AudioSource component
    public PlayerController playerController;
    public float volume = 1.0f; // Volume control variable
    private AudioSource walkingAudioSource;

    void Start()
    {
        walkingAudioSource = gameObject.AddComponent<AudioSource>();

        // Assign the AudioClip to the AudioSource
        walkingAudioSource.clip = walkingAudio;

        // Set the volume to the initial value
        walkingAudioSource.volume = volume;

        walkingAudioSource.loop = true; // Set the audio to loop
    }

    // Update is called once per frame
    void Update()
    {
        // Update the volume in case it changes in the editor
        walkingAudioSource.volume = volume;

        if (playerController.isMoving)
        {
            if (!walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Play();
            }
        }
        else
        {
            if (walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Stop();
            }
        }
    }
}
