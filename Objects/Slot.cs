using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image _img;        // 이미지 컴포넌트를 넣어주기 위해서

    private Item _item;
    public Item item
    {
        get { return _item; }       // 슬롯의 아이템 정보를 넘겨줄 때 사용 
        set
        {
            _item = value;      // 아이템에 들어오는 정보의 값은 _item에 저장됩니다.
            if (_item != null)
            {
                _img.sprite = item.itemImg;
                _img.color = new Color(1, 1, 1, 1);
            }
            else
            {
                _img.color = new Color(1, 1, 1, 0);
            }
        }
        // 나중에 Inventory 스크립트에서 List<item? _items에 등록된 아이템이 있다면 itemImg를 img에 저장 그리고 img의 알파값을 1로하여 이미지를 표시
        // 만약 null(슬롯이 비어있으면) img의 알파값을 0으로 해서 화면에 표시하지 않습니다.
    }
}
