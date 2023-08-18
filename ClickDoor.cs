using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickDoor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string _text = "����ִ�. ���谡 �ʿ��� ���δ�.";
    bool _isKey = false;
    GameObject _Obg;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Ŭ�� �� ��� ���谡 ������ ���踦 ����ϰڳİ� ����� ��ư�� ����. ���� ��� �ؽ�Ʈ�ڽ� �����ͼ� ���踦 ã����� ��. 
        GameObject inven = GameObject.FindGameObjectWithTag("Inventory");
        Inventory inventory = inven.GetComponent<Inventory>();
        foreach(Item num in inventory.items)        // �κ��丮�� �������� �ִ��� Ȯ��.
        {
            if(num.itemName == "Key")
            {
                _isKey = true;
            }
        }

        if (_isKey)
        {
            // ��ư ���� 
            //Debug.Log("key����"); Ȯ������.
            // ���� ������ ���� ���
            Debug.Log("����������");
            IngameManager.Instance._nowGameScene = DefinedEnum.GameType.MapThree;
            Invoke("NextScene", 0.5f);      // ���� ��� ��ٸ� �� ���� ������.
        }
        else
        {
            if (_Obg != null)     // ���� �̹� �����Ǿ� �ִ� ���
            {
                _Obg.SetActive(true);
                Text textNum = _Obg.transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ؽ�Ʈ�� ��������
                textNum.text = _text;
                Invoke("GameObjectSetClose", 0.5f);
            }
            else
            {
                //�ʿ� ���� ��츸 ���� 
                GameObject go = Resources.Load("TextingBox") as GameObject;
                GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
                Text txt = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();       
                txt.text = _text;      
                Instantiate(go, canvas.transform.GetChild(0).transform.parent);
                _Obg = GameObject.FindGameObjectWithTag("TextingBox");
                Invoke("GameObjectSetClose", 0.5f);
            }
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Ingame3Scene");     // ���� ������ �Ѿ��
    }

    public void GameObjectSetClose()
    {
        _Obg.SetActive(false);
    }
}
