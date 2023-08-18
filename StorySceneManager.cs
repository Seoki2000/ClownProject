using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // ui �������� ���ؼ�(�ؽ�Ʈ)
using UnityEngine.Audio;    // ����� �÷��̸� ���ؼ�
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StorySceneManager : MonoBehaviour
{
    public enum StoryType       // ���丮�� �� �����ؾ��ؼ� switch���� �Ἥ �ҷ��� ��� 
    {
        LastMoment       = 0,       // ó�� ���ϴ� ���
        CantSpeak,                  // �� ���� �̾߱⿡ ���� Ÿ����
        Reservation,                // �����ϴ� ���
        Arrive,                     // �����ϴ� ���
        ClownSaid,                  // ���밡 ���ϴ� ���
        Last                        // ���� �� ������ �ؽ�Ʈ ���
    }


    [SerializeField] GameObject _bgImg;     // ��� �̹����� ���ؼ�

    [SerializeField] GameObject _manImg;        // ���ΰ� �̹����� ���ؼ� 
    [SerializeField] Sprite [] _Imgs;          // �׸� �ٲ��ֱ� ���ؼ� 

    [SerializeField] Text _narrationText;   // �����̼� �ؽ�Ʈ
    [SerializeField] Text _mainText;        // ���� �ؽ�Ʈ
    [SerializeField] Text _clownText;       // ���� �ؽ�Ʈ

    [SerializeField] AudioClip _callingSound;       // ��ȭ��
    [SerializeField] AudioClip _driveSound;         // ���� �õ���
    [SerializeField] AudioClip _typingSound;       // Ÿ���λ���
    AudioSource _audio;                            // ����� �ҽ� ������Ʈ�� ���ؼ�

    // ���� ��縦 �÷��� List�� �߰��ϱ⸸ �ϸ� ���̴� ����Ʈ�� ������.
    List<string> _speakSortList;     // ������� ��縦 �ֱ� ���ؼ� 
    List<string> _clownSpeakList;    // ���� ���
    List<string> _mainSpeakList;     // ���ΰ� ���


    StoryType _nowType;             // ���� ���丮 ����
    //RectTransform _storyBox;        // ���丮�ڽ� ��ƮƮ������ �������� ���ؼ�
    bool _isEnd = false;            // ������ �ؽ�Ʈ���� ���� �� �������� true�� ��ȯ
    bool _isStart;                  // �̰� update�� �־�δ� ���ϱ� ��� �����Ѵ�. �׷��� Ŭ�� �� bool�� true�� �ٲ㼭 �ѹ��� �����غ��� 
    float _delay = 0.15f;           // Ÿ���� �ð� delay �ɾ��ֱ� ���ؼ� 

    void Awake()
    {
        _audio = GetComponent<AudioSource>();       // �ڱ� �ڽſ� �پ��ִ� ������Ʈ ��������
        _speakSortList = new List<string>();
        _clownSpeakList = new List<string>();
        _mainSpeakList = new List<string>();
        _manImg.SetActive(false);       // ó������ ���ش�.
    }

    void Start()
    {
        _bgImg.SetActive(false);
        _nowType = StoryType.LastMoment;
        AddSpeak();
        _isStart= false;        // ó���� false�� �Ѵ�.
    }

    // �̰� �� �Լ��� ���� �� ��Ʈ���� ��������.
    // �� ó���� �����̼� ��Ʈ, ��ȭ ��Ʈ, �̵��ϴ� ��Ʈ, ���� ����, ������ ��� �̷��� 5���� �������� ���� �� ��Ʈ �Ϸ� �� ���� ��Ʈ ���� �̷���.
    void Update()
    {
        if (_isEnd)     // ���� �ؽ�Ʈ�� ���� �� �����ٸ� 
        {
            RectTransform rect = GetComponent<RectTransform>();  // ó������ �θ��� Rect�� �����Ծ��µ� �����ϱ� �� ���ϰ� ������.
            rect.anchoredPosition = new Vector2(0, -400); // Pos X, Pos Y�� ���ؼ� �ٲ��ش�.

            // ���⿡ Ŭ�� �� �������� �̵��ϰų� �ΰ��� ȭ������ �Ѿ��. 
            // _manImg�� Image ������Ʈ�� �������� Sprite�� �ٲ������.
            // �׸��� �ؽ�Ʈ�� �ٽ� �ٲٸ鼭 ���;��ϰ� �̰� ������ �� �������� �Ѿ�� ���� ȭ���� ���;��ϰ�
            // ����ȭ�� ������ �߿��ΰ� ���ϴ°� ���;���. ������ �� �Ѱ��ε� �̳� �� �ؽ�Ʈ ��Ʈ�� ��ĳ �ٲ���
            Image img = _manImg.GetComponent<Image>();
            rect = _manImg.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(780, 300);
            img.sprite = _Imgs[1];
            StartCoroutine(MainCalling());
        }
        // ����ġ���� ����ؼ� ��ȭ�� �ѱ�鼭 �غ� �����̴�. 
        switch (_nowType)
        {
            case StoryType.LastMoment:
                if(!_isStart)
                    StartCoroutine(Typing(StoryType.CantSpeak, _speakSortList[0]));       // �ڷ�ƾ ������
                break;
            case StoryType.CantSpeak:
                if (!_isStart)
                {
                    _manImg.SetActive(true);    // �̹��� ȭ���� �����ְ� 
                    RectTransform rect = GetComponent<RectTransform>();  // ó������ �θ��� Rect�� �����Ծ��µ� �����ϱ� �� ���ϰ� ������.
                    rect.anchoredPosition = new Vector2(100, -450); // Pos X, Pos Y�� ���ؼ� �ٲ��ش�.
                    StartCoroutine(Typing(StoryType.Reservation, _speakSortList[1]));       // �ڷ�ƾ ������
                }
                break;
            case StoryType.Reservation:
                if (!_isStart)
                {
                    StartCoroutine(Typing(StoryType.Arrive, _speakSortList[2]));       // �ڷ�ƾ ������
                }
                break;
            case StoryType.Arrive:
                 if (!_isStart)
                    StartCoroutine(Typing(StoryType.ClownSaid, _speakSortList[3]));       // �ڷ�ƾ ������
                break;
            case StoryType.ClownSaid:
                if (!_isStart)
                    StartCoroutine(Typing(StoryType.Last,_speakSortList[4]));       // �ڷ�ƾ ������
                break;
            case StoryType.Last:
                if (!_isStart)
                {
                    StartCoroutine(Typing(StoryType.Last, _speakSortList[5]));       // �ڷ�ƾ ������
                }
                break;
        }
    }

    private void AddSpeak()
    {
        // �����̼� �߰�      (0~5�� ��ȭ �� �����̼�, 6 ~ ���Ĵ� ��ȭ �� �����̼��̴�.)
        _speakSortList.Add("���Ÿ� ���� �� �ִٸ�...");
        _speakSortList.Add("�̰� �������Ե� �� ���ߴ� �̾߱��.");
        _speakSortList.Add("�׶� ��, ���ǿ��� ��� ���ο� ������ ã�� �־���.");
        _speakSortList.Add("�� ���߿�, �ž� �������� �ִ� ��Ż�� ī�並 ���� �Ǿ���.");
        _speakSortList.Add("�̰� �ذ�... ��� ��ȭ �����̿���.");
        _speakSortList.Add("���� ��ȣ�� ���ϰ� ������.");

        _speakSortList.Add("���� ��Ҹ��� �߶�������, �̻��ϰ� �Ҹ��� ���ƾ�.");
        _speakSortList.Add("������ �� ��Ҹ��� ���� �� ��н��׾�.");
        _speakSortList.Add("���� �Ҹ���ġ�� ��Ҹ��� ��Ż�� ����̶�� ��밨���� �Ѱ����� ���� ���� �����ؼ� �ž� �������� ���� �Ǿ���.");
        _speakSortList.Add("������ �����ڸ���, ���밡 �ڱ� �Ұ��� �����߾�.");
        _speakSortList.Add("���� �Ҹ���ġ�� ����� ���� ���� �� ����߾�.");
        _speakSortList.Add("���� ������ ���� �����⵵ ���� �׸� ��ġ�� �濡 ����.");
        _speakSortList.Add("�̶����� ��ſ���.");

        // ���� ��� �߰�
        _clownSpeakList.Add("�ȳ��ϼ���. ��Ż�� ��� ũ����Դϴ�.");
        _clownSpeakList.Add("���� ���ŵ� ������ �� �����ϴ�.");
        _clownSpeakList.Add("�������. ���� '��Ż�� ��� ũ���' �Ұ��� ���� �߿����Դϴ�.");
        _clownSpeakList.Add("�濡 ���ż�, ��۾ȳ��� ���� �����Ͻø� �˴ϴ�.");

        // ���ΰ� ��� �߰� 
        _mainSpeakList.Add("������ �ϰ� ��������. �ð��� ���� ���������?");
    }

    IEnumerator Typing(StoryType nextstory, string speak)
    {
        _isStart = true;     // true�� �ٲ��༭ ���Ŀ� �߰������� �� �Լ��� �����°��� ���´�.

        StartCoroutine(TypingWithSound(speak, _narrationText)); // �ڷ�ƾ���� ���� ���� ��ü
        yield return new WaitForSeconds(5.5f);     // 5�� ��ٸ���.

        if(speak == _speakSortList[5])      // ���� ������ ����� 
        {
            _isEnd = true;                  // �������� �˷��ش�. 
            Debug.Log(_isEnd);
        }
        else
        {                               
            // ������ ��簡 �ƴѰ�� 
            _nowType = nextstory;      // ���丮 ����� �������� �ѱ��. 
            _isStart = false;
            yield return new WaitForSeconds(_delay);
        }
        yield break;               // �̷��� ������ ������ StopCoroutine�ص� �������.
    }

    IEnumerator MainCalling()           // ��ȭ��ȭ�ϴ����
    {
        _isEnd = false;     // update������ �ݺ����ΰ��� ���� ���ؼ� 
        // ���� �ٸ� �ؽ�Ʈ���� �ʱ�ȭ���Ѽ� ȭ�鿡 ������ �ʰ� ���ش�.
        // �̷��� �ʱ�ȭ�����ִ� �κе��� ������ ������ �ʰ� �ִ�.
        _narrationText.text = "";
        _mainText.text = "";
        _clownText.text = "";

        _audio.clip = _callingSound;    // ���� ������Ʈ�� �ִ� Ÿ����ġ�� ���带 ��ȭ������ �ٲ۴�.
        _audio.Play();          // ��ȭ�� ���� ��� 
        yield return new WaitForSecondsRealtime(2.0f);      // 2�ʵ��� ��ٸ���
        _audio.Stop();      // ��ȭ���� ����.

        StartCoroutine(TypingWithSound(_clownSpeakList[0], _clownText));    // ��ǻ� �ؿ� �ּ��� �ִ� ������� �ʹ� �ߺ��Ǿ �ڷ�ƾ���� ���� �������.
        /*_audio.clip = _typingSound;                             // Ÿ���� ����� �ٲ��ش�.
        _audio.Play();
        for(int n = 0; n < _clownSpeakList[0].Length; n++)
        {
            _clownText.text = _clownSpeakList[0].Substring(0, n);
            yield return new WaitForSecondsRealtime(_delay);               
            // �ٽ� Ÿ������ �ѱ��ھ� ġ�� ȿ���� ��Ÿ����.
        }
        _audio.Stop();*/
        yield return new WaitForSecondsRealtime(4.5f);

        _clownText.text = "";     // �ؽ�Ʈ�� �� �����ش�.
        // �밡�� ����̱� ������ TextMeshPro�� �ƴ϶� ��Ʈ�� �ٷ� �ٲ��ִ°� ��ã�Ҵ�. �׷��� �� ������ ���ϰ� 3���� �ٸ� ��Ʈ�� ����
        // �ؽ�Ʈ�� ������� �� ��翡 ���缭 �� ��Ʈ�� ���� �ؽ�Ʈ�� �������� �� �����̴�. 
        // ��ȭ�ϴ� ����� ���⿡ ���� ������ �� �����̰� ���⼭ ��ü �̹����� �������� ������ �ϴ°� �Ѱ�, �׸��� ȭ�� 
        StartCoroutine(TypingWithSound(_speakSortList[6], _narrationText));   // �̰� ���� ��Ҹ��� �߶������� �����̼�
        yield return new WaitForSecondsRealtime(4.5f);  // ���� �ڷ�ƾ ������ ���ؼ� ��ٸ���

        StartCoroutine(TypingWithSound(_speakSortList[7], _narrationText));   // �̰� ������ ���ΰ� ���
        yield return new WaitForSecondsRealtime(4.5f);
        _narrationText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);

        StartCoroutine(TypingWithSound(_mainSpeakList[0], _mainText));  // �����ؽ�Ʈ�� �������� �Ѵ�.
        yield return new WaitForSecondsRealtime(4.5f);

        _mainText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);

        StartCoroutine(TypingWithSound(_clownSpeakList[1], _clownText));
        yield return new WaitForSecondsRealtime(4.5f);
        _clownText.text = "";

        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(TypingWithSound(_speakSortList[8], _narrationText));         // ���ο��� ������ ��� ��
        yield return new WaitForSecondsRealtime(11.5f); // 7.5�ʷ� �ϸ� �ؽ�Ʈ�� �� ������ �� 0.5�ʸ��� bg�� ����
        _audio.clip = _driveSound;      // ����ȿ���� 
        _audio.Play();
        _manImg.SetActive(false); // �̰� �Ѱ� �ǵ�ȴٰ� ���� �� �ؽ�Ʈ �ʱ�ȭ �ȶߴµ� ��������
        yield return new WaitForSecondsRealtime(2.0f);
        _audio.Stop();
        _narrationText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);
        _isStart = true; // �̰ɷ� ��� �̾����� ������ �ʰ� �������� 
        yield return StartCoroutine(MainArrive());
    }

    IEnumerator MainArrive()
    {
        ResetAllText();

        _bgImg.SetActive(true);     // ���⿡ bg�� �������� ���� ȭ���� ���� �̹����� ������ ��簡 ������ ��������.
        yield return new WaitForSecondsRealtime(5.5f);
        _audio.clip = _driveSound;
        _audio.Play();
        yield return new WaitForSecondsRealtime(2.0f);
        _audio.Stop();

        Image img = _bgImg.GetComponent<Image>();       // �̹��� ������Ʈ ��������
        img.sprite = _Imgs[3];                          // ���� �̹����� �ٲٱ�
        RectTransform rect = _bgImg.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(1600, 800);
        rect.anchoredPosition = new Vector2(0, 100);        // y�������� ��¦ �÷��ֱ� 1600 800�� y�� 100~130 ���̷�

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

        _audio.clip = _typingSound;     // �ε����� �Ҹ��� �־� �� �����̴�.
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

    IEnumerator TypingWithSound(string type, Text text)        // type�� ����θ� �װſ� �°� �ٷ� ��� ������. 
    {
        _audio.clip = _typingSound;
        _audio.Play();                               // �ڷ�ƾ�� ���缭 ���� �÷���
        for (int n = 0; n < type.Length; n++)       // �ݺ����� ������� ���̸�ŭ �ݺ�. _testText.Length�� �������� ������ ������.
        {
            text.text = type.Substring(0, n);     // ������� 0���� �Ѱ��� ���� �۾��� �ݺ��ؼ� �����ش�.
            yield return new WaitForSecondsRealtime(_delay);                       // �ٷ� �ٷ� �����°� �ƴ϶� (n)��ŭ ��ٸ� �� �ݺ��� ���� 
        }
        _audio.Stop();          // Ÿ������ ���� �� ���� ��� ���� ���� 

        yield break;    // �ڷ�ƾ ����
    }


    public void ClickTextForNext()
    {
        _nowType = StoryType.Last; // ���� Ÿ�� ���� ���� Ÿ���� �����´�.
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

