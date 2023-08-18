using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickCardObj : MonoBehaviour, IPointerClickHandler
{
    GameObject _cardObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cardObj != null)       // �̹� ������ ���
        {
            _cardObj.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map2/CardObjImgBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // ĵ������ �ڽĳ���� ���������� ����(�׷����� ����.)
            _cardObj = GameObject.FindGameObjectWithTag("CardObj");
        }
    }
}
