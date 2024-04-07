using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxButtons : MonoBehaviour
{
    [SerializeField]   private Material material;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color normalColor;
    
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    public void Click()
   {
      SetColor(selectedColor);
   }
   
   public void Exlit()
   {
       SetColor(normalColor);
   }

   private void SetColor(Color c)
   {
       material.color = c;
   }
}
