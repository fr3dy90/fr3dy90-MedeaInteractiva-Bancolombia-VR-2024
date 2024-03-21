using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question")]
public class ScriptableQuestion : ScriptableObject
{
 [TextArea(3,10)]
 public string question;
 
 [TextArea(3,10)]
 public string[] answer;
 
 public const int correctAnswer = 0;
 
}
