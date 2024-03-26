using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsView : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _txtQuestion;
  [SerializeField] private TextMeshProUGUI A_txtAnswer;
  [SerializeField] private TextMeshProUGUI B_txtAnswer;
  [SerializeField] private TextMeshProUGUI C_txtAnswer;
  [SerializeField] private TextMeshProUGUI D_txtAnswer;
  [SerializeField] private Color _colorRight;
  [SerializeField] private Color _colorWrong;
  [SerializeField] private Image _imageRight;
  [SerializeField] private Image _imageWrong;
  
  public void SetQuestion(Question question)
  {
    _txtQuestion.text = question.question;
    A_txtAnswer.text = question.answer[0].answer;
    B_txtAnswer.text = question.answer[1].answer;
    C_txtAnswer.text = question.answer[2].answer;
    D_txtAnswer.text = question.answer[3].answer;
  }
}
