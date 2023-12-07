using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class setting_script : MonoBehaviour
{
    public Button b;

    void Start() {
        int cur = PlayerPrefs.GetInt("Cutscene", 1);
        if(cur == 1) {
            b.GetComponentInChildren<TMP_Text>().text = "Skip Narative";
        } else {
            b.GetComponentInChildren<TMP_Text>().text = "Enable Narative";
        }
    }

    
    void Update() {
        int cur = PlayerPrefs.GetInt("Cutscene", 1);
        // Debug.Log(cur);
        if(cur == 1) {
            b.GetComponentInChildren<TMP_Text>().text = "Skip Narative";
        } else {
            b.GetComponentInChildren<TMP_Text>().text = "Enable Narative";
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Level Selection");
    }

    public void UnMute() {
        if(BGmusic.instance.isOn) {
            BGmusic.instance.GetComponent<AudioSource>().Pause();
            BGmusic.instance.isOn = false;
        } else {
            BGmusic.instance.GetComponent<AudioSource>().Play();
            BGmusic.instance.isOn = true;
        }
    }

    public void UnlockAll() {
        PlayerPrefs.SetInt("highestLevel", 11);
    }

    public void ResetProgress() {
        PlayerPrefs.DeleteKey("highestLevel");
    }

    public void DisableCutscene() {
        int cur = PlayerPrefs.GetInt("Cutscene", 1);
        PlayerPrefs.SetInt("Cutscene", 1 - cur);
    }

}
