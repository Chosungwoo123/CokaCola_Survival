using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonSFX : MonoBehaviour
{
    [SerializeField] private Sprite muteImage;
    [SerializeField] private Sprite nomalImage;

    [SerializeField] private Image volumeImage;

    [SerializeField] private Slider volumeSlider;

    private bool isMute;

    private float beforeVolume;

    private void Start()
    {
        if (SoundManager.Instance.bgmVolume == 0)
        {
            isMute = true;
            volumeImage.sprite = muteImage;
        }
        else
        {
            isMute = false;
            volumeImage.sprite = nomalImage;
        }
        
        volumeSlider.value = SoundManager.Instance.sfxVolume;
        volumeSlider.onValueChanged.AddListener(val => SoundManager.Instance.SetEffectVolume(val));
    }

    private void Update()
    {
        if (SoundManager.Instance.sfxVolume == 0)
        {
            isMute = true;
            volumeImage.sprite = muteImage;
        }
        else
        {
            isMute = false;
            volumeImage.sprite = nomalImage;
        }
    }

    public void OnSfxButtonClick()
    {
        if (!isMute)
        {
            isMute = true;
            volumeImage.sprite = muteImage;
            beforeVolume = SoundManager.Instance.sfxVolume;
            volumeSlider.value = 0f;
        }
        else
        {
            isMute = false;
            volumeImage.sprite = muteImage;
            volumeSlider.value = beforeVolume;
        }
    }
}
