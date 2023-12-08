using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GridManager : MonoBehaviour {
    [SerializeField] private int _gridSize = 8;
    [SerializeField] private float _gridRightOffset = 4.0f, _gridTopOffset = 4.0f;


    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private int _start, _end;

    public Button run, reset;
    private Dictionary<Vector2, Tile> _tiles;
    [SerializeField] private Text score_text;

    [SerializeField] private Sprite[] bulldozer_sprites = new Sprite[4];
    [SerializeField] private int[] _movement_array;
    
    private float tile_length = 1.0f;
    private bool run_clicked = false;


    private int max_score = 0;

    void Start() {
        run.onClick.AddListener(Run);
        run.interactable = false;

        reset.onClick.AddListener(Reset);
    }

    public void Init(int width, int height, int gridSize, float gridRightOffset, float gridTopOffset, Tile tilePrefab, int start, int end, Button run, Button reset, Text _score_text, int[] movement_array, int score) {
        _width = width;
        _height = height;
        _gridSize = gridSize;
        _gridRightOffset = gridRightOffset;
        _gridTopOffset = gridTopOffset;
        _tilePrefab = tilePrefab;
        _start = start;
        _end = end;
        this.run = run;
        this.reset = reset;
        _movement_array = movement_array;
        score_text = _score_text;
        max_score = score;
        GenerateGrid();

        score_text.text = $"{0}/{max_score}";
    }
 
    void GenerateGrid() {
        float max_size = Math.Max(_width, _height);
        tile_length = (float)_gridSize / max_size;
        float x_offset = ((max_size - (float)_width) / 2.0f) * tile_length + _gridRightOffset + tile_length / 2.0f;
        float y_offset = ((max_size - (float)_height) / 2.0f) * tile_length + _gridTopOffset + tile_length / 2.0f;
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x * tile_length + x_offset, y * tile_length + y_offset), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.localScale = new Vector3(tile_length, tile_length, 1f);
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                var isStart = (x == _start / _height && y == _start % _height);
                var isEnd = (x == _end / _height && y == _end % _height);
                
                spawnedTile.Init(isOffset, isStart, isEnd);
 
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        // Vector3 camPos = new Vector3(((float)_gridSize-tile_length)/2.0f-_gridRightOffset, ((float)_gridSize-tile_length)/2.0f - _gridTopOffset, -10);
        // Debug.Log($"Cam pos: {camPos}");
        // _cam.transform.position = camPos;    
    }
 
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public List<Tile> BFS(Tile startTile, Tile endTile) {

        Dictionary<Tile, Tile> parentMap = new Dictionary<Tile, Tile>();
        Queue<Tile> q = new Queue<Tile>();
        HashSet<Tile> visited = new HashSet<Tile>();

        q.Enqueue(startTile);
        while (q.Count > 0)
        {
            Tile current = q.Dequeue();

            if (current == endTile)
            {
                return ReconstructPath(parentMap, startTile, endTile);
            }

            foreach (Tile neighbor in GetNeighbors(current))
            {
                if (!visited.Contains(neighbor) && !neighbor.isFilled)
                {
                    q.Enqueue(neighbor);
                    visited.Add(neighbor);
                    parentMap[neighbor] = current;
                }
            }
        }
        return null;
    }

    List<Tile> GetNeighbors(Tile tile)
    {
        List<Tile> neighbors = new List<Tile>();
        string[] nameParts = tile.name.Split(' ');
        
        if (nameParts.Length == 3 && nameParts[0] == "Tile")
        {
            int i, j;
            if (int.TryParse(nameParts[1], out i) && int.TryParse(nameParts[2], out j))
            {
                Vector2 tilePos = new Vector2(i, j);

                for (int x=0; x<_movement_array.Length; x+=2)
                {
                    Vector2 neighborPos = tilePos + new Vector2(_movement_array[x], _movement_array[x+1]);
                    Tile neighbor = GetTileAtPosition(neighborPos);
                    if (neighbor != null)
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }
            else
            {
                Debug.LogError("Error parsing indices from tile name: " + gameObject.name);
            }
        }
        else
        {
            Debug.LogError("Invalid tile name format: " + gameObject.name);
        }
        return neighbors;
    }

    List<Tile> ReconstructPath(Dictionary<Tile, Tile> parentMap, Tile start, Tile end)
    {
        List<Tile> path = new List<Tile>();
        Tile current = end;

        while (current != start)
        {
            path.Add(current);
            current = parentMap[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }

    void Update()
    {
        if (!run_clicked && Input.GetMouseButtonDown(0))
        {
            Tile startTile = GetTileAtPosition(new Vector2(_start / _height, _start % _height));
            Tile endTile = GetTileAtPosition(new Vector2(_end / _height, _end % _height));

            List<Tile> path_going = BFS(startTile, endTile);
            List<Tile> path_coming = BFS(endTile, startTile);
            
            if (path_going != null && path_coming != null)
            {
                run.interactable = true;
            } 
            else
            {
                run.interactable = false;
                Debug.Log("No path found");
            }
        }
    }

    void Run() {
        Debug.Log("Run");
        Tile startTile = GetTileAtPosition(new Vector2(_start / _height, _start % _height));
        Tile endTile = GetTileAtPosition(new Vector2(_end / _height, _end % _height));

        List<Tile> path_going = BFS(startTile, endTile);
        List<Tile> path_coming = BFS(endTile, startTile);

        if (path_going != null && path_coming != null)
        {
            run.interactable = true;
            Debug.Log($"Total path length = {path_going.Count + path_coming.Count - 2}");
            
            GameObject bulldozerCopy = Instantiate(startTile._start, startTile.transform.position, Quaternion.identity);
            Vector3 originalScale = bulldozerCopy.transform.localScale;
            bulldozerCopy.transform.localScale = new Vector3(originalScale[0]*tile_length, originalScale[1]*tile_length, 1f);
            bulldozerCopy.SetActive(true);
            
            StartCoroutine(MoveBulldozerAlongPath(bulldozerCopy, path_going, path_coming, startTile, endTile));
        }
        else
        {
            run.interactable = false;
            Debug.Log("No path found");
        }
    }

    IEnumerator animateBulldozer(GameObject bulldozer, Tile tile) {
        float moveSpeed = 5f;
        Vector3 targetPosition = tile.transform.position;
        targetPosition[0] += 0.06f;
        targetPosition[1] += 0.11f;
        float distance = Vector3.Distance(bulldozer.transform.position, targetPosition);
        Vector3 direction = (targetPosition - bulldozer.transform.position).normalized;

        float delta_x = targetPosition[0] - bulldozer.transform.position[0];
        float delta_y = targetPosition[1] - bulldozer.transform.position[1];

        SpriteRenderer sr = bulldozer.GetComponent<SpriteRenderer>();
        if (Math.Abs(delta_x) - Math.Abs(delta_y) < 0.01f)
        {
            if (delta_y > 0)
                sr.sprite = bulldozer_sprites[0];
            else
                sr.sprite = bulldozer_sprites[2];
        }
        else
        {
            if (delta_x > 0)
                sr.sprite = bulldozer_sprites[3];
            else
                sr.sprite = bulldozer_sprites[1];
        }

        while (distance > 0.05f)
        {
            bulldozer.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            distance = Vector3.Distance(bulldozer.transform.position, targetPosition);
            direction = (targetPosition - bulldozer.transform.position).normalized;

            yield return null;
        }
        bulldozer.transform.position = targetPosition;
        yield return new WaitForSeconds(0.1f);

    }

    IEnumerator MoveBulldozerAlongPath(GameObject bulldozer, List<Tile> path1, List<Tile> path2, Tile startTile, Tile endTile)
    {
        Tile.change_locked = true;
        run.interactable = false;
        reset.interactable = false;
        run_clicked = true;
        startTile._start.SetActive(false);
        int count = 0;
        // _canvas.GetComponentInChildren<TMP_Text>().text = $"{count}/{max_score}";
        score_text.text = $"{count}/{max_score}";
        Sprite oldSprite = endTile._leaves.GetComponent<SpriteRenderer>().sprite;
        foreach (Tile tile in path1)
        {
            if (tile == endTile) {
                endTile._end.SetActive(false);
                endTile._end_cut.SetActive(true);
            } 
            if (tile != startTile)
                count++;
            yield return animateBulldozer(bulldozer, tile);
            // _canvas.GetComponentInChildren<TMP_Text>().text = $"{count}/{max_score}";
            score_text.text = $"{count}/{max_score}";
        }
        // count++;
        foreach (Tile tile in path2)
        {
            if (tile != endTile)
                count++;
            yield return animateBulldozer(bulldozer, tile);
            // _canvas.GetComponentInChildren<TMP_Text>().text = $"{count}/{max_score}";
            score_text.text = $"{count}/{max_score}";
        }
        yield return new WaitForSeconds(0.1f);
        endTile._end.SetActive(true);
        endTile._end_cut.SetActive(false);
        startTile._start.SetActive(true);

        Destroy(bulldozer);
        Tile.change_locked = false;
        run.interactable = true;
        reset.interactable = true;
        run_clicked = false;
        LevelComplete(count);
    }

    void Reset() {
        Debug.Log("Reset");
        run.interactable = false;
        for (int i=0; i<_width; i++)
        {
            for (int j=0; j<_height; j++)
            {
                Tile tile = GetTileAtPosition(new Vector2(i, j));
                if (i == _start / _height && j == _start % _height)
                {
                    tile.isFilled = false;
                    tile._leaves.SetActive(false);
                    tile._empty.SetActive(false);
                    tile._start.SetActive(true);
                    tile._end.SetActive(false);
                }
                else if (i == _end / _height && j == _end % _height)
                {
                    tile.isFilled = false;
                    tile._leaves.SetActive(false);
                    tile._empty.SetActive(false);
                    tile._start.SetActive(false);
                    tile._end.SetActive(true);
                }
                else
                {
                    tile.isFilled = true;
                    tile._leaves.SetActive(true);
                    tile._empty.SetActive(false);
                }
            }
        }
    }

    public void LevelComplete(int count) {
        Debug.Log($"Final score {count}/{max_score}");
        int curLevel = PlayerPrefs.GetInt("cur_level", 1);
        
        int stars;
        if (count == max_score) {
            stars = 3;
        } else if (count >= max_score - 5) {
            stars = 2;
        } else {
            stars = 1;
        } 
        
        PlayerPrefs.SetInt("num_stars", stars);
        PlayerPrefs.SetInt($"stars_{curLevel}", stars);
        SceneManager.LoadScene("Level Complete");
    }
}
