using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickHiddenObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string _text = "좌측에 가장 큰 오브젝트는 사람도 지나갈 수 있겠는데?";
    GameObject _textBox;
    Text _textBoxTxt;

    // 걍 텍스트만 넣어줘서 무료 힌트같은 느낌으로 사용.
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
