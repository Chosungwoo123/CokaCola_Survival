using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCard : MonoBehaviour
{
    protected abstract void InitLevelCard();
    public abstract void CardLevelUp();
    public abstract bool CheckMaxLevel();
}