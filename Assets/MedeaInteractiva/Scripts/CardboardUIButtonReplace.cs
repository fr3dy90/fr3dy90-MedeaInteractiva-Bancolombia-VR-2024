using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardboardUIButtonReplace : MonoBehaviour
{
    [SerializeField] private GameObject buttonReplace;
    [SerializeField] private Button _button;

    void OnEnable()
    {
        if (GetComponent<Button>() != null)
        {
            _button = GetComponent<Button>();
        }
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
            if (_button != null)
            {
                go.GetComponent<Collider>().enabled = _button.interactable;
            }
            go.SetActive(value);
        }
    }
}
