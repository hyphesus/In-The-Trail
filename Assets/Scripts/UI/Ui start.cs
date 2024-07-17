using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uistart : MonoBehaviour
{
    public GameObject settingsPanel;
    public Camera uiCamera;
    public Camera playerCamera;
    public GameObject sceneaudio;
    public Scrollbar scrollbar;
    void Start()
    {
        sceneaudio.GetComponent<MusicPlayer>().audioSource.volume = scrollbar.value*0;
        playerCamera.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        // Deactivate UI camera and activate player camera
        uiCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void ChangeAudio(){
        sceneaudio.GetComponent<MusicPlayer>().audioSource.volume = scrollbar.value*0.4f;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
