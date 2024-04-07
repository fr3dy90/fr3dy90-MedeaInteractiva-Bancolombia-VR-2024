using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ConoceElementsController : BaseController
{
    [SerializeField] private ConoceElementsView _conoceElementsView;
    [SerializeField] private Elements _elements;
    [SerializeField] private Transform _leftContainer;
    [SerializeField] private GameObject _prefElement;
    [SerializeField] private List<ElementController> _elementsList;
    [TextArea(10,5), SerializeField] private string _headerIntroText;


   

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
        OnSelectElement(_elementsList[0]);
        _conoceElementsView._exitButton.onClick.AddListener(OnClose);
        _conoceElementsView._headerSubtitle.text = _headerIntroText;
    }

    public void onUpdateGrid()
    {
        foreach (ElementController _elementController in _elementsList)
        {
            if (_elementController.GetElement().isViewed)
            {
                OnSelectElement(_elementController);
            }
        }
    }

    private void OnSelectElement(ElementController _elementController)
    {
        _conoceElementsView.OnUpdateView(_elementController.GetElement());
        _elementController.OnSelect(_elements.selectedColor);
        
    }
    
    private void OnClose()
    {
        ConoceController.OnClose?.Invoke();
    }
    
    public void OnClick()
    {
        StartCoroutine(Close());
    }
    
    IEnumerator Close()
    {
        float seconds = ReticlePointerController.Instace.maxSliderValue;
        ReticlePointerController.Instace.loading = true;

        yield return new WaitForSeconds(seconds);

        if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
        {
           ConoceController.OnClose?.Invoke();

            ReticlePointerController.Instace.ready = false;
        }
    }
    
    public void OnStopLoad()
    {
        ReticlePointerController.Instace.StopLoading();
    }
}
