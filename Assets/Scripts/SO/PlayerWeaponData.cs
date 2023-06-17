using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerWeaponData_", menuName = "ScriptableObject/PlayerWeaponData")]
public class PlayerWeaponData : ScriptableObject
{
    public WeaponDateInfo[] weaponData;

    public Sprite weaponIcon;

    public string weaponName;
    
    [Serializable]
    public struct WeaponDateInfo
    {
        public float damage;
        public int weaponCount;
        public Vector3 bulletSize;
        [TextArea] public string weaponDesc;
    }
}