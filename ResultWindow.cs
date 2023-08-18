using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DefinedEnum;
using UnityEditor;
//using Microsoft.Unity.VisualStudio.Editor;
//using UnityEditorInternal;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] List<Sprite> _itemImgList;     // 아이템 이미지들을 가져오기 위해서 선언 
    [SerializeField] List<GameObject> _itemList;    // 아이템 이미지를 넣어줄 오브젝트 


    GetItems item;      //아이템 획득    인게임에서 이걸 넘겨주면 이걸 가지고 타입이 한개 증가할때마다 ++로 증가해서 한개씩 늘린다. 전부 다 있는 경우 

    private void Awake()
    {
        _itemImgList = new List<Sprite>();
        _itemList = new List<GameObject>();
    }

    // 결과창에서 사용

    // 이미지 리스트도 있고 게임 오브젝트 리스트도 있다. 지금까지 획득했는지 확인하고 만약 있다면 리스트 순서대로 이미지를 넣어준다.
    public void CheckGetItem()
    {
        // 일단 먼저 씬을 불러오는거기 때문에 무조건 있음. 없을 수가 없으니까 전부 다 있고 아이템 이미지들을 받아와야함.
        // 아이템 획득한게 너무 없어 즉 = 한개도 없는 경우 게임 오브젝트에 있는 것들을 전부 꺼버리면 된다.
        switch (item)           // 스위치문으로 현재 아이템에 따라서 어떤 것을 추가할지 선택한다. 
        {
            case GetItems.Null:
                break;
            case GetItems.Key:
                break;
            case GetItems.battery:
                break;
            case GetItems.remote:
                break;
            case GetItems.Count:        // 전부 다 획득한 경우 
                for (int n = 0; n < _itemList.Count; n++)
                {
                    Image img = _itemList[n].GetComponent<Image>();     // 리스트에 있는것들 한개 한개 다 컴포넌트 참조해와서
                    img.sprite = _itemImgList[n];                       // 이미지를 바꿔준다 
                }
                break;
        }
    }
    public void ShowResultWnd()
    {
        gameObject.SetActive(true);
    }



    // 버튼들
    public void ReStartGame()
    {
        //Application.LoadLevel("1_play");        // 빌드셋팅에 있는 젤 첫번째 씬을 다시 불러온다(즉 다시하기 누를 경우 사용)
        SceneManager.LoadScene("MainScene");            // 메인씬 불러오기
    }

    public void ExitGame()
    {
        Application.Quit();                                   // 실제 게임에서 종료 
        //EditorApplication.isPlaying = false;        // 에디터에서 종료. 테스트용이다. 
    }
}
