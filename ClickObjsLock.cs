using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class ClickObjsLock : MonoBehaviour, IPointerClickHandler
{
    // �����ϴ� ȿ������ �־����� ���ڴ� ���� �ݾҴٴ� ������ �ֱ� ���ؼ�.

    [SerializeField] List<string> _textList;
    GameObject _textBox;
    Text _textBoxTxt;

    private void Awake()
    {
        _textList.Add("�ƹ��� �غ��� ������ �ʴ´�..");
        _textList.Add("���� ����ִ�.");
        _textList.Add("�� �� �ִ� ����� ������ �ʴ´�.");
    }
        
    // �� �ؽ�Ʈ�� �־��༭ ���� ��Ʈ���� �������� ���.
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
