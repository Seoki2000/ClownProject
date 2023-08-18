using System.Collections;
using System.Collections.Generic;
using System.IO;        // ���� ������� ���ؼ� 
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;       // ���̳ʸ� ���� ������ ���� ���ӽ����̽�
using DefinedEnum;

public class LoadObjUI : MonoBehaviour
{
    [SerializeField] List<Button> _buttonList;

    private string _dataPath;       // ������ ����� �������� ��� �� ���ϸ��� ������ ����

    private void Awake()
    {
        _buttonList= new List<Button>();
    }

    public void Initialize()
    {
        // persistentDataPath �Ӽ��� ������ �а� �� �� �ִ� ������ ��θ� ��ȣ��
        // IOS�� �ȵ���̵� ���� ��� ���� ���� (public directory)�� ��ΰ� ��ȯ�Ǹ� ���� ���׷��̵� �ŵ� ������ �������� �ʴ´�.
        _dataPath = Application.persistentDataPath + "/gameData.dat";
    }

    public void Save(GetItems gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(_dataPath);

        
    }





}
