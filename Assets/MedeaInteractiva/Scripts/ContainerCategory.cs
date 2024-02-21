using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ContainerCategory : MonoBehaviour
{
    public Category1 category1;

    private Vector3 initialScale;
    public Vector3 endScale;
    void Start()
    {
        initialScale = transform.localScale;
    }

    public void ResetScale()
    {
        transform.DOScale(initialScale, 0.2f);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<DraggableObject>())
            transform.DOScale(endScale, 0.2f);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<DraggableObject>())
            transform.DOScale(initialScale, 0.2f);
    }
}
