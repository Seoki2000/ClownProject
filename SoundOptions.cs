using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundOptions : MonoBehaviour
{
    static SoundOptions instance;

    public static SoundOptions Instance
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

    [SerializeField] AudioMixer _soundMixer;      // ����� �ͼ�

    public Slider _bgmSlider;
    public Slider _sfxSlider;

    // ����� 0 ~ -65������ ������ ���� �����ϴٰ� �Ѵ�. BGM�� ���.
    // ȿ������ �Ȱ��� ����� �ϴµ� �⺻���� -10���� �ϰ� BGM�� -20���� �ؼ� ���� �÷��̾ ���� �� �� �ְ��Ѵ�. 

    public float _bgmVal;
    public float _sfxVal;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public void SetBGMVolme()
    {
        _bgmVal = _bgmSlider.value;
        _soundMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);  // �α� ���� �� ��ȯ
        Debug.Log(_bgmSlider.value);
        IngameManager.Instance._bgmVal = _bgmVal;
    }
    public void SetSFXVolme()
    {
        _sfxVal = _sfxSlider.value;
        _soundMixer.SetFloat("SFX", Mathf.Log10(_sfxSlider.value) * 20);  // �α� ���� �� ��ȯ
        Debug.Log(_sfxSlider.value);
        IngameManager.Instance._sfxVal = _sfxVal;
    }
}
