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
                    temp.text = "��Ʈ�� ����.";
                    return;
                }
                if (_mapOneHintObj == null)
                {
                    GameObject nullobj = Resources.Load("Temp/HintBG") as GameObject;
                    Text temp = nullobj.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                    temp.text = "��Ʈ�� ���̾�.";     //txt.text = _hintList[0]; �̰� �־��ָ� ����. 
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
                    temp.text = "��Ʈ�� ���̾�.";
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
                    temp.text = "��Ʈ�� ���̾�.";
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
        // ����Ʈ�� ������ �ΰ��ӸŴ������� �ǵ帰�ٰ� �����ϰ� ���⼭�� �׻� ����Ʈ�� ù��°�� �־��ش�. ���� ���ٸ� ��Ʈ�� ���ٰ� ����. 
       /* if (_hintList == null)
        {
            GameObject hint = Resources.Load("Temp/HintBG") as GameObject;
            Text txt = hint.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            txt.text = "��Ʈ�� ���̾�.";
            canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _hintObj = GameObject.FindGameObjectWithTag("HintBG");
        }
        // �ð��� ���� �˷��ֱ�. 
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

    // �ؿ� �� �Լ� �Ⱦ����� �ٽ� �ּ�ó�� �� ������ �׳� ��Ʈ���� ��� �ι� �������� ���� ������ �ٷ� ���� ��Ʈ�� �����ִ°� �´ٰ� �����ؼ�
    // �ð�üũ������ ���� �׳� �����ϰ� �ٷ� �����͵� ��Ʈ Ŭ�� �� �˷��ִ� ������ ������.
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

    // init�� ���⼭ ���ְ� �̰� ���� �� �ʸ��� ������ ���ִ°ɷ� �ؾ��ϳ� ������ ���.  �׷��� �� init ��Ʈ��ư�� �ؼ� ������Ʈ �߰����ְ� ������ 
    public void HintMapOne()
    {
        _hintList.Add("���� ������ ����..");
    }
    public void HintMapTwo()
    {
        _hintList.Clear();  // �������� ��Ʈ�� ������ �ʱ� ���ؼ� �ϴ� �ʱ�ȭ�� �����ش�.
        _hintList.Add("���忡 ���� ������ ����.");
        _hintList.Add("��й�ȣ��... ���𰡸� ������ �°� ���ٸ�...?");
        _hintList.Add("���ڸ� �Է��� �����ϴϱ� ��� ���ڷ� ���Ű���.");
    }
    public void HintMapThree()
    {
        _hintList.Clear();  
        _hintList.Add("ī�޶� ã�ƾ� ��.");
        _hintList.Add("ī�޶� ������ �۵� �� ���� ???");
        _hintList.Add("��й�ȣ... ��� �����ұ� ??");
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
