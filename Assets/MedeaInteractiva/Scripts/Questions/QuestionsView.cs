using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _questionsHeader;
    [SerializeField] private TMP_Text _txtQuestion;
    [SerializeField] private TMP_Text[] _txtAnswers;

    [SerializeField] private Color _colorDefault;
    [SerializeField] private Color _colorRight;
    [SerializeField] private Color _colorWrong;
    [SerializeField] private Image _imageRight;
    [SerializeField] private Image _imageWrong;
    [SerializeField] private float _offset;
    public Button backButton;
    public Button nextButton;
    public Button[] buttonsAnswers;
    public Button buttonContinue;


    public void SetQuestion(Question question, int index)
    {
        _txtQuestion.text = question.question;
        for (int i = 0; i < _txtAnswers.Length; i++)
        {
            _txtAnswers[i].text = question.answer[i].answer;
        }

        _questionsHeader.text = "Pregunta " + (index + 1);
        _imageRight.gameObject.SetActive(false);
        _imageWrong.gameObject.SetActive(false);
        
        if (!question.isSelected)
        {
            for (int i = 0; i < buttonsAnswers.Length; i++)
            {
                buttonsAnswers[i].image.color = _colorDefault;
            }
         
        }
        else
        {
            
            for (int i = 0; i < question.answer.Length; i++)
            {
                buttonsAnswers[i].image.color = _colorDefault;
                
                if (question.answer[i].isCorrect)
                {
                    buttonsAnswers[i].image.color = _colorRight;
                    _imageRight.gameObject.SetActive(true);
                    _imageRight.transform.localPosition = new Vector3(_imageRight.transform.localPosition.x, buttonsAnswers[i].transform.localPosition.y+_offset, 0);
                    
                }
                
                if (question.answer[i].isSelected && !question.answer[i].isCorrect)
                {
                    buttonsAnswers[i].image.color = _colorWrong;
                    _imageWrong.gameObject.SetActive(true);
                    _imageWrong.transform.localPosition = new Vector3(_imageWrong.transform.localPosition.x, buttonsAnswers[i].transform.localPosition.y+_offset, 0);
                    
                }
            }
        }
    }

    public void SetColorAnswer(int index, bool correct)
    {
        buttonsAnswers[index].image.color = correct ? _colorRight : _colorWrong;
        if (correct)
        {
            _imageRight.gameObject.SetActive(true);
            _imageRight.transform.localPosition = new Vector3(_imageRight.transform.localPosition.x, buttonsAnswers[index].transform.localPosition.y+_offset, 0);
        }
        else
        {
            _imageWrong.gameObject.SetActive(true);
            _imageWrong.transform.localPosition = new Vector3(_imageWrong.transform.localPosition.x, buttonsAnswers[index].transform.localPosition.y+_offset, 0);
        }
    }

    public void HandleNavigation(Button btn, bool isActive)
    {
        btn.gameObject.SetActive(isActive);
    }
}