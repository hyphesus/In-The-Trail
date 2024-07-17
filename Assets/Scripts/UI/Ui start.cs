using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uistart : MonoBehaviour
{
    public GameObject settingsPanel;
    public Camera uiCamera;
    public Camera playerCamera;

    void Start()
    {
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
