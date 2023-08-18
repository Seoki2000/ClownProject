using System.Collections;
using System.Collections.Generic;
using System.IO;        // 파일 입출력을 위해서 
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;       // 바이너리 파일 포맷을 위한 네임스페이스
using DefinedEnum;

public class LoadObjUI : MonoBehaviour
{
    [SerializeField] List<Button> _buttonList;

    private string _dataPath;       // 파일이 저장될 물리적인 경로 및 파일명을 저장할 변수

    private void Awake()
    {
        _buttonList= new List<Button>();
    }

    public void Initialize()
    {
        // persistentDataPath 속성은 파일을 읽고 쓸 수 있는 폴더의 경로를 반호나
        // IOS와 안드로이드 폰의 경우 공용 폴더 (public directory)의 결로가 반환되며 앱이 업그레이드 돼도 파일은 삭제되지 않는다.
        _dataPath = Application.persistentDataPath + "/gameData.dat";
    }

    public void Save(GetItems gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(_dataPath);

        
    }





}
