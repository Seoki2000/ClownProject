using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickDoor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string _text = "잠겨있다. 열쇠가 필요해 보인다.";
    bool _isKey = false;
    GameObject _Obg;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 한 경우 열쇠가 있으면 열쇠를 사용하겠냐고 물어보는 버튼이 나옴. 없는 경우 텍스트박스 가져와서 열쇠를 찾으라고 함. 
        GameObject inven = GameObject.FindGameObjectWithTag("Inventory");
        Inventory inventory = inven.GetComponent<Inventory>();
        foreach(Item num in inventory.items)        // 인벤토리에 아이템이 있는지 확인.
        {
            if(num.itemName == "Key")
            {
                _isKey = true;
            }
        }

        if (_isKey)
        {
            // 버튼 생성 
            //Debug.Log("key있음"); 확인했음.
            // 문이 열리는 사운드 재생
            Debug.Log("다음맵으로");
            IngameManager.Instance._nowGameScene = DefinedEnum.GameType.MapThree;
            Invoke("NextScene", 0.5f);      // 사운드 재생 기다린 후 다음 맵으로.
        }
        else
        {
            if (_Obg != null)     // 만약 이미 생성되어 있는 경우
            {
                _Obg.SetActive(true);
                Text textNum = _Obg.transform.GetChild(0).GetChild(0).GetComponent<Text>();       // 텍스트만 가져오기
                textNum.text = _text;
                Invoke("GameObjectSetClose", 0.5f);
            }
            else
            {
                //맵에 없는 경우만 생성 
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
        SceneManager.LoadScene("Ingame3Scene");     // 다음 맵으로 넘어가기
    }

    public void GameObjectSetClose()
    {
        _Obg.SetActive(false);
    }
}
