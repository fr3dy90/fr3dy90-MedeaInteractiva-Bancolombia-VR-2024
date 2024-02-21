using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurvedUI;

public class Orient3DOnCurvedSpace : MonoBehaviour
{
    public CurvedUISettings mySettings;
    public bool isCanvas;
    public bool isDragging;

    void Awake()
    {
        if(isCanvas)
            mySettings = GetComponentInParent<CurvedUISettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector3 positionInCanvasSpace = mySettings.transform.worldToLocalMatrix.MultiplyPoint3x4(this.transform.parent.position);
            transform.position = mySettings.CanvasToCurvedCanvas(positionInCanvasSpace);
            transform.rotation = Quaternion.LookRotation(mySettings.CanvasToCurvedCanvasNormal(transform.parent.localPosition), transform.parent.up);
        }
    }
}
