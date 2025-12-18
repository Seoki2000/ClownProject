using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHiddenHint : MonoBehaviour, IPointerClickHandler
{
    GameObject _hintObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_hintObj != null)
        {
            _hintObj.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map3/HintObjectZoomBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _hintObj = GameObject.FindGameObjectWithTag("HintObject");
        }
    }
}
