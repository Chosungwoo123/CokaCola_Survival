using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemCard : MonoBehaviour
{
    [Space(10)]
    [Header("기본 UI 변수")]
    [SerializeField] protected Image weaponIcon;
    [SerializeField] protected Text weaponName;
    [SerializeField] protected Text weaponDesc;
    [SerializeField] protected Text levelText;

    protected int curLevel = 0;
    
    protected abstract void InitLevelCard();
    public abstract void CardLevelUp();
    public abstract bool CheckMaxLevel();
}