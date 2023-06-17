using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLevelCard : ItemCard
{
    [SerializeField] private PlayerWeaponData weaponData;

    [SerializeField] private Image weaponIcon;
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponDesc;
    [SerializeField] private Text levelText;

    private int curLevel = 0;

    private void Start()
    {
        InitLevelCard();
    }

    protected override void InitLevelCard()
    {
        weaponIcon.sprite = weaponData.weaponIcon;
        weaponName.text = weaponData.weaponName;

        CardLevelUp();
        
        gameObject.SetActive(false);
    }

    public override void CardLevelUp()
    {
        GameManager.Instance.curPlayer.GetComponent<PlayerPistol>().BulletLevelUp(
            weaponData.weaponData[curLevel].weaponCount,
            weaponData.weaponData[curLevel].damage, weaponData.weaponData[curLevel].bulletSize);
        
        curLevel++;

        if (curLevel >= weaponData.weaponData.Length)
        {
            levelText.text = $"Lv.MAX";
            weaponDesc.text = "최대 레벨입니다.";
            
            return;
        }
        
        levelText.text = $"Lv.{curLevel:F0}";
        weaponDesc.text = weaponData.weaponData[curLevel].weaponDesc;
    }

    public override bool CheckMaxLevel()
    {
        return (curLevel >= weaponData.weaponData.Length);
    }
}