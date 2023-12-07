using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu_script : MonoBehaviour
{
    public void play() {
        int cur = PlayerPrefs.GetInt("Cutscene", 1);
        if(cur == 1) {
            SceneManager.LoadScene("Cut 1");
        } else {
            SceneManager.LoadScene("Level Selection");
        }
    }
    public void options() {
        SceneManager.LoadScene("Settings");
    }
    public void exit() {
        Application.Quit();
    }
}
