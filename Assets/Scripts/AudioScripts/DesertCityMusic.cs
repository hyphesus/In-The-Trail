using UnityEngine;
using UnityEngine.UI;

public class DesertCityMusic : MonoBehaviour
{
    public AudioClip gameMusic; // Assign your game music audio clip in the inspector
    public float delay = 5f; // Delay time in seconds before the song plays again
    public float fadeDuration = 2f; // Duration of the fade in/out effect in seconds

    public AudioSource audioSource;
    private bool isWaiting = false;
    private bool isFadingIn = true;
    private bool isFadingOut = false;
    private float waitTime = 0f;
    private float initialVolume;
    public Scrollbar scrollbar;

    void Start()
    {
        // Add an AudioSource component to the GameObject this script is attached to
        audioSource = gameObject.AddComponent<AudioSource>();
        
        // Assign the AudioClip to the AudioSource
        audioSource.clip = gameMusic;
        
        // Set initial volume and start with fade-in
        initialVolume = scrollbar.value;
        audioSource.volume = 0;

        // Play the audio
        audioSource.Play();
    }

    void Update()
    {
        // Handle fade-in effect
        if (isFadingIn)
        {
            audioSource.volume += initialVolume * (Time.deltaTime / fadeDuration);
            if (audioSource.volume >= initialVolume)
            {
                audioSource.volume = initialVolume;
                isFadingIn = false;
            }
        }

        // Check if the audio source has finished playing and is not already waiting
        if (!audioSource.isPlaying && !isWaiting && !isFadingOut)
        {
            isFadingOut = true;
        }

        // Handle fade-out effect
        if (isFadingOut)
        {
            audioSource.volume -= initialVolume * (Time.deltaTime / fadeDuration);
            if (audioSource.volume <= 0)
            {
                audioSource.volume = 0;
                isFadingOut = false;
                isWaiting = true;
                waitTime = delay;
            }
        }

        // If waiting, count down the delay
        if (isWaiting)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                isWaiting = false;
                isFadingIn = true;
                audioSource.Play();
            }
        }
    }
}
