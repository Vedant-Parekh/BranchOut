using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    public Button[] levelButtons;
    public Image Locked;
    public Image[] Unlocked;
    // public Image Completed;
    public GameObject lvl;

    private int highestLevel;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start" + 5);
        ButtonsToArray();
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);

        for(int i = 0; i < levelButtons.Length; i++) {
            int levelNum = i + 1;
            // Debug.Log(levelNum);
            // Debug.Log(highestLevel);
            if(levelNum > highestLevel) {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = Locked.sprite;
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "";
            } else {
                levelButtons[i].interactable = true;
                int num_stars = PlayerPrefs.GetInt("stars_" + levelNum, 0);
                Debug.Log("Num stars " + num_stars);
                levelButtons[i].GetComponent<Image>().sprite = Unlocked[num_stars].sprite;
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "" + levelNum;
            }
        }
    }

    void Update() {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);

        for(int i = 0; i < levelButtons.Length; i++) {
            int levelNum = i + 1;
            // Debug.Log(levelNum);
            // Debug.Log(highestLevel);
            if(levelNum > highestLevel) {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = Locked.sprite;
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "";
            } else {
                levelButtons[i].interactable = true;
                int num_stars = PlayerPrefs.GetInt("stars_" + levelNum, 0);
                Debug.Log("Num stars " + num_stars);
                levelButtons[i].GetComponent<Image>().sprite = Unlocked[num_stars].sprite;
                levelButtons[i].GetComponentInChildren<TMP_Text>().text = "" + levelNum;
            }
        }

    }

    public void LoadLevel(int levelNum) {
        PlayerPrefs.SetInt("cur_level", levelNum);
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadSettings() {
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    public void Reset() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    void ButtonsToArray() {
        int childCount = lvl.transform.childCount;
        levelButtons = new Button[childCount];

        for(int i = 0; i < childCount; i++) {
            levelButtons[i] = lvl.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
