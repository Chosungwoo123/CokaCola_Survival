using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingCanColaCard : ItemCard
{
    [SerializeField] private ItemData weaponData;
    
    private void Start()
    {
        InitLevelCard();
    }

    public override void CardLevelUp()
    {
    }

    public override bool CheckMaxLevel()
    {
        return true;
    }

    protected override void InitLevelCard()
    {
        weaponIcon.sprite = weaponData.itemIcon;
        weaponDesc.text = weaponData.levelData[curLevel].itemDesc;
        weaponName.text = weaponData.itemName;
    }
}