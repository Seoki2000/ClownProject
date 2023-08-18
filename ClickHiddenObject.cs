using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickHiddenObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string _text = "������ ���� ū ������Ʈ�� ����� ������ �� �ְڴµ�?";
    GameObject _textBox;
    Text _textBoxTxt;

    // �� �ؽ�Ʈ�� �־��༭ ���� ��Ʈ���� �������� ���.
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_textBox != null)
        {
            _textBox.SetActive(true);
            _textBoxTxt.text = _text;
            Invoke("SetActiveFalse", 1);
        }
        else
        {
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _textBox = GameObject.FindGameObjectWithTag("TextingBox");
            _textBoxTxt = _textBox.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            _textBoxTxt.text = _text;
            Invoke("SetActiveFalse", 1);
        }
        
    }

    public void SetActiveFalse()
    {
        _textBox.SetActive(false);
    }

    
}
