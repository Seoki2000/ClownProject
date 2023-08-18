using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickBed : MonoBehaviour, IPointerClickHandler
{
    public string _text = "사용감 없는 침대. 편해보인다.";

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("TextingBox"))     // 만약 이미 생성되어 있는 경우
        {
            Text textNum = GameObject.FindGameObjectWithTag("TextingBox").transform.GetChild(0).GetChild(0).GetComponent<Text>();       // 텍스트만 가져오기
            textNum.text = _text;
        }
        else
        {
            //Debug.Log(1);         맵에 없는 경우만 생성 
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Text txt = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();       // 두개 아래에 있어서 이렇게 받아와야함.
                                                                                        //Debug.Log(txt);     // 가져오는건 맞게 가져옴 
            txt.text = _text;       // 생성보다 이전으로 두니까 나오고 생성 다음줄에 적으면 두번 클릭해야지만 나옴
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
        }
    }
}
