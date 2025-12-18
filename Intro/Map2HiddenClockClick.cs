using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Map2HiddenClockClick : MonoBehaviour, IPointerClickHandler
{
    // 여기서 시간 맞추는걸로 해서 다이어리에 시간 맞추고 넘어가야 하는걸로.
    // 이걸 열어야 옷장이 열리고 이후 갈 수 있게. 
    // 아니면 이걸 못풀면 마지막맵으로 못가고 바로 넘어가게 하고 배드엔딩 고정으로 
    // 나오게 만들고 끝내는것도. 그러면 맵이 세개지만 나중에 다시 플레이해서 이걸 하면
    // 맵이 사실은 4개였다. 이런식으로도 접근이 가능 해보인다.

    [SerializeField] int _totalCnt = 4;
    [SerializeField] AudioClip _clip;
    int _cnt;


    public void OnPointerClick(PointerEventData eventData) 
    {
        _cnt++;

        if(_cnt >= _totalCnt)           // 내가 원하는 만큼 카운트가 증가한 경우.
        {
            // 문이 열리거나 조건을 달성한걸로 하고 마지막 씬이 나오게 된다. 그게 아닐 경우 그냥 넘어간다.
            StartCoroutine(StartAudio());
        }
    }

    IEnumerator StartAudio()
    {
        // 루프는 어차피 체크해둬서 자동으로 돌아갈거임.
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = _clip;
        audio.Play();
        yield return new WaitForSeconds(0.8f);
        audio.Stop();
        IngameManager.Instance._isHiddenMap = true;
        yield break;
    }
}
