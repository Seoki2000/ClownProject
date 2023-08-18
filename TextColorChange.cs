using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class TextColorChange : MonoBehaviour
{
    [SerializeField] Color _textColor;      // 회색은 기본적으로 #808080 이다. 
    Text _text;                             // 텍스트 가져오기 위해서

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void StartTextClick()
    {
        _text.color = _textColor;   // 텍스트의 색상을 바꾼다.
        Invoke("StartScene", 0.5f);  // 0.5초 뒤에 StartScene이라는 함수를 실행한다.
    }
    public void LoadTextClick()
    {
        _text.color = _textColor;
        LoadBox();
    }
    public void ExitTextClick()
    {
        _text.color = _textColor;
        Invoke("Exit", 0.5f);
    }
    public void StartScene()
    {
        //SceneManager.LoadScene("IngameTestScene");
        SceneManager.LoadScene("StoryScene");
    }
    public void LoadBox()
    {
        if (GameObject.FindWithTag("LoadBGUI"))     // 만약 태그에 해당하는 BG가 있는 경우는 
        {
            GameObject go = GameObject.FindGameObjectWithTag("LoadBGUI");       // 게임오브젝트 찾은 후 
            go.SetActive(true);                                                 // 활성화시킨다.
        }
        else
        {
            GameObject go = GameObject.Instantiate(Resources.Load("LoadBG") as GameObject);       // 프리팹을 리소스 파일에서 가져온다. 
            GameObject goPar = GameObject.FindGameObjectWithTag("CanvasTag");    // 맵에 있는 캔버스를 찾아준다.                                              
            go.transform.SetParent(goPar.transform);   // 위치는 캔버스 위치를 가져온다.
            RectTransform goRect = go.GetComponent<RectTransform>();    // 오브젝트의 트랜스폼 가져오기
            goRect.offsetMin = Vector2.zero; goRect.offsetMax = Vector2.zero;
            go.SetActive(true);
            goRect.localScale = new Vector3(1, 1, 1);     // 이전에 오류뜬거는 스케일이 108로 달라진 상태로 나와서 이걸 잡아주기 위해서 적어둠
        }
    }

    public void Exit()
    {
        /*EditorApplication.wantsToQuit +=
           () => EditorUtility.DisplayDialog("Unity Editor", "Are you sure to quit ?", "Yes", "No");*/
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
}
