using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopUI : MonoBehaviour
{
    public GameObject sceneaudio;
    public Scrollbar scrollbar;
    public GameObject mainMenu;
    public GameObject gameState; // player.ispaused
    // Start is called before the first frame update
    public void ContinueGame(){
        gameState.GetComponent<PlayerController>().Resume();
    }

    public void ChangeAudio(){
        sceneaudio.GetComponent<MusicPlayer>().audioSource.volume = scrollbar.value*0.4f;
    }
}
