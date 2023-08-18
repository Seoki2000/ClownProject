using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class TextColorChange : MonoBehaviour
{
    [SerializeField] Color _textColor;      // ȸ���� �⺻������ #808080 �̴�. 
    Text _text;                             // �ؽ�Ʈ �������� ���ؼ�

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void StartTextClick()
    {
        _text.color = _textColor;   // �ؽ�Ʈ�� ������ �ٲ۴�.
        Invoke("StartScene", 0.5f);  // 0.5�� �ڿ� StartScene�̶�� �Լ��� �����Ѵ�.
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
        if (GameObject.FindWithTag("LoadBGUI"))     // ���� �±׿� �ش��ϴ� BG�� �ִ� ���� 
        {
            GameObject go = GameObject.FindGameObjectWithTag("LoadBGUI");       // ���ӿ�����Ʈ ã�� �� 
            go.SetActive(true);                                                 // Ȱ��ȭ��Ų��.
        }
        else
        {
            GameObject go = GameObject.Instantiate(Resources.Load("LoadBG") as GameObject);       // �������� ���ҽ� ���Ͽ��� �����´�. 
            GameObject goPar = GameObject.FindGameObjectWithTag("CanvasTag");    // �ʿ� �ִ� ĵ������ ã���ش�.                                              
            go.transform.SetParent(goPar.transform);   // ��ġ�� ĵ���� ��ġ�� �����´�.
            RectTransform goRect = go.GetComponent<RectTransform>();    // ������Ʈ�� Ʈ������ ��������
            goRect.offsetMin = Vector2.zero; goRect.offsetMax = Vector2.zero;
            go.SetActive(true);
            goRect.localScale = new Vector3(1, 1, 1);     // ������ ������Ŵ� �������� 108�� �޶��� ���·� ���ͼ� �̰� ����ֱ� ���ؼ� �����
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
