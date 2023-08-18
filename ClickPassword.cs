using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPassword : MonoBehaviour
{
    GameObject _password;


    public void ClickForCheckPassword()
    {
        if(_password != null)       // �̹� ������ ���
        {
            _password.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load("Map2/PasswordImgBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");    
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);       // ĵ������ �ڽĳ���� ���������� ����(�׷����� ����.)
            _password = GameObject.FindGameObjectWithTag("Password");
        }
    }
}
