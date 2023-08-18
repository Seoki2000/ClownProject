using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickForGetCamera : MonoBehaviour, IPointerClickHandler
{
    // �ϴ� Ŭ�� �� �κ��丮�� ȹ���ؾ���. 
    // ȹ�������� false�� �Ǿ �� ���̻� Ŭ�� ���ϰ� �ٽ� Ŭ���ߴٸ� �׳� �ؽ�Ʈ�� �����鼭 �����ϴܿ� ī�޶� ����� �ƹ��͵� ������. ��� ���;���.

    [SerializeField] Item _cameraItem;
    [SerializeField] GameObject _cameraObj;

    [SerializeField] string _textOne = "ī�޶� ����� �ƹ��͵� ����.";
    string _textTwo = "ī�޶�? �ϴ� ì�ܺ���.";
    GameObject _obj;
    Text _txt;
    bool _isGet = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isGet)
        {
            if(_obj != null)
            {
                _obj.SetActive(true);
                _txt.text = _textOne;
                Invoke("SetFalse", 2.5f);
            }
            else
            {
                // �ι�°�� Ŭ���ϸ� ����� ����.
                GameObject go = Resources.Load("TextingBox") as GameObject;         // ���ҽ����� ��������
                GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");  // ĵ���� ã��
                Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // �ؽ�Ʈ���� �����ϴµ� ĵ���� ���� �Ʒ��� �����.
                _obj = GameObject.FindGameObjectWithTag("TextingBox");
                _txt = _obj.transform.GetChild(0).GetChild(0).GetComponent<Text>();                               // �ؽ�Ʈ���� ���� ������ ���� ������
                _txt.text = _textOne;
                Invoke("SetFalse", 2.5f);
            }
        }
        else
        {
            GameObject go = Resources.Load("Map3/CameraGetBG") as GameObject;       // ���� ��������
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");      // ĵ���� ã��
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // ĵ���� ���� �Ʒ��� ������ְ� 
            Text txt = GameObject.FindGameObjectWithTag("TextingBox").transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ؽ�Ʈ���� ��������
            txt.text = _textTwo;                                                        // �ؽ�Ʈ���Ͽ� ���� �־��ְ�
            //Debug.Log(txt);
            //Debug.Log(_textTwo);
            //Debug.Log(txt.text);
            // ī�޶� ȹ�� 
            GameObject inven = GameObject.FindGameObjectWithTag("Inventory");       // �κ��丮 ��������
            Inventory inventory = inven.GetComponent<Inventory>();                  // �κ��丮 ������Ʈ ��������
            inventory.AddItem(_cameraItem);                                         // ������ ȹ��
            _isGet = true;                                                          // ���� ������ ȹ�� ���ϰ� �ѱ��
            IngameManager.Instance.AddInItemList(_cameraItem);
            // ī�޶� ��ư Ȱ��ȭ 
            _cameraObj.SetActive(true);
        }
    }

    public void SetFalse()
    {
        _obj.SetActive(false);
    }
}
