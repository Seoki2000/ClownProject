using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickAircondition : MonoBehaviour, IPointerClickHandler
{
    GameObject _airObj;
    bool _isGet;
    string _text = "Ŭ���� �߰��ؼ� �ָӴϿ� �־�״�";

    void Awake()
    {
        _isGet = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_airObj != null)
        {
            _airObj.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map2/AirConditionBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _airObj = GameObject.FindGameObjectWithTag("Aircondition");
        }
    }

    public void ClickForGetClip(Item item)       // ó�� Ŭ�� �ÿ��� Ŭ�� ������ ȹ�� 
    {
        if (!_isGet)
        {
            Debug.Log("Ŭ��");
            GameObject inven = GameObject.FindGameObjectWithTag("Inventory");
            Inventory inventory = inven.GetComponent<Inventory>();
            inventory.AddItem(item);
            _isGet = true;
            IngameManager.Instance.AddInItemList(item);
            GameObject go = GameObject.FindGameObjectWithTag("TextingBox");
            Text text = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            text.text = _text;
        }
    }
}
