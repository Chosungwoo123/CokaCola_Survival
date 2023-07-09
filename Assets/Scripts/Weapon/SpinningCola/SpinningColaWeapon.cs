using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningColaWeapon : MonoBehaviour
{
    [SerializeField] private float attackDamage;

    public void InitSpainningColaWeapon(float attackDamage, Vector3 weaponSize)
    {
        this.attackDamage = attackDamage;
        this.transform.localScale = weaponSize;
    }           
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.isStop)
        {
            return; 
        }
        
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().OnDamage(attackDamage);
        }
    }
}