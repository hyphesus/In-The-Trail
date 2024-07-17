using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uistart : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject player;
    public Camera uiCamera;
    public Camera playerCamera;

    void Start()
    {
        Cursor.visible = true;
        playerCamera.gameObject.SetActive(false);
        player.GetComponent<HubWalking>().enabled = false;
    }

    public void StartGame()
    {
        // Deactivate UI camera and activate player camera
        Cursor.visible = false;
        uiCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        player.GetComponent<HubWalking>().enabled = true;
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
