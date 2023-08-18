using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DefinedEnum;

public class HintSystem : MonoBehaviour, IPointerClickHandler
{
    static HintSystem instance;

    public static HintSystem Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    GameObject canvas;
    GameObject _mapOneHintObj;
    GameObject _mapTwoHintObj;
    GameObject _mapThreeHintObj;
    List<string> _hintList;

    void Awake()
    {
        instance = this;

        _hintList = new List<string>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (IngameManager.Instance._nowGameScene)
        {
            case GameType.MapOne:
                if(_hintList == null)
                {
                    _mapOneHintObj.SetActive(true);
                    Text temp = _mapOneHintObj.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                    Debug.Log(temp);
                    temp.text = "힌트는 없어.";
                    return;
                }
                if (_mapOneHintObj == null)
                {
                    GameObject nullobj = Resources.Load("Temp/HintBG") as GameObject;
                    Text temp = nullobj.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                    temp.text = "힌트는 끝이야.";     //txt.text = _hintList[0]; 이걸 넣어주면 끝임. 
                    canvas = GameObject.FindGameObjectWithTag("CanvasTag");
                    Instantiate(nullobj, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
                    _mapOneHintObj = GameObject.FindGameObjectWithTag("HintBG");
                    RemoveList();
                }
                else
                {
                    _mapOneHintObj.SetActive(true);
                    RemoveList();
                }
                
                break;
            case GameType.MapTwo:
                if (_mapTwoHintObj == null)
                {
                    GameObject nullobj = Resources.Load("Temp/HintBG") as GameObject;
                    Text temp = nullobj.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                    temp.text = "힌트는 끝이야.";
                    canvas = GameObject.FindGameObjectWithTag("CanvasTag");
                    Instantiate(nullobj, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
                    _mapTwoHintObj = GameObject.FindGameObjectWithTag("HintBG");
                }
                else
                {
                    _mapTwoHintObj.SetActive(true);
                }
                break;
            case GameType.MapThree:
                if (_mapThreeHintObj == null)
                {
                    GameObject nullobj = Resources.Load("Temp/HintBG") as GameObject;
                    Text temp = nullobj.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                    temp.text = "힌트는 끝이야.";
                    canvas = GameObject.FindGameObjectWithTag("CanvasTag");
                    Instantiate(nullobj, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
                    _mapThreeHintObj = GameObject.FindGameObjectWithTag("HintBG");
                }
                else
                {
                    _mapThreeHintObj.SetActive(true);
                }
                break;

        }
        // 리스트는 어차피 인게임매니저에서 건드린다고 생각하고 여기서는 항상 리스트에 첫번째를 넣어준다. 만약 없다면 힌트는 없다고 나옴. 
       /* if (_hintList == null)
        {
            GameObject hint = Resources.Load("Temp/HintBG") as GameObject;
            Text txt = hint.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            txt.text = "힌트는 끝이야.";
            canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _hintObj = GameObject.FindGameObjectWithTag("HintBG");
        }
        // 시간에 따라 알려주기. 
        if (IngameManager.Instance._checkPlayTime >= 60)
        {
            GameObject hint = Resources.Load("HintBG") as GameObject;
            Text txt = hint.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            txt.text = _hintList[0];
            canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            RemoveList();
        }

        if (IngameManager.Instance._checkPlayTime >= 120)
        {
            GameObject hint = Resources.Load("HintBG") as GameObject;
            Text txt = hint.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            txt.text = _hintList[0];
            canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            RemoveList();
        }

        if (IngameManager.Instance._checkPlayTime >= 120)
        {
            GameObject hint = Resources.Load("HintBG") as GameObject;
            Text txt = hint.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            txt.text = _hintList[0];
            canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            RemoveList();
        }*/
    }

    // 밑에 이 함수 안쓸려고 다시 주석처리 한 이유는 그냥 힌트같은 경우 두번 연속으로 쓰고 싶으면 바로 다음 힌트를 보여주는게 맞다고 생각해서
    // 시간체크같은거 없이 그냥 무시하고 바로 다음것도 힌트 클릭 시 알려주는 것으로 생각함.
    /*public void CheckTime()
    {
        if(IngameManager.Instance._mapTwoPlayTime >= 60)
        {
            if (_mapTwoHintObj == null)
            {
                GameObject hint = Resources.Load("HintBG") as GameObject;
                Text txt = hint.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                txt.text = _hintList[0];
                canvas = GameObject.FindGameObjectWithTag("CanvasTag");
                Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            }
            else
            {
                _mapTwoHintObj.SetActive(true);
            }
            RemoveList();
        }
    }*/

    // init을 여기서 해주고 이걸 전부 각 맵마다 생성을 해주는걸로 해야하나 생각이 든다.  그러면 걍 init 힌트버튼을 해서 컴포넌트 추가해주고 끝낼까 
    public void HintMapOne()
    {
        _hintList.Add("벽이 수상해 보여..");
    }
    public void HintMapTwo()
    {
        _hintList.Clear();  // 이전방의 힌트가 나오지 않기 위해서 일단 초기화를 시켜준다.
        _hintList.Add("옷장에 뭔가 있을거 같아.");
        _hintList.Add("비밀번호는... 무언가를 순서에 맞게 본다면...?");
        _hintList.Add("숫자만 입력이 가능하니까 영어를 숫자로 볼거같아.");
    }
    public void HintMapThree()
    {
        _hintList.Clear();  
        _hintList.Add("카메라를 찾아야 해.");
        _hintList.Add("카메라를 여러번 작동 해 볼까 ???");
        _hintList.Add("비밀번호... 어떻게 봐야할까 ??");
    }

    public void RemoveList()
    {
        if(_hintList == null)
        {
            return;
        }
        else
        {
            //_hintList.Remove(_hintList[0]);
        }
        
    }



}
