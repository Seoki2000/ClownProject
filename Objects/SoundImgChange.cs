using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundImgChange : MonoBehaviour
{
    [SerializeField] Sprite _soundImg;
    [SerializeField] Sprite _soundZeroImg;

    [SerializeField] Slider _slider;

    [SerializeField] GameObject _soundObj;
    Image _objImgC;
    private void Awake()
    {
        _objImgC = _soundObj.GetComponent<Image>();
    }
    private void Update()
    {
        if(_slider.value <= _slider.minValue)
        {
            _objImgC.sprite = _soundZeroImg;
        }
        else
        {
            _objImgC.sprite = _soundImg;
        }
    }
}
