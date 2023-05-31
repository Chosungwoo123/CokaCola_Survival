using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelUpItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject LevelUpBgObject;
    [SerializeField] private RectTransform[] levelUpObject;

    private List<RectTransform> Rects;

    private void Start()
    {
        Rects = new List<RectTransform>();
    }

    public void  LevelItemRoutine()
    {
        Rects.Clear();

        for (int i = 0; i < levelUpObject.Length; i++)
        {
            levelUpObject[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomNum = Random.Range(0, levelUpObject.Length);

            while (levelUpObject[randomNum].gameObject.activeSelf)
            {
                randomNum = Random.Range(0, levelUpObject.Length);
            }

            levelUpObject[randomNum].gameObject.SetActive(true);
            levelUpObject[randomNum].gameObject.GetComponent<Button>().interactable = true;
            Rects.Add(levelUpObject[randomNum]);
        }

        Rects[0].anchoredPosition = new Vector2(-600, -1000);
        Rects[1].anchoredPosition = new Vector2(0, -1000);
        Rects[2].anchoredPosition = new Vector2(600, -1000);

        Rects[0].gameObject.SetActive(true);
        Rects[1].gameObject.SetActive(true);
        Rects[2].gameObject.SetActive(true);

        Rects[0].DOAnchorPosY(0, 1f).SetEase(Ease.OutBack);
        Rects[1].DOAnchorPosY(0, 1f).SetDelay(0.2f).SetEase(Ease.OutBack);
        Rects[2].DOAnchorPosY(0, 1f).SetDelay(0.4f).SetEase(Ease.OutBack);
    }

    public void DontClickItemButton()
    {
        for (int i = 0; i < Rects.Count; i++)
        {
            Rects[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void CloseLevelUPUI()
    {
        for(int i = 0; i < Rects.Count; i++)
        {
            Rects[i].gameObject.SetActive(false);
        }

        Rects.Clear();
    }
}
