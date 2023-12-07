using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    private int _boardSize = 9;
    private float _boardRightOffset = -7.5f, _boardTopOffset = -4.5f;

    private int _movementWidth = 5;
    private int _movementSize = 5;

    
    private float _moveRightOffset = 2.5f, _moveTopOffset = -3.75f;


    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private MoveTile _moveTilePrefab;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private MoveManager _moveManager;
    [SerializeField] private Button run, reset;

    [SerializeField] private int start = 0, end = 0;
    [SerializeField] private Text score_text;
    [SerializeField] private int[] _movementArray;


    public void Init(int w, int h, int s, int e, int[] moves, int score)
    {
        _width = w;
        _height = h;
        start = s;
        end = e;
        _movementArray = moves;

        var board = Instantiate(_gridManager, Vector3.zero, Quaternion.identity);
        board.Init(_width, _height, _boardSize, _boardRightOffset, _boardTopOffset, _tilePrefab, start, end, run, reset, score_text, _movementArray, score); 

        var movement_grid = Instantiate(_moveManager, Vector3.zero, Quaternion.identity);
        movement_grid.Init(_movementWidth, _movementWidth, _movementSize, _moveRightOffset, _moveTopOffset, _moveTilePrefab, _movementArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
