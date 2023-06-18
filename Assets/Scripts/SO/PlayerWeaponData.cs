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
        [Tooltip("총알의 데미지를 나타내는 변수")]
        public float damage;

        [Tooltip("연사속도(낮을수록 연사속도가 올라간다.)")] 
        public float fireRate;
        
        [Tooltip("총알이 적을 얼마나 관통하는지 나타내는 변수")]
        public int weaponCount;
        
        [Tooltip("총알의 크기를 나타내는 변수")]
        public Vector3 bulletSize;
        
        [Tooltip("레벨 카드에 표시될 설명")]
        [TextArea] public string weaponDesc;
    }
}