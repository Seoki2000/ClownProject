using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class WndInvenItems : MonoBehaviour
{
    [SerializeField] GameObject _timeBox;

    void Start()
    {
        InitWnd();
    }


    public void InitWnd()
    {
        IngameManager.Instance.ResultWndInven();
        InitPlayTime();
    }

    public void InitPlayTime()
    {
        GameObject txtMin = _timeBox.transform.GetChild(1).gameObject;
        GameObject txtSec = _timeBox.transform.GetChild(2).gameObject;
        Text min = txtMin.GetComponent<Text>();
        Text sec = txtSec.GetComponent<Text>();

        IngameManager.Instance.ResultTime(min, sec);
    }
    public void ReStartButton()
    {
        SceneManager.LoadScene("MainSceneTest");
    }
    public void ExitButton()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
