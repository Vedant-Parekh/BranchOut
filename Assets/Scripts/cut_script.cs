using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cut_script : MonoBehaviour
{
    public int cur_scene;
    public void nextScene() {
        if(cur_scene < 9) {
            SceneManager.LoadScene("Cut " + (cur_scene + 1));
        } else {
            SceneManager.LoadScene("Level Selection");
        }
    }
}
