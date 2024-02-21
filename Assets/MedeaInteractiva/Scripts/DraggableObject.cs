using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour
{
    public Category1 category1;
    public string objectName;
    public bool isDetectCategory;
    public bool isCorrectCategory;

    private Vector3 initialPos;
    private Vector3 initialScale = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 endScale = new Vector3(0.55f, 0.55f, 0.55f);

    private GameObject currentObject;

    void Start()
    {
        initialPos = transform.position;
    }

    public void SetScale(bool startScale)
    {
        if (!startScale)
        {
            transform.DOScale(endScale, 0.2f);
        }
        else
        {
            DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetRecyclable(true)
                .Append(transform.DOMove(initialPos, 0.2f))
                .Append(transform.DOScale(initialScale, 0.2f));
        }
    }

    void OnTriggerEnter(Collider col)
    {
        currentObject = col.gameObject;
        isDetectCategory = true;
        InputController.Instace.detecting = true;
        if(col.GetComponent<ContainerCategory>().category1 == category1)
        {
            isCorrectCategory = true;
        }
        else
        {
            isCorrectCategory = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        currentObject = col.gameObject;
        isDetectCategory = true;
        InputController.Instace.detecting = true;
        if(col.GetComponent<ContainerCategory>().category1 == category1)
        {
            isCorrectCategory = true;
        }
        else
        {
            isCorrectCategory = false;
        }
    }

    void OnTriggerExit()
    {
        ReticlePointerController.Instace.StopLoading();
        currentObject = null;
        isDetectCategory = false;
        isCorrectCategory = false;
    }

    private void OnDisable()
    {
        if (currentObject != null)
        {
            currentObject.GetComponent<ContainerCategory>().ResetScale();
        }
    }
}
