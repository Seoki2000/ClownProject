using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonObjs : MonoBehaviour
{
    // �̰� �� ����Ʈ ������ �ΰ� 
    private static BalloonObjs instance = null;

    List<GameObject> _balloonList;     // ǳ�� 6�� ������� �־��� ������.
    
    public static BalloonObjs Instance
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

    void Awake()
    {
        instance = this;
        _balloonList = new List<GameObject>();
    }

    public void BallonListAdd(int cnt)  // ����Ʈ�� ������� �־��ش�. cnt�� �ΰ��ӿ��� ������� �صΰ� �Ѱ� ������ �� �ٷ� ClownSpawn �����ش�. 
    {
        Debug.Log(1);
        // cnt�� playerHp �־��ָ� ������.
        for (int n = 0; n < cnt - 1; n++)
        {
            _balloonList.Add(gameObject.transform.GetChild(n).gameObject);
            _balloonList[n].SetActive(true);
        }
    }
    // init�� ���� ���ְ� ���Ŀ� ListAdd�� ���ش�.
    public void InitBallonObject()
    {
        GameObject go = Resources.Load("Map1/BalloonIconPos") as GameObject;
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
        Instantiate(go, canvas.transform.GetChild(0).transform.parent);       //��� ���� Getchild(0)�ص� �������.

        for (int n = 0; n < gameObject.transform.childCount - 1; n++)
        {
            go = gameObject.transform.GetChild(0).gameObject;
            go.SetActive(false);
        }
    }
}
