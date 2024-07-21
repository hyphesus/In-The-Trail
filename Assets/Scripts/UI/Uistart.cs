using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Uistart : MonoBehaviour
{
    public GameObject settingsPanel;
    public Camera uiCamera;
    public AudioSource audioSource;

    void Start()
    {
        Cursor.visible = true;

        // Play the audio source and set it to loop
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StartGame()
    {
        // Deactivate UI camera and activate player camera
        Cursor.visible = false;
        uiCamera.gameObject.SetActive(false);
        StartCoroutine(LoadSceneAsync("MainGame"));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        // Wait until the asynchronous scene fully loads
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
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
