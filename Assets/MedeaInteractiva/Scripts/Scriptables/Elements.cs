﻿using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Elements", menuName = "ScriptableObjects/Elements")]
public class Elements : ScriptableObject
{
    public Element[] elements;
    public Element currentElement;
    public Color selectedColor;
    public Color defaultColor;
}

[System.Serializable]
public struct Element
{
    [FormerlySerializedAs("name")] public string _title;
    public string description;
    public Sprite sprite;
    public bool isViewed;
}
