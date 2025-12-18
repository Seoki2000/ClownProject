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
            Debug.Log("성공");
            // 맵 이미지도 바꿔줘야함.
            GameObject go = GameObject.FindGameObjectWithTag("PlayMap");
            Image img = go.GetComponent<Image>();
            img.sprite = _changeMapImg;
            // 생각해보니까 여기서 통로 여는거 만들어줘야하나. 
            // 아니면 인게임 매니저가 이 맵 시작할 때 캔버스에서 getchild로 내려와서 찾은 다음에 setactive false로 바꿔주고 여기서 true만 반환시켜주는걸로 해보자 일단. 
            GoToLastMap.Instance.ClickButton();     // 스크립트 켜두기.
            //여기서 사운드 처리 
            Destroy(gameObject);
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
