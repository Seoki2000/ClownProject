using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickDeadPerson : MonoBehaviour, IPointerClickHandler
{
    GameObject _deadObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        // ö�� �������ų� ���Ҹ��� ���� ������ ���Ҹ��� ������ ���ϰŰ���.
        if (_deadObj != null)
        {
            _deadObj.SetActive(true);
            IngameManager.Instance.ClownSpawn();
        }
        else
        {
            GameObject go = Resources.Load("Map3/DeadPersonBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _deadObj = GameObject.FindGameObjectWithTag("DeadPerson");
            IngameManager.Instance.ClownSpawn();
        }
    }
}
