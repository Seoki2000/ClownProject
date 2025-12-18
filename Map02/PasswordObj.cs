using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using DefinedEnum;

public class PasswordObj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _passwordText;     // 정답을 입력하는 텍스트임.
    [SerializeField] Item _key;
    [SerializeField] string _correct = "";
    [SerializeField] Sprite _openBoxImg;

    IngameManager _ingameM;


    private void Update()
    {
        if (_passwordText.text.Length >= 5)
        {
            _passwordText.text = _passwordText.text.Substring(1, 4);        // 4개넘게 입력 할 경우, 다음입력값이 마지막에 오고 이전에 첫번째 값을 지워준다.
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
        if( _passwordText.text.Length <= 0)     // 0인상태에서 누르니 오류뜸. 그래서 이걸로 오류를 막아줬다.
        {
            return;
        }
        _passwordText.text = _passwordText.text.Substring(0, _passwordText.text.Length - 1);
    }
    public void ClickNumberEnter()
    {
        if(_passwordText.text == _correct || _passwordText.text == "3535" || _passwordText.text == "3553")
        {
            // 여기에서 내가 원하는거. 소리 나오고 필요한 요소 넣어두고 뭐 나와야하면 나오게 하고. 테스트해보니 맞게 나옴.
            Debug.Log("성공");
            // 여기서 이미지 변경 해주고 true 넘겨주기 해야하나. 싱글톤으로 만들고 true 넘겨서 만약 성공한 경우 그냥 더이상 안나오게 하기.
            // 이미지 변경하고 Destory한 후 금고를 열린걸로 이미지를 바꿔줘야함. 옷장 역시 열린채로 있어야함. 이미지 다 따서 바꿔주기.
            // 여기에서 체스로 힌트를 주고 나머지는 다 그냥 함정 오브젝트로 시간끌기 및 헷갈리게 하기.
            GameObject inven = GameObject.FindGameObjectWithTag("Inventory");
            Inventory inventory = inven.GetComponent<Inventory>();
            inventory.AddItem(_key);
            GameObject go = GameObject.FindGameObjectWithTag("Password");
            Destroy(go);

            // 금고 열린 이미지로 변경 및 다시 클릭해도 안나오게 하기 위해서 버튼 컴포넌트 삭제하기 
            go = GameObject.FindGameObjectWithTag("SafeBox");
            Text text = go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            text.text = "금고를 열어서 열쇠를 찾았다!!";
            Image boxImg = go.transform.GetChild(1).GetComponent<Image>();
            boxImg.sprite = _openBoxImg;
            Destroy(go.transform.GetChild(1).GetComponent<Button>());
            IngameManager.Instance.AddInItemList(_key);
        }
        else
        {
            //여기에 정답이 맞는 경우 정답으로 처리하고 아닌 경우 오답이라고 하고 입력값 초기화
            _passwordText.text = "X";
            // 사운드도 넣어주기. 1.2초 적당하고 괜찮음.
            Invoke("ClickNumberClear", 1.2f);
        }
    }

}
