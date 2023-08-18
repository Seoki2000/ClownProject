using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickMicroPassword : MonoBehaviour, IPointerClickHandler
{
    GameObject _Obg;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Obg != null)     // 만약 이미 생성되어 있는 경우
        {
            _Obg.SetActive(true);
        }
        else
        {
            //맵에 없는 경우만 생성 
            GameObject go = Resources.Load("Map3/Map3PasswordImg") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
            _Obg = GameObject.FindGameObjectWithTag("Password");
        }
    }
}
