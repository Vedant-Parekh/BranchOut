using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] public GameObject _highlight;
    [SerializeField] public GameObject _leaves;
    [SerializeField] public GameObject _start;
    [SerializeField] public GameObject _end, _end_cut;
    [SerializeField] public GameObject _empty;
    [SerializeField] private Sprite[] filled_images = new Sprite[3];
    public bool _isStart, _isEnd;
    public bool isFilled;
    public static bool change_locked = false;

    public void Init(bool isOffset, bool isStart, bool isEnd)
    {
        _isStart = isStart;
        _isEnd = isEnd;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _highlight.SetActive(false);
        _highlight.GetComponent<SpriteRenderer>().sortingOrder = 2;

        isFilled = (!_isStart && !_isEnd);
        _leaves.SetActive(isFilled);
        _start.SetActive(_isStart);
        _end.SetActive(_isEnd);
        _end_cut.SetActive(false);

        _empty.SetActive(!isFilled && !_isStart && !_isEnd);
        int randomSpriteIndex = Random.Range(0, 10);
        if (randomSpriteIndex >= filled_images.Length) randomSpriteIndex = 0;
        _leaves.GetComponent<SpriteRenderer>().sprite = filled_images[randomSpriteIndex];
        _leaves.GetComponent<SpriteRenderer>().sortingOrder = 1;
        _leaves.transform.localScale = new Vector3(1f, 1f, 1f);
        Vector3 randomPosition = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        _leaves.transform.localPosition = randomPosition;
    }

    public void flipLeaves()
    {
        if (change_locked) return;
        if (_isStart || _isEnd) return;
        isFilled = !isFilled;
        _leaves.SetActive(isFilled);
        _empty.SetActive(!isFilled);
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        flipLeaves();
    }
}
