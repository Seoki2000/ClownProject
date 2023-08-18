using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickForGetKey : MonoBehaviour, IPointerClickHandler
{
    GameObject _textBox;

    public void OnPointerClick(PointerEventData eventData)  // Ŭ�� �� ��� 
    {
        if (_textBox != null)    // �̹� �ݰ� �̹����� �ִ� ���.
        {
            _textBox.SetActive(true);
        }
        else
        {
            // ������ �Ϸ���.
            GameObject go = Resources.Load("Map2/SafeBoxBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");      // ĵ���� ��������.
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // ĵ������ �ڽĳ���� ���������� ����(�׷����� ����.)
            _textBox = GameObject.FindGameObjectWithTag("SafeBox");
        }
    }
}
