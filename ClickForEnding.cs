using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickForEnding : MonoBehaviour, IPointerClickHandler
{
    // Ŭ�� �� BG�� ��Ӱ� �ϰ� ���� �����ڽ��ϱ�? �� �ƴϿ� ��ư ������ �� ������ ���� �����ְ� ���â �Ѿ��, �ƴϿ� ������ ���� ������Ʈ ����.

    GameObject _Obg;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Obg != null)
        {
            _Obg.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("EndingButtonBoxBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _Obg = GameObject.FindGameObjectWithTag("EndingBox");
        }
    }
}
