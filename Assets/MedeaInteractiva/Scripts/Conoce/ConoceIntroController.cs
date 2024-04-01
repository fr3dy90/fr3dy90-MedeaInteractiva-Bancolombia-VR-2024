using UnityEngine;

public class ConoceIntroController : BaseController
{
    [SerializeField] private ConoceIntroView _conoceIntroView;
    [SerializeField] private int _index = 1;
    
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
   }
}
