using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickForGetKey : MonoBehaviour, IPointerClickHandler
{
    GameObject _textBox;

    public void OnPointerClick(PointerEventData eventData)  // 클릭 한 경우 
    {
        if (_textBox != null)    // 이미 금고 이미지가 있는 경우.
        {
            _textBox.SetActive(true);
        }
        else
        {
            // 생성은 완료함.
            GameObject go = Resources.Load("Map2/SafeBoxBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");      // 캔버스 가져오기.
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // 캔버스의 자식노드중 마지막으로 생성(그래야지 보임.)
            _textBox = GameObject.FindGameObjectWithTag("SafeBox");
        }
    }
}
