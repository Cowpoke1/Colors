using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MainScriptable", menuName = "Scriptable/MainScriptable")]
 public class MainScriptable : ScriptableObject
 {
    [Header("Shapes")]
    public Shape[] shapes;
    [Header("Colors")]
    public Colors[] colors;
}

 [Serializable]
 public class Shape
 {
    public string name;
    public GameObject prefab;
    //public int startColorID;
    public int idShape;
 }

[Serializable]
public class Colors
{
    public string name;
    public Color color;
}

