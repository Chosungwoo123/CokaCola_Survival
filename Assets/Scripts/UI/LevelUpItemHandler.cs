using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelUpItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject LevelUpBgObject;
    [SerializeField] private RectTransform[] levelUpObject;
    [SerializeField] private RectTransform[] infiniteItem;

    [SerializeField] private AudioClip selectItemSound;

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
            
            // 아이템이 만렙이면 힐팩같은 무한으로 먹을 수 있는 아이템으로 대체
            if (levelUpObject[randomNum].GetComponent<ItemCard>().CheckMaxLevel())
            {
                for (int j = 0; j < infiniteItem.Length; j++)
                {
                    if (!infiniteItem[j].gameObject.activeSelf)
                    {
                        infiniteItem[j].gameObject.SetActive(true);
                        infiniteItem[j].gameObject.GetComponent<Button>().interactable = true;
                        Rects.Add(infiniteItem[j]);
                        break;
                    }
                }
                
                continue;
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

        Rects[0].DOAnchorPosY(0, 1f).SetEase(Ease.OutBack).SetUpdate(true);
        Rects[1].DOAnchorPosY(0, 1f).SetDelay(0.2f).SetEase(Ease.OutBack).SetUpdate(true);
        Rects[2].DOAnchorPosY(0, 1f).SetDelay(0.4f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void DontClickItemButton()
    {
        SoundManager.Instance.PlaySound(selectItemSound, 1f);
        
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
