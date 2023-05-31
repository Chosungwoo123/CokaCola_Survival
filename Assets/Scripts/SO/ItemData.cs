using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public ItemDateInfo[] levelData;

    public GameObject projectile;

    public Sprite itemIcon;

    public string itemName;

    [Serializable]
    public struct ItemDateInfo
    {
        public float damage;
        [TextArea] public string itemDesc;
        public int itemCount;
    }
}