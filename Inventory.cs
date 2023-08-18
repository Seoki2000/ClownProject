using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance = null;

    public List<Item> items;        // 아이템 넣어주는 리스트

    [SerializeField]
    private Transform slotParent;       // slot의 부모가 되는 Bag을 담을 곳 
    [SerializeField]
    private Slot[] slots;               // Bag의 하위에 등록된 Slot을 담을 곳 

    
    public static Inventory Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();     
        // 유니티 에디터에서 바로 작동을 하는 역할.처음 인벤토리에 소스를 등록하시면 Console창에 에러가 뜨지만 Bag을 넣어 주면
        // slots에 Slot들이 자동 등록
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        FreshSlot();
    }

    public void FreshSlot()     //아이템이 들어오거나 나가면 Slot의 내용을 다시 정리하여 화면에 보여 주는 기능을 하는 것.
    {
        int i = 0;      // 두개의 For문에 같은 i값을 사용하기 위해서 외부에 선언.
        for (; i < items.Count && i < slots.Length; i++)    // items에 들어가 있는 수만큼 slots에 차례대로 item을 넣어 줍니다.
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(item);
            FreshSlot();
        }
        else
        {
            Debug.Log("슬롯이 가득 차 있습니다.");
            // 나중에 여기서 텍스트파일을 가져와서 슬롯 가득 차 있다고 말해보자.
        }
    }
}
