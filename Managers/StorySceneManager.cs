using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // ui 가져오기 위해서(텍스트)
using UnityEngine.Audio;    // 오디오 플레이를 위해서
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StorySceneManager : MonoBehaviour
{
    public enum StoryType       // 스토리를 쭉 설명해야해서 switch문을 써서 할려고 사용 
    {
        LastMoment       = 0,       // 처음 말하는 장면
        CantSpeak,                  // 말 못할 이야기에 대한 타이핑
        Reservation,                // 예약하는 장면
        Arrive,                     // 도착하는 장면
        ClownSaid,                  // 광대가 말하는 장면
        Last                        // 시작 전 마지막 텍스트 장면
    }


    [SerializeField] GameObject _bgImg;     // 펜션 이미지를 위해서

    [SerializeField] GameObject _manImg;        // 주인공 이미지를 위해서 
    [SerializeField] Sprite [] _Imgs;          // 그림 바꿔주기 위해서 

    [SerializeField] Text _narrationText;   // 나레이션 텍스트
    [SerializeField] Text _mainText;        // 메인 텍스트
    [SerializeField] Text _clownText;       // 광대 텍스트

    [SerializeField] AudioClip _callingSound;       // 통화음
    [SerializeField] AudioClip _driveSound;         // 차량 시동음
    [SerializeField] AudioClip _typingSound;       // 타이핑사운드
    AudioSource _audio;                            // 오디오 소스 컴포넌트를 위해서

    // 추후 대사를 늘려도 List에 추가하기만 하면 끝이니 리스트로 만들어둠.
    List<string> _speakSortList;     // 순서대로 대사를 주기 위해서 
    List<string> _clownSpeakList;    // 광대 대사
    List<string> _mainSpeakList;     // 주인공 대사


    StoryType _nowType;             // 현재 스토리 진행
    //RectTransform _storyBox;        // 스토리박스 렉트트랜스폼 가져오기 위해서
    bool _isEnd = false;            // 마지막 텍스트까지 전부 다 나왔으면 true로 변환
    bool _isStart;                  // 이게 update에 넣어두다 보니까 계속 실행한다. 그래서 클릭 시 bool을 true로 바꿔서 한번만 진행해보자 
    float _delay = 0.15f;           // 타이핑 시간 delay 걸어주기 위해서 

    void Awake()
    {
        _audio = GetComponent<AudioSource>();       // 자기 자신에 붙어있는 컴포넌트 가져오기
        _speakSortList = new List<string>();
        _clownSpeakList = new List<string>();
        _mainSpeakList = new List<string>();
        _manImg.SetActive(false);       // 처음에는 꺼준다.
    }

    void Start()
    {
        _bgImg.SetActive(false);
        _nowType = StoryType.LastMoment;
        AddSpeak();
        _isStart= false;        // 처음은 false로 한다.
    }

    // 이걸 걍 함수로 만들어서 각 파트별로 나눠야함.
    // 즉 처음에 나레이션 파트, 통화 파트, 이동하는 파트, 광대 설명, 마지막 장면 이렇게 5개로 나눠놓고 나서 각 파트 완료 후 다음 파트 실행 이렇게.
    void Update()
    {
        if (_isEnd)     // 만약 텍스트가 전부 다 끝났다면 
        {
            RectTransform rect = GetComponent<RectTransform>();  // 처음에는 부모의 Rect로 가져왔었는데 참조하기 더 편하게 수정함.
            rect.anchoredPosition = new Vector2(0, -400); // Pos X, Pos Y에 대해서 바꿔준다.

            // 여기에 클릭 시 다음으로 이동하거나 인게임 화면으로 넘어간다. 
            // _manImg에 Image 컴포넌트를 가져오고 Sprite를 바꿔줘야함.
            // 그리고 텍스트도 다시 바꾸면서 나와야하고 이게 끝나면 또 다음으로 넘어가서 산장 화면이 나와야하고
            // 산장화면 끝나면 삐에로가 말하는게 나와야함. 문제는 딱 한개인데 이놈에 그 텍스트 폰트를 어캐 바꾸지
            Image img = _manImg.GetComponent<Image>();
            rect = _manImg.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(780, 300);
            img.sprite = _Imgs[1];
            StartCoroutine(MainCalling());
        }
        // 스위치문을 사용해서 대화를 넘기면서 해볼 예정이다. 
        switch (_nowType)
        {
            case StoryType.LastMoment:
                if(!_isStart)
                    StartCoroutine(Typing(StoryType.CantSpeak, _speakSortList[0]));       // 코루틴 돌리기
                break;
            case StoryType.CantSpeak:
                if (!_isStart)
                {
                    _manImg.SetActive(true);    // 이미지 화면을 보여주고 
                    RectTransform rect = GetComponent<RectTransform>();  // 처음에는 부모의 Rect로 가져왔었는데 참조하기 더 편하게 수정함.
                    rect.anchoredPosition = new Vector2(100, -450); // Pos X, Pos Y에 대해서 바꿔준다.
                    StartCoroutine(Typing(StoryType.Reservation, _speakSortList[1]));       // 코루틴 돌리기
                }
                break;
            case StoryType.Reservation:
                if (!_isStart)
                {
                    StartCoroutine(Typing(StoryType.Arrive, _speakSortList[2]));       // 코루틴 돌리기
                }
                break;
            case StoryType.Arrive:
                 if (!_isStart)
                    StartCoroutine(Typing(StoryType.ClownSaid, _speakSortList[3]));       // 코루틴 돌리기
                break;
            case StoryType.ClownSaid:
                if (!_isStart)
                    StartCoroutine(Typing(StoryType.Last,_speakSortList[4]));       // 코루틴 돌리기
                break;
            case StoryType.Last:
                if (!_isStart)
                {
                    StartCoroutine(Typing(StoryType.Last, _speakSortList[5]));       // 코루틴 돌리기
                }
                break;
        }
    }

    private void AddSpeak()
    {
        // 나레이션 추가      (0~5는 통화 전 나레이션, 6 ~ 이후는 통화 후 나레이션이다.)
        _speakSortList.Add("과거를 돌릴 수 있다면...");
        _speakSortList.Add("이건 누구에게도 말 못했던 이야기야.");
        _speakSortList.Add("그때 난, 현실에서 벗어나 새로운 경험을 찾고 있었어.");
        _speakSortList.Add("그 와중에, 신안 송정리에 있는 방탈출 카페를 보게 되었어.");
        _speakSortList.Add("이게 왠걸... 쏘우 영화 컨셉이였어.");
        _speakSortList.Add("예약 번호를 급하게 눌렀어.");

        _speakSortList.Add("그의 목소리는 발랄했지만, 이상하게 소름이 끼쳤어.");
        _speakSortList.Add("하지만 그 목소리가 나를 더 흥분시켰어.");
        _speakSortList.Add("그의 소름끼치는 목소리와 방탈출 펜션이라는 기대감으로 한걸음에 나는 차를 운전해서 신안 송정리로 가게 되었어.");
        _speakSortList.Add("차에서 내리자마자, 광대가 자기 소개를 시작했어.");
        _speakSortList.Add("그의 소름끼치는 모습에 나는 더욱 더 흥분했어.");
        _speakSortList.Add("나는 광대의 말이 끝나기도 전에 그를 밀치고 방에 들어갔어.");
        _speakSortList.Add("이때까진 즐거웠어.");

        // 광대 대사 추가
        _clownSpeakList.Add("안녕하세요. 방탈출 펜션 크라운입니다.");
        _clownSpeakList.Add("지금 오셔도 괜찮을 거 같습니다.");
        _clownSpeakList.Add("어서오세요. 저는 '방탈출 펜션 크라운' 소개를 맡은 삐에로입니다.");
        _clownSpeakList.Add("방에 들어가셔서, 방송안내에 따라 진행하시면 됩니다.");

        // 주인공 대사 추가 
        _mainSpeakList.Add("예약을 하고 싶은데요. 시간이 언제 괜찮을까요?");
    }

    IEnumerator Typing(StoryType nextstory, string speak)
    {
        _isStart = true;     // true로 바꿔줘서 이후에 추가적으로 이 함수로 들어오는것을 막는다.

        StartCoroutine(TypingWithSound(speak, _narrationText)); // 코루틴으로 따로 만들어서 대체
        yield return new WaitForSeconds(5.5f);     // 5초 기다린다.

        if(speak == _speakSortList[5])      // 만약 마지막 대사라면 
        {
            _isEnd = true;                  // 끝난것을 알려준다. 
            Debug.Log(_isEnd);
        }
        else
        {                               
            // 마지막 대사가 아닌경우 
            _nowType = nextstory;      // 스토리 장면을 다음으로 넘긴다. 
            _isStart = false;
            yield return new WaitForSeconds(_delay);
        }
        yield break;               // 이렇게 끝내도 괜찮고 StopCoroutine해도 상관없다.
    }

    IEnumerator MainCalling()           // 전화통화하는장면
    {
        _isEnd = false;     // update문에서 반복적인것을 끊기 위해서 
        // 먼저 다른 텍스트들을 초기화시켜서 화면에 보이지 않게 해준다.
        // 이렇게 초기화시켜주는 부분들이 실제로 먹히지 않고 있다.
        _narrationText.text = "";
        _mainText.text = "";
        _clownText.text = "";

        _audio.clip = _callingSound;    // 현재 컴포넌트에 있는 타이핑치는 사운드를 통화음으로 바꾼다.
        _audio.Play();          // 통화음 사운드 출력 
        yield return new WaitForSecondsRealtime(2.0f);      // 2초동안 기다리기
        _audio.Stop();      // 통화음을 끈다.

        StartCoroutine(TypingWithSound(_clownSpeakList[0], _clownText));    // 사실상 밑에 주석에 있는 내용들이 너무 중복되어서 코루틴으로 따로 만들었다.
        /*_audio.clip = _typingSound;                             // 타이핑 사운드로 바꿔준다.
        _audio.Play();
        for(int n = 0; n < _clownSpeakList[0].Length; n++)
        {
            _clownText.text = _clownSpeakList[0].Substring(0, n);
            yield return new WaitForSecondsRealtime(_delay);               
            // 다시 타이핑을 한글자씩 치는 효과를 나타낸다.
        }
        _audio.Stop();*/
        yield return new WaitForSecondsRealtime(4.5f);

        _clownText.text = "";     // 텍스트를 다 날려준다.
        // 노가다 방식이긴 하지만 TextMeshPro가 아니라서 폰트를 바로 바꿔주는걸 못찾았다. 그래서 걍 돌려서 편하게 3개의 다른 폰트를 가진
        // 텍스트를 만들었고 각 대사에 맞춰서 그 폰트를 가진 텍스트만 보여지게 할 예정이다. 
        // 전화하는 장면을 여기에 전부 구현을 할 예정이고 여기서 전체 이미지를 산장으로 나오게 하는거 한개, 그리고 화면 
        StartCoroutine(TypingWithSound(_speakSortList[6], _narrationText));   // 이게 그의 목소리는 발랄했지만 나레이션
        yield return new WaitForSecondsRealtime(4.5f);  // 다음 코루틴 실행을 위해서 기다리기

        StartCoroutine(TypingWithSound(_speakSortList[7], _narrationText));   // 이거 다음이 주인공 대사
        yield return new WaitForSecondsRealtime(4.5f);
        _narrationText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);

        StartCoroutine(TypingWithSound(_mainSpeakList[0], _mainText));  // 메인텍스트로 보여지게 한다.
        yield return new WaitForSecondsRealtime(4.5f);

        _mainText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);

        StartCoroutine(TypingWithSound(_clownSpeakList[1], _clownText));
        yield return new WaitForSecondsRealtime(4.5f);
        _clownText.text = "";

        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(TypingWithSound(_speakSortList[8], _narrationText));         // 메인에서 마지막 대사 후
        yield return new WaitForSecondsRealtime(11.5f); // 7.5초로 하면 텍스트가 다 나오고 한 0.5초만에 bg가 나옴
        _audio.clip = _driveSound;      // 차량효과음 
        _audio.Play();
        _manImg.SetActive(false); // 이거 한개 건드렸다고 지금 또 텍스트 초기화 안뜨는데 어지럽네
        yield return new WaitForSecondsRealtime(2.0f);
        _audio.Stop();
        _narrationText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);
        _isStart = true; // 이걸로 계속 이어져서 나오지 않게 막아주자 
        yield return StartCoroutine(MainArrive());
    }

    IEnumerator MainArrive()
    {
        ResetAllText();

        _bgImg.SetActive(true);     // 여기에 bg가 나왔으니 이후 화면인 광대 이미지와 마지막 대사가 나오고 끝나야함.
        yield return new WaitForSecondsRealtime(5.5f);
        _audio.clip = _driveSound;
        _audio.Play();
        yield return new WaitForSecondsRealtime(2.0f);
        _audio.Stop();

        Image img = _bgImg.GetComponent<Image>();       // 이미지 컴포넌트 가져오기
        img.sprite = _Imgs[3];                          // 광대 이미지로 바꾸기
        RectTransform rect = _bgImg.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(1600, 800);
        rect.anchoredPosition = new Vector2(0, 100);        // y방향으로 살짝 올려주기 1600 800에 y는 100~130 사이로

        StartCoroutine(TypingWithSound(_clownSpeakList[2], _clownText));
        yield return new WaitForSecondsRealtime(6.5f);

        _clownText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);

        StartCoroutine(TypingWithSound(_speakSortList[9], _narrationText));
        yield return new WaitForSecondsRealtime(4.5f);

        StartCoroutine(TypingWithSound(_speakSortList[10], _narrationText));
        yield return new WaitForSecondsRealtime(4.5f);

        _narrationText.text = "";
        StartCoroutine(TypingWithSound(_clownSpeakList[3], _clownText));
        yield return new WaitForSecondsRealtime(4.5f);

        _clownText.text = "";
        StartCoroutine(TypingWithSound(_speakSortList[11], _narrationText));
        yield return new WaitForSecondsRealtime(3.5f);

        _audio.clip = _typingSound;     // 부딪히는 소리를 넣어 줄 예정이다.
        _audio.Play();
        yield return new WaitForSecondsRealtime(3.5f);
        _audio.Stop();

        _manImg.SetActive(false);
        _bgImg.SetActive(false);

        rect = gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        _narrationText.text = _speakSortList[12];

        yield return new WaitForSecondsRealtime(3.5f);

        //SceneManager.LoadScene("IngameTestScene");
        ChangeSceneManager.Instance.ChangeScene("IngameTestScene");
        yield break;
    }

    IEnumerator TypingWithSound(string type, Text text)        // type만 적어두면 그거에 맞게 바로 사용 가능함. 
    {
        _audio.clip = _typingSound;
        _audio.Play();                               // 코루틴에 맞춰서 사운드 플레이
        for (int n = 0; n < type.Length; n++)       // 반복문을 내용들의 길이만큼 반복. _testText.Length로 했을때는 문제가 없었다.
        {
            text.text = type.Substring(0, n);     // 내용들의 0부터 한개씩 다음 글씨를 반복해서 적어준다.
            yield return new WaitForSecondsRealtime(_delay);                       // 바로 바로 나오는게 아니라 (n)만큼 기다린 후 반복문 시작 
        }
        _audio.Stop();          // 타이핑이 전부 다 끝난 경우 사운드 종료 

        yield break;    // 코루틴 종료
    }


    public void ClickTextForNext()
    {
        _nowType = StoryType.Last; // 현재 타입 말고 다음 타입을 가져온다.
    }

    public void ResetAllText()
    {
        _clownText.text = "";
        _mainText.text = "";
        _narrationText.text = "";
    }

   /* IEnumerator OnTyping(float waitTime, string allText)
    {
        foreach (char temp in allText)
        {
            _text.text += temp;
            yield return new WaitForSeconds(waitTime);
        }
    }*/









}

