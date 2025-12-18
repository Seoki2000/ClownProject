using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickCardObj : MonoBehaviour, IPointerClickHandler
{
    GameObject _cardObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cardObj != null)       // 이미 생성된 경우
        {
            _cardObj.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map2/CardObjImgBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // 캔버스의 자식노드중 마지막으로 생성(그래야지 보임.)
            _cardObj = GameObject.FindGameObjectWithTag("CardObj");
        }
    }
}
