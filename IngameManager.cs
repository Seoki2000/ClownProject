using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefinedEnum;

public class IngameManager : MonoBehaviour
{
    private static IngameManager instance = null;
    /*�ΰ��ӿ��� �ϴ� �ʿ��Ѱ͵� ���غ���
      Ŭ���ϴ� ���� �״�� �����ͼ� ����ϰ� 
      Prefabs�� ���� �����ͼ� �ϴ��� ������ ���ѵ־���.
      �ؽ�Ʈ ȭ��鵵 ���� �ϼ����Ѽ� ������ �ͼ� �����ؾ���.
      */
    // 07.13 �׳� �̰� ������ ������̶� ���� ������ ��������� ó�� �Ұ����� �̹� �ʹ� �ָ� �Ծ�. �� ��� ��ġ�°� �ƴϸ� �ҿ� ���
    // �� Dont Destory ������Ʈ�� ���� ���� �̳༮�� ������ �ְ� �����ϴ� ������� �ؾ���.  
    Inventory _inven;
    List<Item> _getItems;       // �̷��� �ϴ� �ص״µ� �ٸ� ��� �����غ��� �׳� DontDestory�� �Ἥ �̰� ���� ������ �Ѿ�� ������ ingameManager�� 
    // �ڽ����� ���̰� ���� ���� ���۵Ǹ� CanvasTag�� ã�Ƽ� �� ������ �ٿ��ָ� �� �ƴѰ� ��� ������ ���. �� ������ �κ��丮�� �ƴ� ����, ��Ʈ �ý��ۿ�
    // ���� �غ� �����̴�. ��...

    public GameType _nowGameScene; // ���� �÷��� ���� Ȯ���ϱ� ���ؼ� 
    public float _checkPlayTime;   // ��ü �÷��� Ÿ�� üũ�� ���� 
    public bool _isHiddenMap = false;   // �ι�°�ʿ��� ���� ������Ʈ�� ������ Ŭ���� ��� ������ ������� �� �� �ְ� �Ѵ�.
    int _minCount;          // 1�и��� ����
    float _playTime;        // �÷��� Ÿ�� ����� ����
    float _clownTime = 300; // ���� ���� �ð��� �� 5�и��� �̴�. 
    float _clownCount;      // ���� ���� ī��Ʈ�� üũ�ϱ� ���ؼ� 
    int _playerHP = 6;     // �÷��̾� ü�� 6 (10�п� �Ѱ��� �پ���.)
    bool _isMapChange = false;     // �� �ٲ���� �ȹٲ���� üũ�ؼ� �ɾ��ִ� �뵵�� ����ϱ�. 
    bool _endingFinish = true;

    // �Ź� ���带 �ٲ� ������ ������ �ϰ� �� ������ init�� �ؼ� ������Ų��..
    public float _bgmVal;      //bgm���� �Ѱܹޱ� ���ؼ�
    public float _sfxVal;      //sfx���� �Ѱܹޱ� ���ؼ� 


    RectTransform _optionPos;
    RectTransform _hintPos;
    GameObject _mobileExit;

