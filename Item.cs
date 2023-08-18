using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]       // 이걸 사용하면 저장 후 유니티의 프로젝트창에서 마우스 우클릭으로 Item 파일을 생성할 수 있음.
public class Item : ScriptableObject            // 이거까지 적어줘야함 아니면 오류떠서 우클릭해도 안나옴.
{
    public string itemName;
    public Sprite itemImg;
}
