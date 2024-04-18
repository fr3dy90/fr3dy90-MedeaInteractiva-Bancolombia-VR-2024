using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElementController : MonoBehaviour
{
   [SerializeField] private ElementView _elementView;
   [SerializeField] private Element _element;
   [SerializeField] private Button _button;

   public void InitElement(Element _element, Color col)
   {
       _elementView.InitElement(_element, col);
       this._element = _element;
       _button = GetComponent<Button>();
   }
   
   public void OnSelect(Color col)
   {
       _elementView.OnSelect(col);
       _element.isViewed = true;
   }
   
   public Element GetElement()
   {
       return _element;
   }
   
   public void OnClick()
   {
       StartCoroutine(OnHandleClic());
   }

   IEnumerator OnHandleClic()
   {
       float seconds = 1;

       yield return new WaitForSeconds(seconds);

       {
           _button.onClick?.Invoke();
       }
   }

   public void OnStopLoading()
   {
   }
}
