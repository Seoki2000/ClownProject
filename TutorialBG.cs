using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBG : MonoBehaviour
{
    private static TutorialBG instance = null;
    public static TutorialBG Instance
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

    [SerializeField] string _nextSpeak = "핸드폰을 클릭해보자.";
    [SerializeField] string _lastSpeak = "이제 게임을 시작하죠. 행운을 빌어요.";
    GameObject _textBox;
    Text _mainText;

    public bool _isFinish = false;
    bool _isClick = false;  
    void Awake()
    {
        instance = this;
        _textBox = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        _mainText = _textBox.GetComponent<Text>();
    }
    void Start()
    {
        StartCoroutine(ChangeText());
    }
    private void Update()
    {
        if (_isFinish)
        {
            FinishTutorial();
        }
    }

    public void ClickPhone()
    {
        // 먼저 간단하게 튜토리얼을 진행해보자.
        // 핸드폰을 클릭해보자.
        // 잘 찾아보면서 진행해보자. 화면을 클릭하면 게임이 시작될거야.
        if (_isClick)
        {
            GameObject go = Resources.Load("Temp/PhoneObjBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _isClick = false;
        }
    }
    public void FinishTutorial()
    {
        _isFinish = false;
        _mainText.text = _lastSpeak;
        Destroy(gameObject,2.5f);
    }
    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(2.3f);
        _mainText.text = _nextSpeak;
        _isClick = true;
        yield break;
    }
}
