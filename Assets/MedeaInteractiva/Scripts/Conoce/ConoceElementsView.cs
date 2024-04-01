using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConoceElementsView : MonoBehaviour
{
    [SerializeField] private Image _imageElement;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    
    public void OnUpdateView(Element _element)
    {
        _imageElement.sprite = _element.sprite;
        _title.text = _element._title;
        _description.text = _element.description;
    }
}
