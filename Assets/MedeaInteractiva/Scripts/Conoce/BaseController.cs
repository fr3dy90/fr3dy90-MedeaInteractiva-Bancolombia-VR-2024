using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected bool isInit = false;
    public virtual void Init()
    {
        if (isInit) return;
        isInit = true;
    }
}
