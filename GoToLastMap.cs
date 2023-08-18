using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoToLastMap : MonoBehaviour, IPointerClickHandler
{
    private static GoToLastMap instance;
    public static GoToLastMap Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }


    private void Awake()
    {
        instance = this;
        gameObject.GetComponent<GoToLastMap>().enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ŭ�� �ÿ� ���� ������ �Ѿ�°ɷ�.
        StartCoroutine(NextScene());
    }
    public void ClickButton()
    {
        gameObject.GetComponent<GoToLastMap>().enabled = true;      // ��ũ��Ʈ �ѱ�
    }
    IEnumerator NextScene()
    {
        if (IngameManager.Instance._isHiddenMap)
        {
            yield return new WaitForSeconds(0.3f);
            IngameManager.Instance._nowGameScene = DefinedEnum.GameType.HiddenMap;
            SceneManager.LoadScene("EndingGameScene");
        }
        else
        {
            IngameManager.Instance._nowGameScene = DefinedEnum.GameType.Ending;
            SceneManager.LoadScene("ResultScene");
        }
        yield break;
    }
}
