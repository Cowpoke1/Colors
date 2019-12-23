using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    public MainScriptable mainObj;

    [SerializeField]
    Transform parentModels;
    ModelShape[] models;

    [SerializeField]
    GUIController guiController;

    [SerializeField]
    Transform cam;
    Vector3 startPos;
   public int idModel = 0;    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        guiController.AddShapeSetListener(SetShape);
        guiController.AddColorsSetListener(SetColor);
        Init();
    }

    void Init()
    {
        startPos = cam.position;
        int count = mainObj.shapes.Length;
        models = new ModelShape[count];
        Vector3 pos = Vector3.zero;
        for(int i = 0; i < count; i++)
        {
            Shape _shape = mainObj.shapes[i];
            pos = new Vector3(-3 + i*6, 0, -4);
            models[i] = Instantiate(_shape.prefab,pos,Quaternion.identity, parentModels).GetComponent<ModelShape>();
            models[i].Init(_shape.idShape);
            guiController.InitShape(i, _shape.name);
        }
        InitStartColors();
        InitColors();
    }

    void InitColors()
    {
        int count = mainObj.colors.Length;
        for (int i = 0; i < count; i++)
        {
            guiController.InitColor(i, mainObj.colors[i].color);
        }
    }

    void InitStartColors()
    {
        int count = 0;
        foreach(ModelShape var in models)
        {
            string str = Prefs.GetColorShape(var.id);
            if (String.IsNullOrEmpty(str))
            {
                int idCol = count;//mainObj.shapes[var.id].startColorID;
                var.SetColor(mainObj.colors[idCol].color);

            }
            else
            {
                var.SetColor(str);
            }
            count++;
        }
    }
    
    void SetColor(int id)
    {
        Color color = mainObj.colors[id].color;
        String str = ColorUtility.ToHtmlStringRGBA(color);
        Prefs.SetColorShape(idModel, str);
        Prefs.SetColor(idModel, id);
        models[idModel].SetColor(color);
    }

    void SetShape(int id)
    {
        idModel = id;
        Vector3 pos = new Vector3(-3 + id * 6, 0.5f, -8f);
        LeanTween.cancel(cam.gameObject);
        LeanTween.move(cam.gameObject, pos, 1f);
    }

    public void CloseColor()
    {
        LeanTween.cancel(cam.gameObject);
        LeanTween.move(cam.gameObject, startPos, 1f);
    }

    private void OnDestroy()
    {
        guiController.RemoveShapeSetListener(SetShape);
        guiController.RemoveColorsSetListener(SetColor);
    }

}
