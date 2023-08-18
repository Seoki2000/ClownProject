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
    [SerializeField] List<Sprite> _itemImgList;     // ������ �̹������� �������� ���ؼ� ���� 
    [SerializeField] List<GameObject> _itemList;    // ������ �̹����� �־��� ������Ʈ 


    GetItems item;      //������ ȹ��    �ΰ��ӿ��� �̰� �Ѱ��ָ� �̰� ������ Ÿ���� �Ѱ� �����Ҷ����� ++�� �����ؼ� �Ѱ��� �ø���. ���� �� �ִ� ��� 

    private void Awake()
    {
        _itemImgList = new List<Sprite>();
        _itemList = new List<GameObject>();
    }

    // ���â���� ���

    // �̹��� ����Ʈ�� �ְ� ���� ������Ʈ ����Ʈ�� �ִ�. ���ݱ��� ȹ���ߴ��� Ȯ���ϰ� ���� �ִٸ� ����Ʈ ������� �̹����� �־��ش�.
    public void CheckGetItem()
    {
        // �ϴ� ���� ���� �ҷ����°ű� ������ ������ ����. ���� ���� �����ϱ� ���� �� �ְ� ������ �̹������� �޾ƿ;���.
        // ������ ȹ���Ѱ� �ʹ� ���� �� = �Ѱ��� ���� ��� ���� ������Ʈ�� �ִ� �͵��� ���� �������� �ȴ�.
        switch (item)           // ����ġ������ ���� �����ۿ� ���� � ���� �߰����� �����Ѵ�. 
        {
            case GetItems.Null:
                break;
            case GetItems.Key:
                break;
            case GetItems.battery:
                break;
            case GetItems.remote:
                break;
            case GetItems.Count:        // ���� �� ȹ���� ��� 
                for (int n = 0; n < _itemList.Count; n++)
                {
                    Image img = _itemList[n].GetComponent<Image>();     // ����Ʈ�� �ִ°͵� �Ѱ� �Ѱ� �� ������Ʈ �����ؿͼ�
                    img.sprite = _itemImgList[n];                       // �̹����� �ٲ��ش� 
                }
                break;
        }
    }
    public void ShowResultWnd()
    {
        gameObject.SetActive(true);
    }



    // ��ư��
    public void ReStartGame()
    {
        //Application.LoadLevel("1_play");        // ������ÿ� �ִ� �� ù��° ���� �ٽ� �ҷ��´�(�� �ٽ��ϱ� ���� ��� ���)
        SceneManager.LoadScene("MainScene");            // ���ξ� �ҷ�����
    }

    public void ExitGame()
    {
        Application.Quit();                                   // ���� ���ӿ��� ���� 
        //EditorApplication.isPlaying = false;        // �����Ϳ��� ����. �׽�Ʈ���̴�. 
    }
}
