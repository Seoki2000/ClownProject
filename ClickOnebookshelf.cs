using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickOnebookshelf : MonoBehaviour, IPointerClickHandler
{
    public string _text = "�Ҽ�å�� �������� �����Ǿ� �ִ�.";
    GameObject _Obg;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Obg != null)     // ���� �̹� �����Ǿ� �ִ� ���
        {
            _Obg.SetActive(true);
            Text textNum = _Obg.transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ؽ�Ʈ�� ��������
            textNum.text = _text;
            Invoke("GameObjectSetClose", 1.5f);
        }
        else
        {
            //�ʿ� ���� ��츸 ���� 
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Text txt = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ΰ� �Ʒ��� �־ �̷��� �޾ƿ;���.
            txt.text = _text;       // �������� �������� �δϱ� ������ ���� �����ٿ� ������ �ι� Ŭ���ؾ����� ����
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
            _Obg = GameObject.FindGameObjectWithTag("TextingBox");
            Invoke("GameObjectSetClose", 1.5f);
        }
    }

    public void GameObjectSetClose()
    {
        _Obg.SetActive(false);
    }
}
