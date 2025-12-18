using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPassword : MonoBehaviour
{
    GameObject _password;


    public void ClickForCheckPassword()
    {
        if(_password != null)       // 이미 생성된 경우
        {
            _password.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map2/PasswordImgBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");    
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // 캔버스의 자식노드중 마지막으로 생성(그래야지 보임.)
            _password = GameObject.FindGameObjectWithTag("Password");
        }
    }
}
