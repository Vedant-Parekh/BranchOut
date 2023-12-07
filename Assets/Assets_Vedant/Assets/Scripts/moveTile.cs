using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MoveTile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _allowed;
    [SerializeField] private GameObject _bulldozer;
 
    public void Init(bool isOffset, bool isAllowed, bool isBulldozer) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _allowed.SetActive(isAllowed);
        _bulldozer.SetActive(isBulldozer);
    }
}
