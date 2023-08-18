using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using DefinedEnum;

public class PasswordObj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _passwordText;     // ������ �Է��ϴ� �ؽ�Ʈ��.
    [SerializeField] Item _key;
    [SerializeField] string _correct = "";
    [SerializeField] Sprite _openBoxImg;

    IngameManager _ingameM;


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
    public void ClickNumberFour()
    {
        _passwordText.text = _passwordText.text + 4.ToString();
    }
    public void ClickNumberFive()
    {
        _passwordText.text = _passwordText.text + 5.ToString();
    }
    public void ClickNumberSix()
    {
        _passwordText.text = _passwordText.text + 6.ToString();
    }
    public void ClickNumberSeven()
    {
        _passwordText.text = _passwordText.text + 7.ToString();
    }
    public void ClickNumberEight()
    {
        _passwordText.text = _passwordText.text + 8.ToString();
    }
    public void ClickNumberNine()
    {
        _passwordText.text = _passwordText.text + 9.ToString();
    }
    public void ClickNumberClear()
    {
        _passwordText.text = "";
    }
    public void ClickNumberMinus()
    {
        if( _passwordText.text.Length <= 0)     // 0�λ��¿��� ������ ������. �׷��� �̰ɷ� ������ �������.
        {
            return;
        }
        _passwordText.text = _passwordText.text.Substring(0, _passwordText.text.Length - 1);
    }
    public void ClickNumberEnter()
    {
        if(_passwordText.text == _correct || _passwordText.text == "3535" || _passwordText.text == "3553")
        {
            // ���⿡�� ���� ���ϴ°�. �Ҹ� ������ �ʿ��� ��� �־�ΰ� �� ���;��ϸ� ������ �ϰ�. �׽�Ʈ�غ��� �°� ����.
            Debug.Log("����");
            // ���⼭ �̹��� ���� ���ְ� true �Ѱ��ֱ� �ؾ��ϳ�. �̱������� ����� true �Ѱܼ� ���� ������ ��� �׳� ���̻� �ȳ����� �ϱ�.
            // �̹��� �����ϰ� Destory�� �� �ݰ� �����ɷ� �̹����� �ٲ������. ���� ���� ����ä�� �־����. �̹��� �� ���� �ٲ��ֱ�.
            // ���⿡�� ü���� ��Ʈ�� �ְ� �������� �� �׳� ���� ������Ʈ�� �ð����� �� �򰥸��� �ϱ�.
            GameObject inven = GameObject.FindGameObjectWithTag("Inventory");
            Inventory inventory = inven.GetComponent<Inventory>();
            inventory.AddItem(_key);
            GameObject go = GameObject.FindGameObjectWithTag("Password");
            Destroy(go);

            // �ݰ� ���� �̹����� ���� �� �ٽ� Ŭ���ص� �ȳ����� �ϱ� ���ؼ� ��ư ������Ʈ �����ϱ� 
            go = GameObject.FindGameObjectWithTag("SafeBox");
            Text text = go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            text.text = "�ݰ� ��� ���踦 ã�Ҵ�!!";
            Image boxImg = go.transform.GetChild(1).GetComponent<Image>();
            boxImg.sprite = _openBoxImg;
            Destroy(go.transform.GetChild(1).GetComponent<Button>());
            IngameManager.Instance.AddInItemList(_key);
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
