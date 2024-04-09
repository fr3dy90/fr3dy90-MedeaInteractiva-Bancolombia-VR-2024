using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardboardUIButtonReplace : MonoBehaviour
{
    [SerializeField] private GameObject buttonReplace;

    void OnEnable()
    {
       
        ShowGameobject(buttonReplace,true);
    }

    void OnDisable()
    {
        ShowGameobject(buttonReplace,false);
    }

    void ShowGameobject(GameObject go ,bool value)
    {
        if(go != null)
        {
           
            go.SetActive(value);
        }
    }
}
