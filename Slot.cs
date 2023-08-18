using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image _img;        // �̹��� ������Ʈ�� �־��ֱ� ���ؼ�

    private Item _item;
    public Item item
    {
        get { return _item; }       // ������ ������ ������ �Ѱ��� �� ��� 
        set
        {
            _item = value;      // �����ۿ� ������ ������ ���� _item�� ����˴ϴ�.
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
        // ���߿� Inventory ��ũ��Ʈ���� List<item? _items�� ��ϵ� �������� �ִٸ� itemImg�� img�� ���� �׸��� img�� ���İ��� 1���Ͽ� �̹����� ǥ��
        // ���� null(������ ���������) img�� ���İ��� 0���� �ؼ� ȭ�鿡 ǥ������ �ʽ��ϴ�.
    }
}
