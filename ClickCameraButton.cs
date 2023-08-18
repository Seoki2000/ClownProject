using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickCameraButton : MonoBehaviour, IPointerClickHandler
{
    // ī�޶� ��ư Ŭ�� �� �ؾ� �ϴ� ��.
    // �ϴ� ���� ī�޶� �̹����� ����. �� �� ��� �̹��� �޾ƿͼ� �ű⿡ �̹���������Ʈ �޾ƿ��� �� Alpha���� 60���� �����ؼ� ���� ��Ӱ� �������.
    // ���� �������� �̰� Ŭ���ؼ� ���̴� �������� ���°Ŵϱ�.... �� ����� �Ⱥ��̴� ���� Ȥ�� ��й�ȣ �����°� ���̰� �ǰ� �� ��ư�� Ŭ�� �� 
    // ��й�ȣ�� Ǯ �� �ְ� �ȴ�. ��й�ȣ�� Ʋ�� ��� ������ �߰��ϰ� 10���� �̻��� ��� Ŭ���� ���� �� �ٽ��ϱ� Ȥ�� �����ϱ� ��ư�� ������ �ǰ� �Ѵ�. (�̸� �������ش�.)

    // �ϴ� Ŭ���̴ϱ� �̺�Ʈ�ý������� �޾ƿͼ� �غ��� 

    // ����Ŵ� �����µ� ���⼭ Ŭ�� �� ���̰� ���ְ� �װ� �ƴ� ��� �ٽ� ���ִ� ������ �ؼ� �ʿ� ������ ������Ʈ�� ���̰� ���ش�.(���� ������ �ִ� ū ����ó�� ���̴� ������Ʈ�� ���� ��й�ȣ �����°�ó�� ���̴°� ������ְ� �װ� Ŭ�� �� ��й�ȣ �����Ⱑ ������
    // ��й�ȣ�� ���߸� ���� �����鼭 ������ ���������� �Ѿ�� �ȴ�.

    GameObject _hiddenObjOne;          // ī�޶� ȹ�����ڸ��� ������ ������ �ι�° ��ư Ŭ�� �� ����.
    GameObject _hiddenObjTwo;
    GameObject _camButton;

    bool _isOn;

    private void Awake()
    {
        _isOn = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isOn)      // ���� �ִ� ��쿡 Ŭ�� �� ���. �׷��� �� ī�޶� �������� setActive false�� �ھ��ְ� �� �̹����� �ٽ� �࿡ ���ش�.
        {
            _camButton.SetActive(false);        // ī�޶��̹��� ���ֱ�.

            _hiddenObjOne.SetActive(false);        // ���� ������Ʈ ����.
            _hiddenObjTwo.SetActive(false);        // ���� ������Ʈ ����.

            ChangeMapImg(1);                    // ���İ� �������ֱ� 
            
            _isOn = false;
            return;
        }
        
        if (_camButton != null)     // ��ư�� ������ ī�޶� Ȱ��ȭ ��Ų ���
        {
            _camButton.SetActive(true);     // ī�޶� ���ֱ�

            _hiddenObjOne.SetActive(true);     // ���������Ʈ ���ֱ�
            _hiddenObjTwo.SetActive(true);        // ���� ������Ʈ ����.

            ChangeMapImg(0.45f);            // �÷��� �� ���İ� ���� 
            
            _isOn = true;                   // ���� ���¸� �˷���.
            // ���⿡�ٰ� ������ܿ� ������Ʈ�� �����ͼ� ��ư������ ���̰� �̰ɷ� 
        }
        else
        {
            // ī�޶� �̹��� ĵ���� ���� �ؿ� �ڽ����� �����ϱ�
            GameObject go = Resources.Load("Map3/CameraImg") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag");
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _camButton = GameObject.FindGameObjectWithTag("CameraImg");
            ChangeMapImg(0.45f);
            _hiddenObjOne = canvas.transform.GetChild(0).GetChild(5).gameObject;
            _hiddenObjTwo = canvas.transform.GetChild(0).GetChild(13).gameObject;
            Debug.Log(canvas.transform.childCount);
            _isOn = true;
        }
    }


    public void ChangeMapImg(float alphaValue)      // ���⼭ �ʿ� �̹��� �������� �� �� �̹����� alphaValue�� �� �����ϱ� 
    {
        GameObject go = GameObject.FindGameObjectWithTag("PlayMap");        // ���θ� ��������
        Image mapImg = go.GetComponent<Image>();
        mapImg.color = new Color(1, 1, 1, alphaValue);                            // ���İ� ���̱�(��Ӱ� ȭ�� ����.)
    }


}
