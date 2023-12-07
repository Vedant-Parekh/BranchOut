using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class completion_script : MonoBehaviour
{

    public GameObject stars;
    public Image[] star_images;
    public Image Active;
    // Start is called before the first frame update
    void Start()
    {
        int cur_level = PlayerPrefs.GetInt("cur_level", 1);
        PlayerPrefs.SetInt("highestLevel", (cur_level + 1));
        StarsToArray();
        int recieved = PlayerPrefs.GetInt("num_stars", 0);
        PlayerPrefs.SetInt("stars_" + cur_level, recieved);
        Debug.Log("Received: " + recieved);
        Debug.Log("cur_level: " + cur_level);
        Debug.Log("PP: " + PlayerPrefs.GetInt("stars_" + cur_level, 100));
        for(int i = 0; i < recieved; i++) {
            star_images[i].GetComponent<Image>().sprite = Active.sprite;
        }
    }

    
    public void NextLevel() {
        int cur_level = PlayerPrefs.GetInt("cur_level", 1);
        PlayerPrefs.SetInt("cur_level", cur_level + 1);
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadLevelPage() {
        SceneManager.LoadScene("Level Selection");
    }

    public void RepeatLevel() {
        int cur_level = PlayerPrefs.GetInt("cur_level", 1);
        SceneManager.LoadScene("SampleScene");
    }

    void StarsToArray() {
        star_images = new Image[3];
        for(int i = 0; i < 3; i++) {
            star_images[i] = stars.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }
}
