using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefinedEnum;

public class IngameManager : MonoBehaviour
{
    private static IngameManager instance = null;
    /*인게임에서 일단 필요한것들 말해보면
      클릭하는 형태 그대로 가져와서 써야하고 
      Prefabs도 전부 가져와서 일단은 생성은 시켜둬야함.
      텍스트 화면들도 전부 완성시켜서 가지고 와서 정리해야함.
      */
    // 07.13 그냥 이거 어차피 강사님이랑 같이 수업때 만들었던것 처럼 불가능임 이미 너무 멀리 왔어. 다 뜯어 고치는거 아니면 소용 없어서
    // 걍 Dont Destory 오브젝트로 값들 전부 이녀석이 가지고 있고 조종하는 방식으로 해야함.  
    Inventory _inven;
    List<Item> _getItems;       // 이렇게 일단 해뒀는데 다른 방식 생각해보면 그냥 DontDestory를 써서 이걸 다음 씬으로 넘어가는 곳에서 ingameManager에 
    // 자식으로 붙이고 다음 씬이 시작되면 CanvasTag를 찾아서 그 밑으로 붙여주면 끝 아닌가 라는 생각도 들고. 이 생각을 인벤토리가 아닌 설정, 힌트 시스템에
    // 적용 해볼 예정이다. 음...

    public GameType _nowGameScene; // 현재 플레이 맵을 확인하기 위해서 
    public float _checkPlayTime;   // 전체 플레이 타임 체크용 변수 
    public bool _isHiddenMap = false;   // 두번째맵에서 히든 오브젝트를 여러번 클릭한 경우 마지막 히든맵을 갈 수 있게 한다.
    int _minCount;          // 1분마다 증가
    float _playTime;        // 플레이 타임 저장용 변수
    float _clownTime = 300; // 광대 출현 시간은 매 5분마다 이다. 
    float _clownCount;      // 광대 출현 카운트를 체크하기 위해서 
    int _playerHP = 6;     // 플레이어 체력 6 (10분에 한개씩 줄어든다.)
    bool _isMapChange = false;     // 걍 바뀐건지 안바뀐건지 체크해서 걸어주는 용도로 사용하기. 
    bool _endingFinish = true;

    // 매번 사운드를 바꿀 때마다 저장을 하고 이 값으로 init을 해서 생성시킨다..
    public float _bgmVal;      //bgm값을 넘겨받기 위해서
    public float _sfxVal;      //sfx값을 넘겨받기 위해서 


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
        // 일단 임시로 막아두고StartCoroutine("PlayTime");     // 시간 체크 시작     
        _nowGameScene = GameType.MapOne;
        HintSystem.Instance.HintMapOne();

