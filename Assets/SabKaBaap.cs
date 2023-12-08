using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SabKaBaap : MonoBehaviour
{
    // Start is called before the first frame update
    public int i = 1;
    public bool get_level = true;

    [SerializeField] private LevelManager _lvl_manager;
    [SerializeField] private int[] widths = {5,5,7,7,9,8,4,8};
    [SerializeField] private int[] heights = {5,5,7,7,9,8,8,8};
    [SerializeField] private int[] abcd = {4,0,23,0,36,0,0,0};
    [SerializeField] private int[] _ends = {20,4,24,48,76,1,31,63};
    // private int[,] moves = {
    //     {1,0,-1,0,0,1,0,-1},
    //     {1,1,-1,1,0,-1},
    //     {0,1,-1,0,1,-1},
    //     {-1,0,0,-1,1,0,2,0,0,1,1,1,2,1,0,2,1,2,2,2},
    //     {2,1,1,1,1,-2},
    //     {-1,1,1,1,1,-1,-1,-1,0,-1},
    //     {0,2,0,-1,0,-2,-1,0,1,0},
    //     {0,1,0,2,0,-1,0,-2,1,0,2,0,-1,0,-2,0},
    // };
    [SerializeField] private int[][] moves = new int[][]
    {
        new int[] {1,0,-1,0,0,1,0,-1},
        new int[] {1,1,-1,1,0,-1},
        new int[] {0,1,-1,0,1,-1},
        new int[] {-1,0,0,-1,1,0,2,0,0,1,1,1,2,1,0,2,1,2,2,2},
        new int[] {-2,1,1,1,1,-2},
        new int[] {-1,1,1,1,1,-1,-1,-1,0,-1},
        new int[] {0,2,0,-1,0,-2,-1,0,1,0},
        new int[] {0,1,0,2,0,-1,0,-2,1,0,2,0,-1,0,-2,0},
    };
    [SerializeField] private int[] scores = {32,32,27,35,21,32,27,52};
    

    void Start()
    {
        if (get_level)
        {
            i = PlayerPrefs.GetInt("cur_level", 1);
        }
        Debug.Log($"Playing level {i}");

        int x = abcd[i-1];
        Debug.Log($"x = {x}");
        LevelManager levelManager = Instantiate(_lvl_manager, Vector3.zero, Quaternion.identity);
        levelManager.Init(widths[i-1], heights[i-1], x, _ends[i-1], moves[i-1], scores[i-1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}