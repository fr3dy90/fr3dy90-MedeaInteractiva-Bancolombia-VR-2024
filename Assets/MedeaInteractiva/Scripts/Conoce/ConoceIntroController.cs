using UnityEngine;

public class ConoceIntroController : BaseController
{
    [SerializeField] private ConoceIntroView _conoceIntroView;
    [SerializeField] private int _index = 1;
    [TextArea(10,4), SerializeField] private string _introText;
    
    private void Awake()
    {
        _conoceIntroView._buttonContinue.onClick.AddListener(() =>
        {
            ConoceController.OnInitScreen?.Invoke(_index, true, null);
        });
    }

    public override void Init()
   {
       base.Init();
       _conoceIntroView.SetText(_introText);
   }
}
