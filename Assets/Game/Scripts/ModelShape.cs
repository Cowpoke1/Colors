using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelShape : MonoBehaviour
{
    public Renderer rendMaterial;
    public int id;

    public void Init(int _id)
    {
        id = _id;
        rendMaterial = GetComponent<Renderer>();
    }

    public void SetColor(string str)
    {
        Color col = Color.white;
        if (ColorUtility.TryParseHtmlString("#" + str, out col))
        {
            rendMaterial.material.SetColor("_Color", col);
        }
        else
        {
            rendMaterial.material.SetColor("_Color", Color.white);
        }
        
    }

    public void SetColor(Color _color)
    {

        rendMaterial.material.SetColor("_Color", _color);

    }
}
