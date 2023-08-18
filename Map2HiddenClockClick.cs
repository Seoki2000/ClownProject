using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Map2HiddenClockClick : MonoBehaviour, IPointerClickHandler
{
    // ���⼭ �ð� ���ߴ°ɷ� �ؼ� ���̾�� �ð� ���߰� �Ѿ�� �ϴ°ɷ�.
    // �̰� ����� ������ ������ ���� �� �� �ְ�. 
    // �ƴϸ� �̰� ��Ǯ�� ������������ ������ �ٷ� �Ѿ�� �ϰ� ��忣�� �������� 
    // ������ ����� �����°͵�. �׷��� ���� �������� ���߿� �ٽ� �÷����ؼ� �̰� �ϸ�
    // ���� ����� 4������. �̷������ε� ������ ���� �غ��δ�.

    [SerializeField] int _totalCnt = 4;
    [SerializeField] AudioClip _clip;
    int _cnt;


    public void OnPointerClick(PointerEventData eventData) 
    {
        _cnt++;

        if(_cnt >= _totalCnt)           // ���� ���ϴ� ��ŭ ī��Ʈ�� ������ ���.
        {
            // ���� �����ų� ������ �޼��Ѱɷ� �ϰ� ������ ���� ������ �ȴ�. �װ� �ƴ� ��� �׳� �Ѿ��.
            StartCoroutine(StartAudio());
        }
    }

    IEnumerator StartAudio()
    {
        // ������ ������ üũ�صּ� �ڵ����� ���ư�����.
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = _clip;
        audio.Play();
        yield return new WaitForSeconds(0.8f);
        audio.Stop();
        IngameManager.Instance._isHiddenMap = true;
        yield break;
    }
}
