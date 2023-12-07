using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MoveManager : MonoBehaviour {
    [SerializeField] private int _gridSize = 8;
    [SerializeField] private float _gridRightOffset = 4.0f, _gridTopOffset = 4.0f;
    [SerializeField] private int _width, _height;
    [SerializeField] private MoveTile _tilePrefab;
    [SerializeField] private int[] _movement_array;
    
    private float tile_length = 1.0f;

    void Start() {
    }

    public void Init(int width, int height, int gridSize, float gridRightOffset, float gridTopOffset, MoveTile tilePrefab, int[] movement_array) {
        _width = width;
        _height = height;
        _gridSize = gridSize;
        _gridRightOffset = gridRightOffset;
        _gridTopOffset = gridTopOffset;
        _tilePrefab = tilePrefab;
        _movement_array = movement_array;
        GenerateGrid();
    }
 
    void GenerateGrid() {
        float max_size = Math.Max(_width, _height);
        tile_length = (float)_gridSize / max_size;
        float x_offset = ((max_size - (float)_width) / 2.0f) * tile_length + _gridRightOffset + tile_length / 2.0f;
        float y_offset = ((max_size - (float)_height) / 2.0f) * tile_length + _gridTopOffset + tile_length / 2.0f;
        Debug.Log($"Tile length: {tile_length}");
        
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x * tile_length + x_offset, y * tile_length + y_offset), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.localScale = new Vector3(tile_length, tile_length, 1f);
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                var isAllowed = false;
                int center = (int)Math.Floor(max_size/2);
                var isBulldozer = (x == center && y == center);

                for (int i = 0; i < _movement_array.Length; i+=2)
                {
                    if (x == center + _movement_array[i] && y == center + _movement_array[i+1])
                    {
                        isAllowed = true;
                        // Debug.Log($"Allowed: {x} {y}");
                    }
                }
                spawnedTile.Init(isOffset, isAllowed, isBulldozer);
            }
        }  
    }
}
