using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickCameraButton : MonoBehaviour, IPointerClickHandler
{
    // 카메라 버튼 클릭 시 해야 하는 것.
    // 일단 먼저 카메라 이미지가 나옴. 그 후 배경 이미지 받아와서 거기에 이미지컴포넌트 받아오고 그 Alpha값을 60까지 조절해서 맵을 어둡게 만들어줌.
    // 원래 없었던게 이걸 클릭해서 보이는 느낌으로 가는거니까.... 음 냉장고에 안보이던 패턴 혹은 비밀번호 누르는게 보이게 되고 그 버튼을 클릭 시 
    // 비밀번호를 풀 수 있게 된다. 비밀번호가 틀린 경우 스택을 추가하고 10스택 이상일 경우 클리어 실패 및 다시하기 혹은 종료하기 버튼이 나오게 되게 한다. (미리 설명해준다.)

    // 일단 클릭이니까 이벤트시스템으로 받아와서 해보자 

    // 만든거는 끝났는데 여기서 클릭 시 보이게 해주고 그게 아닌 경우 다시 꺼주는 식으로 해서 맵에 숨겨진 오브젝트를 보이게 해준다.(가장 좌측에 있는 큰 서랍처럼 보이는 오브젝트에 밝은 비밀번호 누르는것처럼 보이는걸 만들어주고 그걸 클릭 시 비밀번호 누르기가 나오고
    // 비밀번호를 맞추면 문이 열리면서 마지막 엔딩맵으로 넘어가게 된다.

    GameObject _hiddenObjOne;          // 카메라 획득하자마자 누르면 안켜짐 두번째 버튼 클릭 시 켜짐.
    GameObject _hiddenObjTwo;
    GameObject _camButton;

    bool _isOn;

    private void Awake()
    {
        _isOn = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isOn)      // 켜져 있는 경우에 클릭 한 경우. 그러면 걍 카메라를 꺼버리고 setActive false를 박아주고 맵 이미지를 다시 밝에 해준다.
        {
            _camButton.SetActive(false);        // 카메라이미지 꺼주기.

            _hiddenObjOne.SetActive(false);        // 히든 오브젝트 끄기.
            _hiddenObjTwo.SetActive(false);        // 히든 오브젝트 끄기.

            ChangeMapImg(1);                    // 알파값 복원해주기 
            
            _isOn = false;
            return;
        }
        
        if (_camButton != null)     // 버튼을 눌려서 카메라를 활성화 시킨 경우
        {
            _camButton.SetActive(true);     // 카메라 켜주기

            _hiddenObjOne.SetActive(true);     // 히든오브젝트 켜주기
            _hiddenObjTwo.SetActive(true);        // 히든 오브젝트 끄기.

            ChangeMapImg(0.45f);            // 플레이 맵 알파값 변경 
            
            _isOn = true;                   // 현재 상태를 알려줌.
            // 여기에다가 좌측상단에 오브젝트를 가져와서 버튼같은게 보이고 이걸로 
        }
        else
        {
            // 카메라 이미지 캔버스 가장 밑에 자식으로 생성하기
            GameObject go = Resources.Load("Map3/CameraImg") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _camButton = GameObject.FindGameObjectWithTag("CameraImg");
            ChangeMapImg(0.45f);
            _hiddenObjOne = canvas.transform.GetChild(0).GetChild(5).gameObject;
            _hiddenObjTwo = canvas.transform.GetChild(0).GetChild(13).gameObject;
            Debug.Log(canvas.transform.childCount);
            _isOn = true;
        }
    }


    public void ChangeMapImg(float alphaValue)      // 여기서 맵에 이미지 가져오고 그 맵 이미지에 alphaValue로 값 조절하기 
    {
        GameObject go = GameObject.FindGameObjectWithTag("PlayMap");        // 메인맵 가져오기
        Image mapImg = go.GetComponent<Image>();
        mapImg.color = new Color(1, 1, 1, alphaValue);                            // 알파값 줄이기(어둡게 화면 변경.)
    }


}
