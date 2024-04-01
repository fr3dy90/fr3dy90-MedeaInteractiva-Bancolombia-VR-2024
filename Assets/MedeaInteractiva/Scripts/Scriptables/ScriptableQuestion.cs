using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question")]
public class ScriptableQuestion : ScriptableObject
{
    public Question[] questions;
}

[System.Serializable]
public struct Question
{
    [TextArea(2,10)]
    public string question;
    public Answer[] answer;
    public bool isSelected;
}

[System.Serializable]
public struct Answer
{
    [TextArea(2,5)]
    public string answer;
    public bool isCorrect;
    public bool isSelected;
}
