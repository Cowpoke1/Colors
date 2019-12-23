using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GUIController : MonoBehaviour
{
    [SerializeField]
    GameObject panelShape;
    [SerializeField]
    GameObject panelColors;

    [SerializeField]
    Button btnClose;
    [SerializeField]
    GameObject btnShapePref;
    [SerializeField]
    GameObject btnColorPref;
    [SerializeField]
    List<CellColor> cells;

    Action<int> setShapeAction;
    Action<int> setColorAction;
    int idModel;

    private void Start()
    {
        OpenShapes();
        btnClose.onClick.AddListener(()=> { OpenShapes(); GameController.Instance.CloseColor();});
    }

    public void InitShape(int id, string name)
    {
        Button btn = Instantiate(btnShapePref, Vector3.zero, Quaternion.identity, panelShape.transform).GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(()=>SetShape(id));

        btn.GetComponentInChildren<Text>().text = name;
    }

    public void InitColor(int id, Color _color)
    {
        Button btn = Instantiate(btnColorPref, Vector3.zero, Quaternion.identity, panelColors.transform).GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => SetColor(id));
        cells.Add(new CellColor());
        cells[id].fill = btn.transform.GetChild(0).GetComponent<Image>();
        cells[id].mark = btn.transform.GetChild(1).GetComponent<Image>();
        cells[id].fill.color = _color;
    }

    void OpenShapes()
    {
        panelShape.SetActive(true);
        panelColors.SetActive(false);
        btnClose.gameObject.SetActive(false);
    }

    void OpenColors()
    {
        panelShape.SetActive(false);
        panelColors.SetActive(true);
        btnClose.gameObject.SetActive(true);
        SetMark(Prefs.GetColor(idModel));
    }

    void SetShape(int id)
    {
        idModel = id;
        OpenColors();
        setShapeAction(id);
    }

    void SetColor(int id)
    {
        SetMark(id);
        setColorAction(id);
    }
    
    void SetMark(int id)
    {
        foreach (CellColor var in cells)
        {
            var.mark.gameObject.SetActive(false);
        }
        cells[id].mark.gameObject.SetActive(true);
    }

    public void AddShapeSetListener(Action<int> listener)
    {
        setShapeAction += listener;
    }

    public void RemoveShapeSetListener(Action<int> listener)
    {
        setShapeAction -= listener;
    }

    public void AddColorsSetListener(Action<int> _listener)
    {
        setColorAction += _listener;
    }

    public void RemoveColorsSetListener(Action<int> _listener)
    {
        setColorAction -= _listener;
    }

}

[Serializable]
public class CellColor
{
    public Image mark;
    public Image fill;
}
