using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using Random = System.Random;

[ExecuteInEditMode]
public class QuestionsController : MonoBehaviour
{
   [SerializeField, InspectorButton("TestQestions")]
   private string pressToSetQuestions;
   [SerializeField, InspectorButton("GetQuestion")]
   private string pressToShowQuestion;
   
   [SerializeField] private CanvasGroup _canvasGroup;
   [SerializeField] private QuestionsView _questionsView;
   [SerializeField] private ScriptableQuestion _questions;
   [SerializeField] private int _indexQuestion;
   
   [SerializeField]private Transform _parent;
   [SerializeField] private CanvasGroup _juegoCanvasGroup;
   


   private void Start()
   {
      
      _parent.gameObject.SetActive(false);
      _questionsView.buttonContinue.onClick.AddListener(() =>
      {
         StartCoroutine(Tools.Fade(1,0, 1f, _canvasGroup, OnEnded));
      });
   }
   
   private void OnEnded()
   {
      _canvasGroup.alpha = 0;
     SceneController.Instance.ChangeScene(MomentScene.Momento2); 
      _parent.gameObject.SetActive(false);
      _juegoCanvasGroup.blocksRaycasts = true;
   }
   
   public void StartQuestions()
   {
      SetQuestion(SetAnswers);
      _canvasGroup.alpha = 0;
      _parent.gameObject.SetActive(true);
      
     OnInit(() => { 
      StartCoroutine(Tools.Fade(0,1, 1f, _canvasGroup, null));
     });
   }

   private void OnInit(Action onComplete)
   {
      _indexQuestion = 0;
      for (int i = 0; i < _questions.questions.Length; i++)
      {
         _questions.questions[i].isSelected = false;
         for (int j = 0; j < _questions.questions[i].answer.Length; j++)
         {
            _questions.questions[i].answer[j].isSelected = false;
         }
      }
      
      foreach (var t in _questionsView.buttonsAnswers)
      {
         t.onClick.RemoveAllListeners();
      }

      foreach (var t in _questionsView.buttonsAnswers)
      {
         t.onClick.AddListener(() => SetAnswer(t.transform.GetSiblingIndex()));
      }
      _questionsView.backButton.onClick.RemoveAllListeners();
      _questionsView.nextButton.onClick.RemoveAllListeners();
      
      _questionsView.backButton.onClick.AddListener(() => HandleIndex(_indexQuestion - 1));
      _questionsView.nextButton.onClick.AddListener(() =>
      {
         if (_questions.questions[_indexQuestion].isSelected)
         {
            HandleIndex(_indexQuestion + 1);
         }
      });
      HandleIndex(_indexQuestion);
      _questionsView.HandleNavigation(_questionsView.buttonContinue, false);
      onComplete?.Invoke();
   }

   private void TestQestions()
   {
      SetQuestion(SetAnswers);
   }

   private void GetQuestion(int selectedQuestionIndex)
   {
      _questionsView.SetQuestion(_questions.questions[selectedQuestionIndex], selectedQuestionIndex);
   }


   public void SetQuestion(Action onComplete = null)
   {
      _juegoCanvasGroup.blocksRaycasts = false;
      Random rnd = new Random();
      _questions.questions = _questions.questions.OrderBy(x => rnd.Next()).ToArray();
      onComplete?.Invoke();
   }
   
   public void SetAnswers()
   {
      for (int i = 0; i < _questions.questions.Length; i++)
      {
         Random rnd = new Random();
         _questions.questions[i].answer = _questions.questions[i].answer.OrderBy(x => rnd.Next()).ToArray();
      }
   }
   
   public void SetAnswer(int indexAnswer)
   {
      if (!_questions.questions[_indexQuestion].isSelected)
      {
         _questions.questions[_indexQuestion].isSelected = true;
         _questions.questions[_indexQuestion].answer[indexAnswer].isSelected = true;
         _questionsView.SetColorAnswer(indexAnswer, _questions.questions[_indexQuestion].answer[indexAnswer].isCorrect);
         if (!_questions.questions[_indexQuestion].answer[indexAnswer].isCorrect)
         {
            ShowRigth();
         }

         if (_indexQuestion == _questions.questions.Length - 1)
         {
            _questionsView.HandleNavigation(_questionsView.buttonContinue, true);
         }
      }
   }

   public void ShowRigth()
   {
      for (int i = 0; i < _questions.questions[_indexQuestion].answer.Length; i++)
      {
         if(_questions.questions[_indexQuestion].answer[i].isCorrect)
         {
            _questionsView.SetColorAnswer(i, _questions.questions[_indexQuestion].answer[i].isCorrect);
         }
      }
   }

   public void HandleIndex(int index)
   {
      if (index < 0)
      {
         index = 0;
      }
      if(index > _questions.questions.Length - 1)
      {
         index = _questions.questions.Length - 1;
      }
      
     _questionsView.HandleNavigation(_questionsView.backButton, index != 0);
     _questionsView.HandleNavigation(_questionsView.nextButton, index != _questions.questions.Length - 1);
      
     _indexQuestion = index;
      GetQuestion(_indexQuestion);
   }
   
   public void OnClick()
   {
      StartCoroutine(Clic());
   }

   IEnumerator Clic()
   {
      float seconds = ReticlePointerController.Instace.maxSliderValue;
      ReticlePointerController.Instace.loading = true;

      yield return new WaitForSeconds(seconds);

      if(ReticlePointerController.Instace.ready && !ReticlePointerController.Instace.loading)
      {
         OnEnded();
         ReticlePointerController.Instace.ready = false;
      }
   }

   public void OnExit()
   {
      ReticlePointerController.Instace.StopLoading();
   }
}