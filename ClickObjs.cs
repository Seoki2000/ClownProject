using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObjs : MonoBehaviour
{
    // ���� ���� ������ ��� �޾ƿͼ� �÷��� ���� �����ؼ� ���������� �ٲٰ� ���������� �ٲ�� ���ڱ� ���� Ƣ��ͼ� ���� �ϰ� ���嵵 ���Ҹ�������
    // �׸��� ���밡 ���� Ȯ���ϸ鼭 ������ Ŀ���� �ݺ� �� 3������ �ݺ� �� �����.

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
            if (!_stop) // �ڷ�ƾ ������ ���Ƽ� �ؽ�Ʈ�� ���ļ� �����°� �����ϱ� ���ؼ�
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
        // ���常 �־��ָ� �Ǵµ� �����...? �� ���°͸����ε� ����� ���....

        Color off = new Color(0, 0, 0, 0);
        Color On = new Color(1, 1, 1, 1);
        // ���� ���İ� �����ؼ� 1�� ����� 
        GameObject go = gameObject.transform.GetChild(0).gameObject;        // ���� �̹��� �������ִ� ������Ʈ ��������.
        Image clownImg = go.GetComponent<Image>();                          // ���� �̹��� ������Ʈ ��������.
        RectTransform Rect = go.GetComponent<RectTransform>();              // ������ ������ ���ؼ� ������Ʈ ��������.

        // ������ ����, ������, ��ٸ��� ������ �ݺ���.
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

        IngameManager.Instance.PlayerHPMinus();     // ü�� �Ѱ� �ٿ��ֱ�.
        
        //BalloonObjs.Instance.DeleteLastBalloon();   // ������ ������ �����ֱ�.

        Destroy(gameObject, 1.5f);
        yield break;
    }
    
}
