using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickForGetCamera : MonoBehaviour, IPointerClickHandler
{
    // 일단 클릭 시 인벤토리에 획득해야함. 
    // 획득했으면 false가 되어서 걍 더이상 클릭 안하고 다시 클릭했다면 그냥 텍스트가 나오면서 좌측하단에 카메라 말고는 아무것도 없었다. 라고 나와야함.

    [SerializeField] Item _cameraItem;
    [SerializeField] GameObject _cameraObj;

    [SerializeField] string _textOne = "카메라 말고는 아무것도 없다.";
    string _textTwo = "카메라? 일단 챙겨보자.";
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
                // 두번째로 클릭하면 여기로 들어옴.
                GameObject go = Resources.Load("TextingBox") as GameObject;         // 리소스에서 가져오기
                GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");  // 캔버스 찾기
                Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // 텍스트파일 생성하는데 캔버스 가장 아래에 만들기.
                _obj = GameObject.FindGameObjectWithTag("TextingBox");
                _txt = _obj.transform.GetChild(0).GetChild(0).GetComponent<Text>();                               // 텍스트파일 내용 변경을 위해 가져옴
                _txt.text = _textOne;
                Invoke("SetFalse", 2.5f);
            }
        }
        else
        {
            GameObject go = Resources.Load("Map3/CameraGetBG") as GameObject;       // 파일 가져오고
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");      // 캔버스 찾고
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // 캔버스 가장 아래에 만들어주고 
            Text txt = GameObject.FindGameObjectWithTag("TextingBox").transform.GetChild(0).GetChild(0).GetComponent<Text>();       // 텍스트파일 가져오고
            txt.text = _textTwo;                                                        // 텍스트파일에 내용 넣어주고
            //Debug.Log(txt);
            //Debug.Log(_textTwo);
            //Debug.Log(txt.text);
            // 카메라 획득 
            GameObject inven = GameObject.FindGameObjectWithTag("Inventory");       // 인벤토리 가져오고
            Inventory inventory = inven.GetComponent<Inventory>();                  // 인벤토리 컴포넌트 가져오고
            inventory.AddItem(_cameraItem);                                         // 아이템 획득
            _isGet = true;                                                          // 이후 아이템 획득 못하게 넘기기
            IngameManager.Instance.AddInItemList(_cameraItem);
            // 카메라 버튼 활성화 
            _cameraObj.SetActive(true);
        }
    }

    public void SetFalse()
    {
        _obj.SetActive(false);
    }
}
