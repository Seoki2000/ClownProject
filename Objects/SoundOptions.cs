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

    [SerializeField] AudioMixer _soundMixer;      // 오디오 믹서

    public Slider _bgmSlider;
    public Slider _sfxSlider;

    // 사운드는 0 ~ -65까지가 볼륨이 가장 적절하다고 한다. BGM인 경우.
    // 효과음은 똑같은 사운드로 하는데 기본값을 -10으로 하고 BGM은 -20으로 해서 이후 플레이어가 조절 할 수 있게한다. 

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
        _soundMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);  // 로그 연산 값 반환
        Debug.Log(_bgmSlider.value);
        IngameManager.Instance._bgmVal = _bgmVal;
    }
    public void SetSFXVolme()
    {
        _sfxVal = _sfxSlider.value;
        _soundMixer.SetFloat("SFX", Mathf.Log10(_sfxSlider.value) * 20);  // 로그 연산 값 반환
        Debug.Log(_sfxSlider.value);
        IngameManager.Instance._sfxVal = _sfxVal;
    }
}
