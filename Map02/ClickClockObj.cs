using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickClockObj : MonoBehaviour, IPointerClickHandler
{
    GameObject _clockObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_clockObj != null)    
        {
            _clockObj.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map2/ClockImgBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);      
            _clockObj = GameObject.FindGameObjectWithTag("ClockImg");
        }
    }
}
