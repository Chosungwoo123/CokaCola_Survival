using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private RectTransform titleRect;

    [SerializeField] private RectTransform[] buttonRects;

    [SerializeField] private AudioClip bgm;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(bgm);
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        titleRect.localScale = new Vector3(0, 0, 0);

        foreach (var item in buttonRects)
        {
            item.anchoredPosition = new Vector3(600, item.anchoredPosition.y, 0);
        }

        titleRect.DOScale(8f, 1.5f).SetDelay(1f).SetEase(Ease.OutElastic);

        yield return new WaitForSeconds(2f);

        int index = 0;
        
        for (int i = buttonRects.Length - 1; i >= 0; i--)
        {
            buttonRects[i].DOAnchorPosX(-140, 0.7f).SetDelay(index * 0.1f).SetEase(Ease.OutBack);
            index++;
        }

        yield return null;
    }

    public void PlayUISound(AudioClip sound)
    {
        SoundManager.Instance.PlaySound(sound, 1f);
    }
}