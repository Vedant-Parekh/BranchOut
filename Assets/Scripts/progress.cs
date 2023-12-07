using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class progress : MonoBehaviour
{
    public Image bar;
    private int highestLevel;
    // Start is called before the first frame update
    void Start()
    {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);

        bar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 oldScale = bar.transform.localScale;
        int progress = 0;
        for(int i = 1; i <= 8; i++) {
            progress += PlayerPrefs.GetInt("stars_" + i, 0);
        }
        oldScale[0] = progress / 24.0f;
        bar.transform.localScale = oldScale;
        // Debug.Log("oldscale {oldScale}");
    }
}
