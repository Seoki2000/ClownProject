using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordForExit : MonoBehaviour
{
    [SerializeField] string _correct = "AC13";
    [SerializeField] Text _passwordText;
    [SerializeField] Sprite _changeMapImg;

    private void Update()
    {
        if (_passwordText.text.Length >= 5)
        {
            _passwordText.text = _passwordText.text.Substring(1, 4);        // 4���Ѱ� �Է� �� ���, �����Է°��� �������� ���� ������ ù��° ���� �����ش�.
        }

    }

    public void ClickNumberOne()
    {
        _passwordText.text = _passwordText.text + 1.ToString();
    }
    public void ClickNumberTwo()
    {
        _passwordText.text = _passwordText.text + 2.ToString();
    }
    public void ClickNumberThree()
    {
        _passwordText.text = _passwordText.text + 3.ToString();
    }
   
    public void ClickA()
    {
        _passwordText.text = _passwordText.text + "A";
    }
    public void ClickB()
    {
        _passwordText.text = _passwordText.text + "B";
    }
    public void ClickC()
    {
        _passwordText.text = _passwordText.text + "C";
    }
    public void ClickNumberClear()
    {
        _passwordText.text = "";
    }
    public void ClickNumberEnter()
    {
        if (_passwordText.text == _correct || _passwordText.text == "13AC")
        {
            Debug.Log("����");
            // �� �̹����� �ٲ������.
            GameObject go = GameObject.FindGameObjectWithTag("PlayMap");
            Image img = go.GetComponent<Image>();
            img.sprite = _changeMapImg;
            // �����غ��ϱ� ���⼭ ��� ���°� ���������ϳ�. 
            // �ƴϸ� �ΰ��� �Ŵ����� �� �� ������ �� ĵ�������� getchild�� �����ͼ� ã�� ������ setactive false�� �ٲ��ְ� ���⼭ true�� ��ȯ�����ִ°ɷ� �غ��� �ϴ�. 
            GoToLastMap.Instance.ClickButton();     // ��ũ��Ʈ �ѵα�.
            //���⼭ ���� ó�� 
            Destroy(gameObject);
        }
        else
        {
            //���⿡ ������ �´� ��� �������� ó���ϰ� �ƴ� ��� �����̶�� �ϰ� �Է°� �ʱ�ȭ
            _passwordText.text = "X";
            // ���嵵 �־��ֱ�. 1.2�� �����ϰ� ������.
            Invoke("ClickNumberClear", 1.2f);
        }
    }
}
