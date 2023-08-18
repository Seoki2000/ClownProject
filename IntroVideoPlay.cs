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

    float _checkTime;       //동영상 재생 시간 체크를 위해서 
    float _imgSetTime = 5;
    bool _isDone = false;

    void Update()
    {
        if (!_isDone)
        {
            _checkTime += Time.deltaTime;       // 체크타임 시간마다 증가
        }
        if (_checkTime >= _imgSetTime)       // 만약 시간체크가 이미지가 나와야 한다면
        {
            _isDone = true;
            // 동영상 끄고 이미지 출력
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
        if (_introImg == null)       // 만약 없는 경우
        {
            // 없는 경우 가져오는거까지는 가능한데 렌더링이 안됨. 무슨 오류인지는 모르겠는데 그냥 X표시가 크게 나옴
            GameObject go = GameObject.Instantiate(Resources.Load("Intro/CheckTouch") as GameObject);      // 리소스 파일에서 찾아서 게임오브젝트 형식으로 가져온다.
            go.transform.SetParent(num.transform);   // 위치는 캔버스 위치를 가져온다.
            RectTransform goRect = go.GetComponent<RectTransform>();    // 오브젝트의 트랜스폼 가져오기
            //goRect.pivot
            //goRect.sizeDelta = new Vector2(num.transform.position.x, num.transform.position.y);   // 이게 맞는 접근 방법인거 같은데 값을 잘 써줘야함.
            //goRect.position = Vector3.zero; 이렇게 해버리니까 걍 오류처럼 안나옴
            goRect.offsetMin = Vector2.zero; goRect.offsetMax = Vector2.zero;   // 이게 맞음 결과적으로 
            // 캔버스에 아예 붙여서 하는걸로 
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
    public void ChangeScene()       // 씬 체인지
    {
        StopBGM();
        SceneManager.LoadScene("MainScene");         // 메인 씬으로 이동 
        //ChangeSceneManager.Instance.ChangeScene("MainScene");
        // 이걸 UIMask에 버튼 컴포넌트에 추가해줘서 클릭 시 이동으로 바꾼다. 
    }
}
