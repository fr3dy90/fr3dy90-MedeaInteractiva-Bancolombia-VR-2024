using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConoceElementsView : MonoBehaviour
{
    [SerializeField] public TMP_Text _headerSubtitle;
    [SerializeField] private Image _imageElement;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] public Button _exitButton;
    [SerializeField] private Vector3 _deseiredPosition;
    public bool isdebug = false;
    private int _xScale;
    
    public void Update()
    {
        if (isdebug)
            _imageElement.transform.localPosition = _deseiredPosition;
    }

    public void OnUpdateView(Element _element)
    {
        _imageElement.sprite = _element.sprite;
        _title.text = _element._title;
        _description.text = _element.description;
        _imageElement.transform.localPosition = _element.deseiredPosition;
        _xScale = _element.isFlip ? -1 : 1;
        _imageElement.transform.localScale = new Vector3(_xScale, 1, 1);
    }
}
