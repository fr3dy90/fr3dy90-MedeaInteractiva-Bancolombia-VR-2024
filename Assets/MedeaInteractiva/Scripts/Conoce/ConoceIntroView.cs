using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConoceIntroView : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtItro;
    public Button _buttonContinue;
    
    public void SetIntro(string intro)
    {
        _txtItro.text = intro;
    }

    public void SetText(string txt)
    {
        _txtItro.text = txt;
    }
}
