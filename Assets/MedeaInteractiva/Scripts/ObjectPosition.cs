using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition : MonoBehaviour
{
    [SerializeField] private Vector3 initialPosition;

    void Start()
    {
        initialPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);    
    }

    public void ResetPosition()
    {
        transform.localPosition = initialPosition;
    }

}
