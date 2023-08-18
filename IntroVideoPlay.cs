using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class IntroVideoPlay : MonoBehaviour
{
    [SerializeField] GameObject _introImg;
    [SerializeField] GameObject _introVideo;

    float _checkTime;       //������ ��� �ð� üũ�� ���ؼ� 
    float _imgSetTime = 5;
    bool _isDone = false;

    void Update()
    {
        if (!_isDone)
        {
            _checkTime += Time.deltaTime;       // üũŸ�� �ð����� ����
        }
        if (_checkTime >= _imgSetTime)       // ���� �ð�üũ�� �̹����� ���;� �Ѵٸ�
        {
            _isDone = true;
            // ������ ���� �̹��� ���
            _checkTime = 0;

            PlayBGM();
            SetIntroImg();
        }
    }

    public void SetIntroImg()
    {
        //var canvasobj = GameObject.Find("Canvas");
        GameObject num = GameObject.FindGameObjectWithTag("CanvasTag");
        Destroy(_introVideo);
        if (_introImg == null)       // ���� ���� ���
        {
            // ���� ��� �������°ű����� �����ѵ� �������� �ȵ�. ���� ���������� �𸣰ڴµ� �׳� Xǥ�ð� ũ�� ����
            GameObject go = GameObject.Instantiate(Resources.Load("Intro/CheckTouch") as GameObject);      // ���ҽ� ���Ͽ��� ã�Ƽ� ���ӿ�����Ʈ �������� �����´�.
            go.transform.SetParent(num.transform);   // ��ġ�� ĵ���� ��ġ�� �����´�.
            RectTransform goRect = go.GetComponent<RectTransform>();    // ������Ʈ�� Ʈ������ ��������
            //goRect.pivot
            //goRect.sizeDelta = new Vector2(num.transform.position.x, num.transform.position.y);   // �̰� �´� ���� ����ΰ� ������ ���� �� �������.
            //goRect.position = Vector3.zero; �̷��� �ع����ϱ� �� ����ó�� �ȳ���
            goRect.offsetMin = Vector2.zero; goRect.offsetMax = Vector2.zero;   // �̰� ���� ��������� 
            // ĵ������ �ƿ� �ٿ��� �ϴ°ɷ� 
            go.SetActive(true);
        }
        else
        {
            _introImg.SetActive(true);
        }
    }
    public void PlayBGM()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void StopBGM()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();

    } 
    public void ChangeScene()       // �� ü����
    {
        StopBGM();
        SceneManager.LoadScene("MainScene");         // ���� ������ �̵� 
        //ChangeSceneManager.Instance.ChangeScene("MainScene");
        // �̰� UIMask�� ��ư ������Ʈ�� �߰����༭ Ŭ�� �� �̵����� �ٲ۴�. 
    }
}
