using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelUpItem : MonoBehaviour
{
    RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void ItemClickEvent()
    {
        LevelUpManager.Instance.levelUpHandler.DontClickItemButton();

        StartCoroutine(ItemClickRoutine());
    }

    IEnumerator ItemClickRoutine()
    {
        rt.DOAnchorPosY(-1000, 1).SetEase(Ease.InBack).SetUpdate(true);

        yield return new WaitForSecondsRealtime(1.3f);

        if (!gameObject.GetComponent<ItemCard>().CheckMaxLevel())
        {
            gameObject.GetComponent<ItemCard>().CardLevelUp();
        }

        LevelUpManager.Instance.CloseLevelUpUI();
    }
}