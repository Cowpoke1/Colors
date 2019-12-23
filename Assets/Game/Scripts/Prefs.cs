using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs : MonoBehaviour
{

    public static string GetColorShape(int id)
    {
        return PlayerPrefs.GetString("color_" + id, "");
    }

    public static void SetColorShape(int id, string _color)
    {
         PlayerPrefs.SetString("color_" + id, _color);
    }

    public static int GetColor(int id)
    {
        return PlayerPrefs.GetInt("currentCol_" + id, id);
    }

    public static void SetColor(int id, int _color)
    {
        PlayerPrefs.SetInt("currentCol_" + id, _color);
    }

}
