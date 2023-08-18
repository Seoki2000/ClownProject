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
        if (_Obg != null)     // ���� �̹� �����Ǿ� �ִ� ���
        {
            _Obg.SetActive(true);
        }
        else
        {
            //�ʿ� ���� ��츸 ���� 
            GameObject go = Resources.Load("Map3/Map3PasswordImg") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
            _Obg = GameObject.FindGameObjectWithTag("Password");
        }
    }
}
