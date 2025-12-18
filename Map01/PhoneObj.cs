using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneObj : MonoBehaviour
{
    [SerializeField] GameObject _textBox;
    [SerializeField] string _twoText = "이렇게 확대 화면과 설명 및 나가기 버튼이 있다.";
    [SerializeField] string _threeText = "확대 화면이 없는 경우도 존재한다.";
    [SerializeField] string _fourText = "오브젝트에 힌트가 있거나 쓸데없는 내용이다.";
    [SerializeField] string _lastText = "자 이제 나가기 버튼을 클릭해보자.";


    Text _textObj;
    bool _nowExit = false;
    bool _finishSpeak = false;
    private void Awake()
    {
        _textObj = _textBox.GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(NextText());
    }
    private void Update()
    {
        if (_finishSpeak)
        {
            _textObj.text = _lastText;
            _nowExit = true;
        }
    }
    public void ClickExitButton()
    {
        if (_nowExit)
        {
            StopCoroutine(NextText());
            TutorialBG.Instance._isFinish = true;
            Destroy(gameObject);
        }
    }
    IEnumerator NextText()
    {
        yield return new WaitForSeconds(4.0f);      // 15초로 했었음 테스트때문에 줄인거
        _textObj.text = _twoText;
        yield return new WaitForSeconds(4.0f);
        _textObj.text = _threeText;
        yield return new WaitForSeconds(4.0f);
        _textObj.text = _fourText;
        yield return new WaitForSeconds(4.0f);
        _finishSpeak = true;
        yield return new WaitForSeconds(600);
    }
}
