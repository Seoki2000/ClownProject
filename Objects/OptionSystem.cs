using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionSystem : MonoBehaviour, IPointerClickHandler
{

    GameObject _optionUI;
    bool _isFirst;

    private void Awake()
    {
        _isFirst = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_optionUI == null)
        {
            GameObject go = Resources.Load("OptionUIBoxBG") as GameObject;
            GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTag") as GameObject;
            Instantiate(go, canvas.transform.GetChild(canvas.transform.childCount - 1).transform.parent);
            _optionUI = GameObject.FindGameObjectWithTag("OptionUiBox");
        }
        else
        {
            _optionUI.SetActive(true);
        }
       
        if (_isFirst)
        {
            if (IngameManager.Instance._bgmVal <= 0 || IngameManager.Instance._sfxVal <= 0)     // 0보다 낮은 경우 즉 값이 없는 경우나 오류로 너무 낮게 나온 경우.
            {
                SoundOptions.Instance._bgmSlider.value = 0.5f;
                SoundOptions.Instance._sfxSlider.value = 0.5f;
            }
            else
            {
                SoundOptions.Instance._bgmSlider.value = IngameManager.Instance._bgmVal;
                SoundOptions.Instance._sfxSlider.value = IngameManager.Instance._sfxVal;
            }
            _isFirst = false;
        }
        else
        {
            SoundOptions.Instance._bgmSlider.value = SoundOptions.Instance._bgmVal;
            SoundOptions.Instance._sfxSlider.value = SoundOptions.Instance._sfxVal;
        }
    }
}