        _optionPos = GameObject.FindGameObjectWithTag("OptionSystem").GetComponent<RectTransform>();
        _hintPos = GameObject.FindGameObjectWithTag("HintSystem").GetComponent<RectTransform>();
    }

    void Update()
    {
        if (_playerHP <= 0)
        {
            // 바로 엔딩씬으로 넘겨버리고 이미지는 제일 안좋은걸로 바꿔주기.
        }
        _checkPlayTime += Time.deltaTime;   // 이걸로 시간체크하고 

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
                    // 여기에 설정 옵션 다시 생성
                    //InitButtonsOnLoad(true);
                    StartCoroutine(InitButtonsOnLoad());
                    _playerHP = _playerHP - 2;
                }
                break;
            case GameType.MapThree:
                if (_isMapChange)
                {
                    Debug.Log(1);
                    // 여기에 설정 옵션 다시 생성
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
                // 여기서 ResultWndColor를 싱글톤으로 정리했으니 이걸 가져와서 지금 걸린 시간에 따라서 이미지 다른걸로 바꿔주기.
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

            foreach (Item num in _inven.items)        // 인벤토리에 아이템이 있는지 확인.
            {
                if (num.itemName == "Key")
                {
                    //_inven.items.RemoveAt(num);
                }
            }
            _inven.FreshSlot();
        }*/

    // 클론카운트가 증가한 경우 
    public void ClownSpawn()
    {
        _clownCount--;
        GameObject clown = Resources.Load("Temp/ClownBG") as GameObject;         // 광대 가져오기
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");      // 캔버스 가져오기.
        Instantiate(clown, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent); // 캔버스 마지막에 생성해줘서 제일 먼저 보이게 하기.
    }
    // 이걸로 지금까지 걸린 시간 체크하게 하기. 
    public void ResultTime(Text minText, Text secText)
    {
        Debug.Log(_checkPlayTime);
        _minCount = 34;     // 임시로 늘린거라서 그냥 이거 없이 해도 문제없음. 
        // 소수점 전부 버리기.
        int sec = Mathf.FloorToInt(_checkPlayTime);

        minText.text = _minCount.ToString();
        secText.text = sec.ToString();

    }
    // 이걸로 아이템 결과창에 지금까지 뭐 획득했는지 확인하게 하는거 가능함. test로 _key 넣었음.
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
    // 이걸로 다른데에서 Instance해서 아이템 추가해서 나중에 획득 하고 사라져도 이걸로 리스트에 추가해둬서 결과창에 보이게 할 수 있음.
    public void AddInItemList(Item item)
    {
        // 이렇게 아이템 리스트 만들어 두고 마지막 결과창에서 따로 인벤토리 만들어서 거기에 슬롯 한개 한개씩 차례대로 넣어주면 된다고 생각함.
        _getItems.Add(item);
    }

    // 다음 맵으로 넘어 갈 시 만들어준다. 로딩때 사라지기 때문에. 버튼 두개를 만들어준 후 이걸 코루틴으로 바꿔줘서 다음 맵으로 넘어가면 시간에 맞춰서 나오는데
    // 이게 자연스럽게 바로 있던것처럼 보인다. 
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
             // 마지막 맵인 경우 다시 인벤토리가 없는 상황이니 그냥 그대로 다시 끝으로 가야함.
             hintRect.anchoredPosition = _hintPos.anchoredPosition;
             optionRect.anchoredPosition = _optionPos.anchoredPosition;
         }
     }*/

    // 걍 이 두개로 넘길 생각은 하지 말고 다시 생성시키자 일단 실패해서 다시 들어간 후 Recttransform도 꼬이고
    // 오류도 생기고 다시 canvas 아래로 붙지도 않음 의미 x 그래도 생각은 괜찮았다. 나중에 더 
    /*public void ButtonsMoveOnLoad()
    {
        // 일단 다음 맵으로 넘어가기 전에 이 함수를 불러와서 일단 인게임에 버튼 두개 다 찾아와서 setparent를 인게임으로 하고 다음 맵으로 넘어간 경우 남겨둔다.
        GameObject hint = GameObject.FindGameObjectWithTag("HintSystem");
        GameObject option = GameObject.FindGameObjectWithTag("OptionSystem");
        hint.transform.SetParent(gameObject.transform);
        option.transform.SetParent(gameObject.transform);
    }
    public void ButtonMoveAfterLoad()
    {
        _isMapTwo = true;
        // 두번째 맵부터는 인벤토리가 있기 때문에 옆으로 옮겨줘야한다. 

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
            // 마지막 맵인 경우 다시 인벤토리가 없는 상황이니 그냥 그대로 다시 끝으로 가야함.
            hintRect.anchoredPosition = _hintPos.anchoredPosition;
            optionRect.anchoredPosition = _optionPos.anchoredPosition;
        }
    }*/
    /*public void MoveButtonPos()
    {
        // 여기서 힌트 버튼, 설정 버튼을 좌측으로 옮겨줘야함. 
        GameObject hint = GameObject.FindGameObjectWithTag("HintSystem");
        RectTransform hintrect = hint.GetComponent<RectTransform>();
        // hintrect.
        hintrect.position = new Vector2(-400, -10);

        GameObject option = GameObject.FindGameObjectWithTag("OptionSystem");
        RectTransform optionrect = option.GetComponent<RectTransform>();
        optionrect.position = new Vector2(-200, -10);
        _isMapTwo = true;
    }*/
    public void PlayTimeFinish()        // 시간 체크 종료를 위해서
    {
        _playTime = _checkPlayTime;     // 플레이 타임을 저장해준다.
        StopCoroutine("PlayTime");      // 코루틴을 멈춘다. 
    }

    public void PlayerHPMinus()
    {
        _playerHP--;
        GameObject ballon = GameObject.FindGameObjectWithTag("Balloons");
        Debug.Log(_playerHP);
        GameObject go = ballon.transform.GetChild(_playerHP).gameObject;
        go.SetActive(false);
    }

    // 이것도 역시 initbuttons로 만들때 같이 해서 넣어둠. cnt를 실시간으로 줄이는 내용은 위에 HPMinus로 사용하자
    public void InitBollon()
    {
        GameObject go = Resources.Load("Map1/BalloonIconPos") as GameObject;
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
        Instantiate(go, canvas.transform.GetChild(0).transform.parent);       // 사실 여기 Getchild(0)해도 상관없음.
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
        // 이걸로 먼저 막아줘서 이후 코루틴이 더 동작하지 않게 막아줌.
        if (!_isMapChange)
        {
            _isMapChange = true;
        }
        else
        {
            _isMapChange = false;
        }

        yield return new WaitForSeconds(0.58f);      // 이렇게 하면 정상적으로 만들어진다. 그리고 값도 다 가져와서 사운드 값이 남아있다.

        // 게임 오브젝트 먼저 가져오기
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag") as GameObject;
        GameObject hint = Resources.Load("Button/HintButton") as GameObject;
        GameObject option = Resources.Load("Button/OptionButton") as GameObject;

        // Recttransform 가져와주기
        RectTransform hintRect = hint.GetComponent<RectTransform>();
        RectTransform optionRect = option.GetComponent<RectTransform>();

        // 인벤토리가 있는 씬과 없는 신이 존재해서 각 씬에 맞게 위치 조정
        if (_nowGameScene == GameType.MapTwo || _nowGameScene == GameType.MapThree)
        {
            Debug.Log(4);
            hintRect.anchoredPosition = new Vector2(-400, -26);
            optionRect.anchoredPosition = new Vector2(-200, -30);
            //HintSystem.Instance.HintMapTwo();
        }
        else
        {
            // 마지막 맵인 경우 다시 인벤토리가 없는 상황이니 그냥 그대로 다시 끝으로 가야함.
            hintRect.anchoredPosition = new Vector2(-220, -30);
            optionRect.anchoredPosition = new Vector2(-20, -26);
        }
        // 생성 후 코루틴 종료
        Instantiate(hint, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
        Instantiate(option, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
        InitBollon();
        //BalloonObjs.Instance.BallonListAdd(_playerHP);
        /*if(_getItems != null)       // 근데 이러면 획득했던 모든 아이템들이 안사라지는데 
        {
            // 아이템이 있는 경우 
            for(int n = 0; n < _getItems.Count; n++)
            {
                Inventory.Instance.AddItem(_getItems[n]);
            }
        }  이건 좀 고민해봐야겠다. 리스트를 한개 더 추가해서 똑같이 획득 시 넣어주는데 만약 사용 한 경우 다시 그 리스트에서 지워주고 다음 맵에서 로딩할떄
           그 리스트 따로 만든거를 체크해서 사용하는 인벤토리로 생각해서 가져와야할듯.*/
        yield break;
    }


    IEnumerator PlayTime()
    {
        _checkPlayTime += Time.deltaTime;   // 시간을 체크해준다.   
        if (_checkPlayTime >= 60)
        {
            _minCount++;
            _checkPlayTime = 0;
        }
        yield return null;
    }

    IEnumerator ClowTimeCheck()
    {
        _clownTime -= Time.deltaTime;   // 광대 출현 시간 체크를 위해서

        if (_clownTime <= 0)    // 0보다 작거나 같을경우 
        {
            _clownCount++;      // 카운트 증가 걍 카운트 증가할때마다 hp가 감소하는걸로 하거나 걍 카운트로 6이면 끝나는걸로 한다면 문제없다고 생각함. 
            // 추후 출현은 여기서 init만 하게 만들기. 
            _clownTime = 300;   // 광대 체크 타임 다시 돌리기 
        }
        yield return null;
    }


    // 이게 시간이 너무 여유있고 잘못 깨면 배드엔딩 루트가 따로 있어서 그쪽으로 맵을 열고 플레이에 따라서 엔딩을 다른 느낌으로 주는걸로 바꾸자.
    IEnumerator EndingSceneImg()
    {
        _endingFinish = false;
        //yield return new WaitForSeconds(0.5f);
        if (_isHiddenMap)
        {
            Debug.Log("히든맵");
            ResultWndColor.Instance._ending = EndingType.BestEnding;
        }
        else
        {
            //ResultWndColor.Instance._ending = EndingType.WorstEnding;
            ResultWndColor.Instance._ending = (EndingType)Random.Range(0, 2);       //1~3번째 엔딩을 랜덤으로 보여줌. 
            Debug.Log("랜덤엔딩");
            Debug.Log(ResultWndColor.Instance._ending);
        }
        yield break;
    }


    // 이 밑에 있는 것들은 크게 신경 안써도 괜찮다.(갤럭시용임.)
    public void ExitButton()            // 뒤로가기 누를 시 종료하는 함수
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // 만약 뒤로가기가 눌린 경우
        {
            // 여기에 팝업창으로 정말 나가시겠습니까 yes or no를 나오게 해주고 
            GameObject go = Resources.Load("MoblieExitButtonBG") as GameObject;      // 나가기 박스 가져오기.
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");       // 캔버스 가져오기.
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // 질문 생성 
        }
    }

    public void HomeButton()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            // 홈버튼
        }
    }
    public void MenuButton()
    {
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            // 메뉴버튼
        }
    }
}
