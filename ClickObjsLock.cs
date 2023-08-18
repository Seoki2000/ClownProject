using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class ClickObjsLock : MonoBehaviour, IPointerClickHandler
{
    // 끼익하는 효과음이 있었으면 좋겠다 열고 닫았다는 느낌을 주기 위해서.

    [SerializeField] List<string> _textList;
    GameObject _textBox;
    Text _textBoxTxt;

    private void Awake()
    {
        _textList.Add("아무리 해봐도 열리지 않는다..");
        _textList.Add("굳게 잠겨있다.");
        _textList.Add("열 수 있는 방법이 보이지 않는다.");
    }
        
    // 걍 텍스트만 넣어줘서 무료 힌트같은 느낌으로 사용.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_textBox != null)
        {
            _textBox.SetActive(true);
            _textBoxTxt.text = _textList[Random.Range(0, _textList.Count)];
            Invoke("SetActiveFalse", 1);
        }
        else
        {
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _textBox = GameObject.FindGameObjectWithTag("TextingBox");
            _textBoxTxt = _textBox.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            _textBoxTxt.text = _textList[Random.Range(0, _textList.Count)];
            Invoke("SetActiveFalse", 1);
        }
    }

    public void SetActiveFalse()
    {
        _textBox.SetActive(false);
    }

}
