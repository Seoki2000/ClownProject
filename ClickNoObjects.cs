using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickNoObjects : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<string> _textList;
    GameObject _Obg;

    private void Awake()
    {
        _textList.Add("�̰� �Ѿ��.");
        _textList.Add("�ٸ��� ã�ƺ���.");
        _textList.Add("�ƴ� �� ����.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Obg != null)     // ���� �̹� �����Ǿ� �ִ� ���
        {
            _Obg.SetActive(true);
            Text textNum = _Obg.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            textNum.text = _textList[Random.Range(0, _textList.Count)];
            Invoke("GameObjectSetClose", 1);
        }
        else
        {
            //�ʿ� ���� ��츸 ���� 
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Text txt = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            txt.text = _textList[Random.Range(0, _textList.Count)];
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
            _Obg = GameObject.FindGameObjectWithTag("TextingBox");
            Invoke("GameObjectSetClose", 1);
        }
    }

    public void GameObjectSetClose()
    {
        _Obg.SetActive(false);
    }
}
