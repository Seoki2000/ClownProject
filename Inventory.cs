using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance = null;

    public List<Item> items;        // ������ �־��ִ� ����Ʈ

    [SerializeField]
    private Transform slotParent;       // slot�� �θ� �Ǵ� Bag�� ���� �� 
    [SerializeField]
    private Slot[] slots;               // Bag�� ������ ��ϵ� Slot�� ���� �� 

    
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
        // ����Ƽ �����Ϳ��� �ٷ� �۵��� �ϴ� ����.ó�� �κ��丮�� �ҽ��� ����Ͻø� Consoleâ�� ������ ������ Bag�� �־� �ָ�
        // slots�� Slot���� �ڵ� ���
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        FreshSlot();
    }

    public void FreshSlot()     //�������� �����ų� ������ Slot�� ������ �ٽ� �����Ͽ� ȭ�鿡 ���� �ִ� ����� �ϴ� ��.
    {
        int i = 0;      // �ΰ��� For���� ���� i���� ����ϱ� ���ؼ� �ܺο� ����.
        for (; i < items.Count && i < slots.Length; i++)    // items�� �� �ִ� ����ŭ slots�� ���ʴ�� item�� �־� �ݴϴ�.
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
            Debug.Log("������ ���� �� �ֽ��ϴ�.");
            // ���߿� ���⼭ �ؽ�Ʈ������ �����ͼ� ���� ���� �� �ִٰ� ���غ���.
        }
    }
}
