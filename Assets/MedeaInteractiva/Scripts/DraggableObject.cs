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

    private ContainerCategory lastCategory;
    private ObjectPosition _objPos;
    public bool isLastPost = false;

    void Start()
    {
        initialPos = transform.position;
        if (isLastPost)
        {
            _objPos = GetComponent<ObjectPosition>();
        }
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
        if (col.GetComponent<ContainerCategory>() != null)
        {
            lastCategory = col.gameObject.GetComponent<ContainerCategory>();
            isDetectCategory = true;
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.GetComponent<ContainerCategory>() != null)
        {
            lastCategory = col.gameObject.GetComponent<ContainerCategory>();
            isDetectCategory = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<ContainerCategory>() != null)
        {
            lastCategory = null;
            isDetectCategory = false;
            isCorrectCategory = false;
        }
    }

    private void OnDisable()
    {
        if (lastCategory != null)
        {
            lastCategory.GetComponent<ContainerCategory>().ResetScale();
        }
    }

    public void OnRelease()
    {
        if (isDetectCategory && lastCategory != null)
        {
            isCorrectCategory = lastCategory.category1 == category1;
            if (_objPos == null)
            {
                GameLogic.Instance.DoActionEvent(isCorrectCategory, category1);
                gameObject.SetActive(false);
            }
            else
            {
                GameLogic.Instance.DoActionEvent(isCorrectCategory, category1, _objPos);
                if(isCorrectCategory) gameObject.SetActive(false);
            }
        }
        else
        {
            if(_objPos != null) _objPos.ResetPosition();
        }
    }
}
