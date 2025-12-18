using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickFakeObject : MonoBehaviour, IPointerClickHandler
{
    public string _text = "다른방법으로 봐야하나...";
    GameObject _Obg;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Obg != null)     // 만약 이미 생성되어 있는 경우
        {
            _Obg.SetActive(true);
            Text textNum = _Obg.transform.GetChild(0).GetChild(0).GetComponent<Text>();      
            textNum.text = _text;
            Invoke("GameObjectSetClose", 1);
        }
        else
        {
            //맵에 없는 경우만 생성 
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Text txt = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();     
            txt.text = _text;       
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
            _Obg = GameObject.FindGameObjectWithTag("TextingBox");
            Invoke("GameObjectSetClose", 1);
        }
    }

    public void GameObjectSetClose()
    {
        _Obg.SetActive(false);
    }
}
