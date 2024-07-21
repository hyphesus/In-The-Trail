using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector component
    public GameObject playerCamera; // Reference to the player camera
    public GameObject timelineCamera; // Reference to the timeline camera
    public string sceneName; // Name of the scene with the Timeline
    public GameObject ninjaTemp;
    public AudioSource audioSource;

    private void Start()
    {
        // Check if the current scene is the scene with the Timeline
        if (sceneName == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            PlayTimeline();
        }
    }

    public void PlayTimeline()
    {
        playerCamera.SetActive(false);
        timelineCamera.SetActive(true);
        ninjaTemp.SetActive(true);
        audioSource.Play();
        playableDirector.Play();
        playableDirector.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        timelineCamera.SetActive(false);
        playerCamera.SetActive(true);
        ninjaTemp.SetActive(false);
    }
}
