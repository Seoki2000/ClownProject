using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonObjs : MonoBehaviour
{
    // 이거 걍 리스트 박은건 두고 
    private static BalloonObjs instance = null;

    List<GameObject> _balloonList;     // 풍선 6개 순서대로 넣어줄 예정임.
    
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

    public void BallonListAdd(int cnt)  // 리스트에 순서대로 넣어준다. cnt는 인게임에서 목숨으로 해두고 한개 없어질 때 바로 ClownSpawn 나와준다. 
    {
        Debug.Log(1);
        // cnt에 playerHp 넣어주면 맞을듯.
        for (int n = 0; n < cnt - 1; n++)
        {
            _balloonList.Add(gameObject.transform.GetChild(n).gameObject);
            _balloonList[n].SetActive(true);
        }
    }
    // init을 먼저 해주고 이후에 ListAdd를 해준다.
    public void InitBallonObject()
    {
        GameObject go = Resources.Load("Map1/BalloonIconPos") as GameObject;
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
        Instantiate(go, canvas.transform.GetChild(0).transform.parent);       //사실 여기 Getchild(0)해도 상관없음.

        for (int n = 0; n < gameObject.transform.childCount - 1; n++)
        {
            go = gameObject.transform.GetChild(0).gameObject;
            go.SetActive(false);
        }
    }
}
