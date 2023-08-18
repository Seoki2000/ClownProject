using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickDeadPerson : MonoBehaviour, IPointerClickHandler
{
    GameObject _deadObj;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 철퍽 떨어지거나 쿵소리랑 같이 나오고 비명소리도 나오면 딱일거같음.
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
