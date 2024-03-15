using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void DoActionEvent(bool isCorrect, Category1 currentCategory, ObjectPosition objPosition = null)
    {
       
        if(isCorrect)
        {
            if(GlobalData.level == 0)
            {
                LevelGame1.Instance.SetScore(currentCategory);
            }
            if(GlobalData.level == 1)
            {
                LevelGame2.Instance.SetScore(currentCategory);
                LevelGame2.Instance.Compare(true,currentCategory);
            }
            Debug.Log("correcto");
        }
        else
        {
            if(GlobalData.level == 0)
            {
                LevelGame1.Instance.ShowObject();
            }
            if(GlobalData.level == 1)
            {
                LevelGame2.Instance.Compare(false,currentCategory);
                objPosition.ResetPosition();
            }
            Debug.Log("incorrecto");
        }
    }
}
