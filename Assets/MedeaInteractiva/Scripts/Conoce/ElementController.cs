using UnityEngine;

public class ElementController : MonoBehaviour
{
   [SerializeField] private ElementView _elementView;
   [SerializeField] private Element _element;

   public void InitElement(Element _element, Color col)
   {
       _elementView.InitElement(_element, col);
       this._element = _element;
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
}
