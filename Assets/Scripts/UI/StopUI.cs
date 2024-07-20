using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StopUI : MonoBehaviour
{
    public GameObject sceneaudio;
    public Scrollbar scrollbar;
    public GameObject gameState; // player.ispaused
    // Start is called before the first frame update
    public Toggle toggle;

    void Start()
    {
        toggle.isOn = gameState.GetComponent<PlayerController>().easyMode;
    }
    public void ContinueGame()
    {
        print("continue");
        gameState.GetComponent<PlayerController>().Resume();
    }

    public void ChangeAudio()
    {
        if (sceneaudio.GetComponent<MusicPlayer>() != null)
        {
            sceneaudio.GetComponent<MusicPlayer>().audioSource.volume = scrollbar.value * 0.4f;
        }
        else if (sceneaudio.GetComponent<DesertCityMusic>() != null)
        {
            sceneaudio.GetComponent<DesertCityMusic>().audioSource.volume = scrollbar.value * 0.4f;
        }
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void EasyMode()
    {
        if (toggle.isOn)
        {
            gameState.GetComponent<PlayerController>().easyMode = true;
            gameState.GetComponent<PlayerController>().speed = 0.05f;
            Debug.Log("Easy Mode is on, speed is 0.05");
        }
        else
        {
            gameState.GetComponent<PlayerController>().easyMode = false;
            gameState.GetComponent<PlayerController>().speed = 0.04f;
            Debug.Log("Easy Mode is off, speed is 0.04");
        }
    }
}
