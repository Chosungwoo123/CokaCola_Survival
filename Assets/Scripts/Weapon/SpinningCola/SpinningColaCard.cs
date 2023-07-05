using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningColaCard : ItemCard
{
    [SerializeField] private ItemData weaponData;

    private SpinningCola weaponObj;

    private void Start()
    {
        InitLevelCard();
    }

    protected override void InitLevelCard()
    {
        weaponIcon.sprite = weaponData.itemIcon;
        weaponDesc.text = weaponData.levelData[curLevel].itemDesc;
        weaponName.text = weaponData.itemName;

        levelText.text = "NEW!!";

        gameObject.SetActive(false);
    }

    public override void CardLevelUp()
    {
        if (curLevel <= 0)
        {
            GameObject newWeapon = new GameObject();
            weaponObj = newWeapon.AddComponent<SpinningCola>();
            weaponObj.Init(weaponData);

            curLevel++;

            if (curLevel >= weaponData.levelData.Length)
            {
                levelText.text = $"Lv.MAX";
                weaponDesc.text = "최대 레벨입니다.";

                return;
            }

            levelText.text = $"Lv.{curLevel:F0}";
            weaponDesc.text = weaponData.levelData[curLevel].itemDesc;
        }
        else
        {
            weaponObj.LevelUp();
            curLevel++;

            if (curLevel >= weaponData.levelData.Length)
            {
                levelText.text = $"Lv.MAX";
                weaponDesc.text = "최대 레벨입니다.";

                return;
            }

            levelText.text = $"Lv.{curLevel:F0}";
            weaponDesc.text = weaponData.levelData[curLevel].itemDesc;
        }
    }

    public override bool CheckMaxLevel()
    {
        return (curLevel >= weaponData.levelData.Length);
    }
}