using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefinedEnum
{
    public enum ItemType
    {
        ItemUse             = 0,            // ȹ���� �� �ִ� ������
        DoorObj,                            // Ŭ�� �� ������ Ȯ�� �� ���� ������ �Ѿ�� ������Ʈ
        Question,                           // ����Ǯ��
        HiddenObj,                          // ������ ������
        FakeItme,                           // ��ֹ� ������

        Count
    }

    public enum GameType
    {
        Intro           = 0,        // ��Ʈ��
        Main,                       // ����ȭ��
        Story,                      // ���䷯ ���� ȭ��
        MapOne,                     // ù��° ��
        MapTwo,                     // �ι�° ��
        MapThree,                   // ����° ��
        HiddenMap,                  // �̺�Ʈ ��
        Ending,                     // ����
        Result                      // ���â
    }

    public enum EndingType
    {
        WorstEnding             = 0,        
        NotBadEnding,   
        NormalEnding,
        BestEnding
    }

    public enum GetItems        // ȹ���� ������ ������� � ���� �ִ���. ������ �� 9���� �� �����̴�. 
    {
        Null = 0,                   // �Ѱ��� ���� ��� 
        Key,                        // ����
        battery,                    // ������
        remote,                     // ������  




        Count                       // ���â �����쿡 �پ��ִ� ��ũ��Ʈ ResultWindow ���� ������ ����Ʈ�� �ִµ� �̰ſ� ������ �°� �ؼ� ����� ���̴�.
            
    }
    public enum MapPlaying
    {
        None = 0,       // ù ������ ���
        FindObj,        // ������Ʈ�� ã�� ���
        ClearHide,      // ����Ǯ�̸� Ŭ������ ��� 
        GetKey,         // ���� ������ �Ѿ�� ���踦 ���� ��� 


        Count
    }

    public enum Map1Objects
    {
        Closet      = 0,        // ����
        TableObj,               // å��
        Bed,                    // ħ��
        Window,                 // â��
        Light,                  // ����
        Shoes,                  // ������
        Flowerpot,              // ȭ��
        Carpet,                 // ī��Ʈ
        Chiffonier,             // ������ 
        BreakForNextStage       // �� ������ Ŭ���ϴ°�
    }
}