    public static IngameManager Instance
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

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        _getItems = new List<Item>();
    }
    private void Start()
    {
        // �ϴ� �ӽ÷� ���Ƶΰ�StartCoroutine("PlayTime");     // �ð� üũ ����     
        _nowGameScene = GameType.MapOne;
        HintSystem.Instance.HintMapOne();

        _optionPos = GameObject.FindGameObjectWithTag("OptionSystem").GetComponent<RectTransform>();
        _hintPos = GameObject.FindGameObjectWithTag("HintSystem").GetComponent<RectTransform>();
    }

    void Update()
    {
        if (_playerHP <= 0)
        {
            // �ٷ� ���������� �Ѱܹ����� �̹����� ���� �������ɷ� �ٲ��ֱ�.
        }
        _checkPlayTime += Time.deltaTime;   // �̰ɷ� �ð�üũ�ϰ� 

        if (_checkPlayTime >= 60)
        {
            _minCount++;
            _checkPlayTime = 0;
        }
        switch (_nowGameScene)
        {
            case GameType.MapOne:
                break;
            case GameType.MapTwo:
                if (!_isMapChange)
                {
                    // ���⿡ ���� �ɼ� �ٽ� ����
                    //InitButtonsOnLoad(true);
                    StartCoroutine(InitButtonsOnLoad());
                    _playerHP = _playerHP - 2;
                }
                break;
            case GameType.MapThree:
                if (_isMapChange)
                {
                    Debug.Log(1);
                    // ���⿡ ���� �ɼ� �ٽ� ����
                    StartCoroutine(InitButtonsOnLoad());
                }
                break;
            case GameType.HiddenMap:
                if (!_isMapChange)
                {
                    StartCoroutine(InitButtonsOnLoad());
                }
                break;
            case GameType.Ending:
                // ���⼭ ResultWndColor�� �̱������� ���������� �̰� �����ͼ� ���� �ɸ� �ð��� ���� �̹��� �ٸ��ɷ� �ٲ��ֱ�.
                if (_endingFinish)
                {
                    StartCoroutine(EndingSceneImg());
                }
                break;
            case GameType.Result:
                break;
        }
    }

    /*    public void CheckInven()
        {

            foreach (Item num in _inven.items)        // �κ��丮�� �������� �ִ��� Ȯ��.
            {
                if (num.itemName == "Key")
                {
                    //_inven.items.RemoveAt(num);
                }
            }
            _inven.FreshSlot();
        }*/

    // Ŭ��ī��Ʈ�� ������ ��� 
    public void ClownSpawn()
    {
        _clownCount--;
        GameObject clown = Resources.Load("Temp/ClownBG") as GameObject;         // ���� ��������
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");      // ĵ���� ��������.
        Instantiate(clown, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent); // ĵ���� �������� �������༭ ���� ���� ���̰� �ϱ�.
    }
    // �̰ɷ� ���ݱ��� �ɸ� �ð� üũ�ϰ� �ϱ�. 
    public void ResultTime(Text minText, Text secText)
    {
        Debug.Log(_checkPlayTime);
        _minCount = 34;     // �ӽ÷� �ø��Ŷ� �׳� �̰� ���� �ص� ��������. 
        // �Ҽ��� ���� ������.
        int sec = Mathf.FloorToInt(_checkPlayTime);

        minText.text = _minCount.ToString();
        secText.text = sec.ToString();

    }
    // �̰ɷ� ������ ���â�� ���ݱ��� �� ȹ���ߴ��� Ȯ���ϰ� �ϴ°� ������. test�� _key �־���.
    public void ResultWndInven()
    {
        Debug.Log(_getItems[0]);
        GameObject go = GameObject.FindGameObjectWithTag("Inventory");
        Inventory inven = go.GetComponent<Inventory>();
        for (int n = 0; n < _getItems.Count; n++)
        {
            inven.AddItem(_getItems[n]);
        }
    }
    // �̰ɷ� �ٸ������� Instance�ؼ� ������ �߰��ؼ� ���߿� ȹ�� �ϰ� ������� �̰ɷ� ����Ʈ�� �߰��صּ� ���â�� ���̰� �� �� ����.
    public void AddInItemList(Item item)
    {
        // �̷��� ������ ����Ʈ ����� �ΰ� ������ ���â���� ���� �κ��丮 ���� �ű⿡ ���� �Ѱ� �Ѱ��� ���ʴ�� �־��ָ� �ȴٰ� ������.
        _getItems.Add(item);
    }

    // ���� ������ �Ѿ� �� �� ������ش�. �ε��� ������� ������. ��ư �ΰ��� ������� �� �̰� �ڷ�ƾ���� �ٲ��༭ ���� ������ �Ѿ�� �ð��� ���缭 �����µ�
    // �̰� �ڿ������� �ٷ� �ִ���ó�� ���δ�. 
    /* public void InitButtonsOnLoad(bool change)
     {
         Debug.Log(3);
         _isMapChange = change;

         GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag") as GameObject;

         GameObject hint = Resources.Load("Button/HintButton") as GameObject;
         Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);

         GameObject option = Resources.Load("Button/OptionButton") as GameObject;
         Instantiate(option, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);

         RectTransform hintRect = hint.GetComponent<RectTransform>();
         RectTransform optionRect = option.GetComponent<RectTransform>();

         if (_nowGameScene == GameType.MapTwo)
         {
             Debug.Log(4);
             hintRect.anchoredPosition = new Vector2(-400, -26);
             optionRect.anchoredPosition = new Vector2(-200, -30);
             HintSystem.Instance.HintMapTwo();
         }
         if (_nowGameScene == GameType.MapThree)
         {
             Debug.Log(5);
             hintRect.anchoredPosition = new Vector2(-400, -26);
             optionRect.anchoredPosition = new Vector2(-200, -30);
             HintSystem.Instance.HintMapThree();
         }
         else
         {
             // ������ ���� ��� �ٽ� �κ��丮�� ���� ��Ȳ�̴� �׳� �״�� �ٽ� ������ ������.
             hintRect.anchoredPosition = _hintPos.anchoredPosition;
             optionRect.anchoredPosition = _optionPos.anchoredPosition;
         }
     }*/

    // �� �� �ΰ��� �ѱ� ������ ���� ���� �ٽ� ������Ű�� �ϴ� �����ؼ� �ٽ� �� �� Recttransform�� ���̰�
    // ������ ����� �ٽ� canvas �Ʒ��� ������ ���� �ǹ� x �׷��� ������ �����Ҵ�. ���߿� �� 
    /*public void ButtonsMoveOnLoad()
    {
        // �ϴ� ���� ������ �Ѿ�� ���� �� �Լ��� �ҷ��ͼ� �ϴ� �ΰ��ӿ� ��ư �ΰ� �� ã�ƿͼ� setparent�� �ΰ������� �ϰ� ���� ������ �Ѿ ��� ���ܵд�.
        GameObject hint = GameObject.FindGameObjectWithTag("HintSystem");
        GameObject option = GameObject.FindGameObjectWithTag("OptionSystem");
        hint.transform.SetParent(gameObject.transform);
        option.transform.SetParent(gameObject.transform);
    }
    public void ButtonMoveAfterLoad()
    {
        _isMapTwo = true;
        // �ι�° �ʺ��ʹ� �κ��丮�� �ֱ� ������ ������ �Ű�����Ѵ�. 

        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
        GameObject hint = GameObject.FindGameObjectWithTag("HintSystem");
        GameObject option = GameObject.FindGameObjectWithTag("OptionSystem");

        RectTransform hintRect = hint.GetComponent<RectTransform>();
        RectTransform optionRect = option.GetComponent<RectTransform>();

        hint.transform.SetParent(canvas.transform);
        option.transform.SetParent(canvas.transform);

        
        if (_nowGameScene == GameType.MapTwo || _nowGameScene == GameType.MapThree)
        {
            hintRect.anchoredPosition = new Vector2(-400, -26);
            optionRect.anchoredPosition = new Vector2(-200, -30);
        }
        else
        {
            // ������ ���� ��� �ٽ� �κ��丮�� ���� ��Ȳ�̴� �׳� �״�� �ٽ� ������ ������.
            hintRect.anchoredPosition = _hintPos.anchoredPosition;
            optionRect.anchoredPosition = _optionPos.anchoredPosition;
        }
    }*/
    /*public void MoveButtonPos()
    {
        // ���⼭ ��Ʈ ��ư, ���� ��ư�� �������� �Ű������. 
        GameObject hint = GameObject.FindGameObjectWithTag("HintSystem");
        RectTransform hintrect = hint.GetComponent<RectTransform>();
        // hintrect.
        hintrect.position = new Vector2(-400, -10);

        GameObject option = GameObject.FindGameObjectWithTag("OptionSystem");
        RectTransform optionrect = option.GetComponent<RectTransform>();
        optionrect.position = new Vector2(-200, -10);
        _isMapTwo = true;
    }*/
    public void PlayTimeFinish()        // �ð� üũ ���Ḧ ���ؼ�
    {
        _playTime = _checkPlayTime;     // �÷��� Ÿ���� �������ش�.
        StopCoroutine("PlayTime");      // �ڷ�ƾ�� �����. 
    }

    public void PlayerHPMinus()
    {
        _playerHP--;
        GameObject ballon = GameObject.FindGameObjectWithTag("Balloons");
        Debug.Log(_playerHP);
        GameObject go = ballon.transform.GetChild(_playerHP).gameObject;
        go.SetActive(false);
    }

    // �̰͵� ���� initbuttons�� ���鶧 ���� �ؼ� �־��. cnt�� �ǽð����� ���̴� ������ ���� HPMinus�� �������
    public void InitBollon()
    {
        GameObject go = Resources.Load("Map1/BalloonIconPos") as GameObject;
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
        Instantiate(go, canvas.transform.GetChild(0).transform.parent);       // ��� ���� Getchild(0)�ص� �������.
        GameObject ballon = GameObject.FindGameObjectWithTag("Balloons");
        List<GameObject> balloonList = new List<GameObject>();

        for (int n = 0; n < ballon.transform.childCount; n++)
        {
            balloonList.Add(ballon.transform.GetChild(n).gameObject);
            balloonList[n].SetActive(false);
        }

        for (int n = 0; n < _playerHP; n++)
        {
            //balloonList.Add(ballon.transform.GetChild(n).gameObject);
            balloonList[n].SetActive(true);
        }
    }
    IEnumerator InitButtonsOnLoad()
    {
        // �̰ɷ� ���� �����༭ ���� �ڷ�ƾ�� �� �������� �ʰ� ������.
        if (!_isMapChange)
        {
            _isMapChange = true;
        }
        else
        {
            _isMapChange = false;
        }

        yield return new WaitForSeconds(0.58f);      // �̷��� �ϸ� ���������� ���������. �׸��� ���� �� �����ͼ� ���� ���� �����ִ�.

        // ���� ������Ʈ ���� ��������
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag") as GameObject;
        GameObject hint = Resources.Load("Button/HintButton") as GameObject;
        GameObject option = Resources.Load("Button/OptionButton") as GameObject;

        // Recttransform �������ֱ�
        RectTransform hintRect = hint.GetComponent<RectTransform>();
        RectTransform optionRect = option.GetComponent<RectTransform>();

        // �κ��丮�� �ִ� ���� ���� ���� �����ؼ� �� ���� �°� ��ġ ����
        if (_nowGameScene == GameType.MapTwo || _nowGameScene == GameType.MapThree)
        {
            Debug.Log(4);
            hintRect.anchoredPosition = new Vector2(-400, -26);
            optionRect.anchoredPosition = new Vector2(-200, -30);
            //HintSystem.Instance.HintMapTwo();
        }
        else
        {
            // ������ ���� ��� �ٽ� �κ��丮�� ���� ��Ȳ�̴� �׳� �״�� �ٽ� ������ ������.
            hintRect.anchoredPosition = new Vector2(-220, -30);
            optionRect.anchoredPosition = new Vector2(-20, -26);
        }
        // ���� �� �ڷ�ƾ ����
        Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
        Instantiate(option, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
        InitBollon();
        //BalloonObjs.Instance.BallonListAdd(_playerHP);
        /*if(_getItems != null)       // �ٵ� �̷��� ȹ���ߴ� ��� �����۵��� �Ȼ�����µ� 
        {
            // �������� �ִ� ��� 
            for(int n = 0; n < _getItems.Count; n++)
            {
                Inventory.Instance.AddItem(_getItems[n]);
            }
        }  �̰� �� ����غ��߰ڴ�. ����Ʈ�� �Ѱ� �� �߰��ؼ� �Ȱ��� ȹ�� �� �־��ִµ� ���� ��� �� ��� �ٽ� �� ����Ʈ���� �����ְ� ���� �ʿ��� �ε��ҋ�
           �� ����Ʈ ���� ����Ÿ� üũ�ؼ� ����ϴ� �κ��丮�� �����ؼ� �����;��ҵ�.*/
        yield break;
    }


    IEnumerator PlayTime()
    {
        _checkPlayTime += Time.deltaTime;   // �ð��� üũ���ش�.   
        if (_checkPlayTime >= 60)
        {
            _minCount++;
            _checkPlayTime = 0;
        }
        yield return null;
    }

    IEnumerator ClowTimeCheck()
    {
        _clownTime -= Time.deltaTime;   // ���� ���� �ð� üũ�� ���ؼ�

        if (_clownTime <= 0)    // 0���� �۰ų� ������� 
        {
            _clownCount++;      // ī��Ʈ ���� �� ī��Ʈ �����Ҷ����� hp�� �����ϴ°ɷ� �ϰų� �� ī��Ʈ�� 6�̸� �����°ɷ� �Ѵٸ� �������ٰ� ������. 
            // ���� ������ ���⼭ init�� �ϰ� �����. 
            _clownTime = 300;   // ���� üũ Ÿ�� �ٽ� ������ 
        }
        yield return null;
    }


    // �̰� �ð��� �ʹ� �����ְ� �߸� ���� ��忣�� ��Ʈ�� ���� �־ �������� ���� ���� �÷��̿� ���� ������ �ٸ� �������� �ִ°ɷ� �ٲ���.
    IEnumerator EndingSceneImg()
    {
        _endingFinish = false;
        //yield return new WaitForSeconds(0.5f);
        if (_isHiddenMap)
        {
            Debug.Log("�����");
            ResultWndColor.Instance._ending = EndingType.BestEnding;
        }
        else
        {
            //ResultWndColor.Instance._ending = EndingType.WorstEnding;
            ResultWndColor.Instance._ending = (EndingType)Random.Range(0, 2);       //1~3��° ������ �������� ������. 
            Debug.Log("��������");
            Debug.Log(ResultWndColor.Instance._ending);
        }
        yield break;
    }


    // �� �ؿ� �ִ� �͵��� ũ�� �Ű� �Ƚᵵ ������.(�����ÿ���.)
    public void ExitButton()            // �ڷΰ��� ���� �� �����ϴ� �Լ�
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // ���� �ڷΰ��Ⱑ ���� ���
        {
            // ���⿡ �˾�â���� ���� �����ðڽ��ϱ� yes or no�� ������ ���ְ� 
            GameObject go = Resources.Load("MoblieExitButtonBG") as GameObject;      // ������ �ڽ� ��������.
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");       // ĵ���� ��������.
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // ���� ���� 
        }
    }

    public void HomeButton()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            // Ȩ��ư
        }
    }
    public void MenuButton()
    {
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            // �޴���ư
        }
    }
}
