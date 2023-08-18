using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickBed : MonoBehaviour, IPointerClickHandler
{
    public string _text = "��밨 ���� ħ��. ���غ��δ�.";

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("TextingBox"))     // ���� �̹� �����Ǿ� �ִ� ���
        {
            Text textNum = GameObject.FindGameObjectWithTag("TextingBox").transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ؽ�Ʈ�� ��������
            textNum.text = _text;
        }
        else
        {
            //Debug.Log(1);         �ʿ� ���� ��츸 ���� 
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Text txt = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ΰ� �Ʒ��� �־ �̷��� �޾ƿ;���.
                                                                                        //Debug.Log(txt);     // �������°� �°� ������ 
            txt.text = _text;       // �������� �������� �δϱ� ������ ���� �����ٿ� ������ �ι� Ŭ���ؾ����� ����
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
        }
    }
}
