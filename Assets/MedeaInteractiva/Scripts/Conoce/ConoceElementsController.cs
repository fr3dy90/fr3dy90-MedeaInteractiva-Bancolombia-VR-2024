using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConoceElementsController : BaseController
{
    [SerializeField] private ConoceElementsView _conoceElementsView;
    [SerializeField] private Elements _elements;
    [SerializeField] private Transform _leftContainer;
    [SerializeField] private GameObject _prefElement;
    [SerializeField] private List<ElementController> _elementsList;


    private void Start()
    {
        //Init();
    }

    public override void Init()
    {
        base.Init();
        for (int i = 0; i < _elements.elements.Length; i++)
        {
            GameObject element = Instantiate(_prefElement, _leftContainer);
            ElementController elementController = element.GetComponent<ElementController>();
            elementController.InitElement(_elements.elements[i], _elements.elements[i].isViewed?_elements.selectedColor:_elements.defaultColor);
            _elementsList.Add(elementController);
            elementController.GetComponent<Button>().onClick.AddListener(() => OnSelectElement(elementController));
        }
    }

    public void onUpdateGrid()
    {
        
    }

    private void OnSelectElement(ElementController _elementController)
    {
        _conoceElementsView.OnUpdateView(_elementController.GetElement());
        _elementController.OnSelect(_elements.selectedColor);
        
    }
}
