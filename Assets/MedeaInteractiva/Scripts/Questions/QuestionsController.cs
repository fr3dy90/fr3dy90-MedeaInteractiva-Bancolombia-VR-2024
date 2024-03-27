using UnityEngine;
using System.Linq;
using System;
using Random = System.Random;

public class QuestionsController : MonoBehaviour
{
   [SerializeField] private QuestionsView _questionsView;
   [SerializeField] private ScriptableQuestion _questions;
   [SerializeField] private int _index;

   private void Update()
   {
      if (Input.GetButtonDown("Jump"))
      {
         SetQuestion();
      }
   }
   
   

   public void SetQuestion()
   {
      for (int i = 0; i < _questions.questions.Length; i++)
      {
         Random rnd = new Random();
         _questions.questions[i].answer = _questions.questions[i].answer.OrderBy(x => rnd.Next()).ToArray();
      }
      
      _questionsView.SetQuestion(_questions.questions[0]);
   }
}
