using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAudio : MonoBehaviour
{
    public AudioClip dashAudio; // Reference to the AudioSource component
    public Dash playerController;
    public float volume = 0.1f; // Volume control variable
    private AudioSource dashAudioSource;
    private bool triggeredOnce;
    void Start()
    {
        dashAudioSource = gameObject.AddComponent<AudioSource>();

        // Assign the AudioClip to the AudioSource
        dashAudioSource.clip = dashAudio;

        // Set the volume to the initial value
        dashAudioSource.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the volume in case it changes in the editor
        dashAudioSource.volume = volume;

        if (playerController.isDashing)
        {
            
            if (!dashAudioSource.isPlaying && !triggeredOnce)
            {
                triggeredOnce =true;
                dashAudioSource.Play();
            }
        }
        else{
            triggeredOnce =false;
        }

    }
}