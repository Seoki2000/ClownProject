using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickForEnding : MonoBehaviour, IPointerClickHandler
{
    // 클릭 시 BG로 어둡게 하고 정말 나가겠습니까? 예 아니오 버튼 나오고 예 누르면 엔딩 보여주고 결과창 넘어가기, 아니요 누르면 게임 오브젝트 끄기.

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
