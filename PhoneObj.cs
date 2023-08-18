using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneObj : MonoBehaviour
{
    [SerializeField] GameObject _textBox;
    [SerializeField] string _twoText = "�̷��� Ȯ�� ȭ��� ���� �� ������ ��ư�� �ִ�.";
    [SerializeField] string _threeText = "Ȯ�� ȭ���� ���� ��쵵 �����Ѵ�.";
    [SerializeField] string _fourText = "������Ʈ�� ��Ʈ�� �ְų� �������� �����̴�.";
    [SerializeField] string _lastText = "�� ���� ������ ��ư�� Ŭ���غ���.";


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
        yield return new WaitForSeconds(4.0f);      // 15�ʷ� �߾��� �׽�Ʈ������ ���ΰ�
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
