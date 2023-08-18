using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefinedEnum
{
    public enum ItemType
    {
        ItemUse             = 0,            // 획득할 수 있는 아이템
        DoorObj,                            // 클릭 시 아이템 확인 후 다음 맵으로 넘어가는 오브젝트
        Question,                           // 문제풀이
        HiddenObj,                          // 숨겨진 아이템
        FakeItme,                           // 장애물 아이템

        Count
    }

    public enum GameType
    {
        Intro           = 0,        // 인트로
        Main,                       // 메인화면
        Story,                      // 스토러 설명 화면
        MapOne,                     // 첫번째 맵
        MapTwo,                     // 두번째 맵
        MapThree,                   // 세번째 맵
        HiddenMap,                  // 이벤트 맵
        Ending,                     // 엔딩
        Result                      // 결과창
    }

    public enum EndingType
    {
        WorstEnding             = 0,        
        NotBadEnding,   
        NormalEnding,
        BestEnding
    }

    public enum GetItems        // 획득한 아이템 순서대로 어떤 것이 있는지. 아이템 총 9개로 할 예정이다. 
    {
        Null = 0,                   // 한개도 없는 경우 
        Key,                        // 열쇠
        battery,                    // 건전지
        remote,                     // 리모컨  




        Count                       // 결과창 윈도우에 붙어있는 스크립트 ResultWindow 보면 아이템 리스트가 있는데 이거와 순서를 맞게 해서 사용할 것이다.
            
    }
    public enum MapPlaying
    {
        None = 0,       // 첫 시작인 경우
        FindObj,        // 오브젝트를 찾은 경우
        ClearHide,      // 문제풀이를 클리어한 경우 
        GetKey,         // 다음 맵으로 넘어가는 열쇠를 얻은 경우 


        Count
    }

    public enum Map1Objects
    {
        Closet      = 0,        // 옷장
        TableObj,               // 책상
        Bed,                    // 침대
        Window,                 // 창문
        Light,                  // 전등
        Shoes,                  // 슬리퍼
        Flowerpot,              // 화분
        Carpet,                 // 카페트
        Chiffonier,             // 서랍임 
        BreakForNextStage       // 벽 여러번 클릭하는거
    }
}