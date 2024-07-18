using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StopUI : MonoBehaviour
{
    public GameObject sceneaudio;
    public Scrollbar scrollbar;
    public GameObject gameState; // player.ispaused
    // Start is called before the first frame update
    public void ContinueGame(){
        print("continue");
        gameState.GetComponent<PlayerController>().Resume();
    }

    public void ChangeAudio(){
        if (sceneaudio.GetComponent<MusicPlayer>() != null){
            sceneaudio.GetComponent<MusicPlayer>().audioSource.volume = scrollbar.value*0.4f;
        }
        else if(sceneaudio.GetComponent<DesertCityMusic>() != null){
            sceneaudio.GetComponent<DesertCityMusic>().audioSource.volume = scrollbar.value*0.4f;
        }
    }
}
