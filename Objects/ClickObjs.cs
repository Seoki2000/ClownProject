using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObjs : MonoBehaviour
{
    // 사운드 먼저 나오고 배경 받아와서 컬러값 점점 조절해서 검은색으로 바꾸고 검은색으로 바뀌면 갑자기 본인 튀어나와서 놀라게 하고 사운드도 비명소리나오고
    // 그리고 광대가 점점 확대하면서 꺼졌다 커졌다 반복 한 3번까지 반복 후 사라짐.

    Image _img;
    float _colorVal;
    bool _isDone;
    bool _stop;

    void Awake()
    {
        _isDone = false;
        _stop = false;  
        _img = gameObject.GetComponent<Image>();
    }
    void Update()
    {
        _colorVal += Time.deltaTime * 0.5f;

        if(_colorVal >= 1)
        {
            _isDone = true;
        }
        else
        {
            ChangeColor();
        }

        if (_isDone)
        {
            if (!_stop) // 코루틴 여러번 돌아서 텍스트랑 겹쳐서 나오는거 방지하기 위해서
            {
                StartCoroutine(ClownMove());
            }   
        }
    }

    public void ChangeColor()
    {
        _img.color = new Color(1 - _colorVal, 1 - _colorVal, 1 - _colorVal, 1);
    }

    IEnumerator ClownMove()
    {
        _stop = true;
        // 사운드만 넣어주면 되는데 무서운데...? 걍 보는것만으로도 무서운데 어라....

        Color off = new Color(0, 0, 0, 0);
        Color On = new Color(1, 1, 1, 1);
        // 먼저 알파값 조절해서 1로 만들고 
        GameObject go = gameObject.transform.GetChild(0).gameObject;        // 광대 이미지 가지고있는 오브젝트 가져오기.
        Image clownImg = go.GetComponent<Image>();                          // 광대 이미지 컴포넌트 가져오기.
        RectTransform Rect = go.GetComponent<RectTransform>();              // 스케일 조절을 위해서 컴포넌트 가져오기.

        // 스케일 변경, 켜지기, 기다리고 꺼지고 반복임.
        Rect.localScale = new Vector3(5.5f, 5.5f, 1);
        clownImg.color = On;
        yield return new WaitForSeconds(0.1f);
        clownImg.color = off;
        yield return new WaitForSeconds(0.1f);

        Rect.localScale = new Vector3(6.5f, 6.5f, 1);
        clownImg.color = On;
        yield return new WaitForSeconds(0.1f);
        clownImg.color = off;
        yield return new WaitForSeconds(0.1f);

        Rect.localScale = new Vector3(7.5f, 7.5f, 1);
        clownImg.color = On;
        yield return new WaitForSeconds(0.1f);
        clownImg.color = off;
        yield return new WaitForSeconds(0.1f);

        Rect.localScale = new Vector3(8.5f, 8.5f, 1);
        clownImg.color = On;
        yield return new WaitForSeconds(0.1f);
        clownImg.color = off;
        yield return new WaitForSeconds(0.1f);

        Rect.localScale = new Vector3(9.5f, 9.5f, 1);
        clownImg.color = On;
        yield return new WaitForSeconds(0.1f);
        clownImg.color = off;
        yield return new WaitForSeconds(0.1f);

        go = gameObject.transform.GetChild(1).gameObject;
        go.SetActive(true);

        IngameManager.Instance.PlayerHPMinus();     // 체력 한개 줄여주기.
        
        //BalloonObjs.Instance.DeleteLastBalloon();   // 마지막 아이콘 지워주기.

        Destroy(gameObject, 1.5f);
        yield break;
    }
    
}
