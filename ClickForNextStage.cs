using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickForNextStage : MonoBehaviour, IPointerClickHandler
{
    public string _text = "���̴�...? ��";
    public string _lastText = "����.. �з�,..?...????";
    float _checkClickTime;
    float _breakTime = 3;

    public void OnPointerClick(PointerEventData eventData)
    {
        _checkClickTime++;

        if (_checkClickTime >= _breakTime)
        {
            Text textNum = GameObject.FindGameObjectWithTag("TextingBox").transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ؽ�Ʈ�� ��������
            textNum.text = _lastText;      // �� �� �״���ϱ� 
            IngameManager.Instance._nowGameScene = DefinedEnum.GameType.MapTwo;
            Invoke("NextScene", 0.5f);
        }

        if (GameObject.FindGameObjectWithTag("TextingBox"))     // ���� �̹� �����Ǿ� �ִ� ���
        {
            Text textNum = GameObject.FindGameObjectWithTag("TextingBox").transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ؽ�Ʈ�� ��������
            textNum.text = _text;
        }
        else
        {
            //Debug.Log(1);         �ʿ� ���� ��츸 ���� 
            GameObject go = Resources.Load("TextingBox") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Text txt = go.transform.GetChild(0).GetChild(0).GetComponent<Text>();       // �ΰ� �Ʒ��� �־ �̷��� �޾ƿ;���.
                                                                                        //Debug.Log(txt);     // �������°� �°� ������ 
            txt.text = _text;       // �������� �������� �δϱ� ������ ���� �����ٿ� ������ �ι� Ŭ���ؾ����� ����
            Instantiate(go, canvas.transform.GetChild(0).transform.parent);
        }
    }

    public void NextScene()
    {
        IngameManager.Instance._nowGameScene = DefinedEnum.GameType.MapTwo;     // ���� ������ ���ٴ� ������ �ֱ� .
        HintSystem.Instance.HintMapTwo();
        SceneManager.LoadScene("Ingame2Scene");
    }
}
