using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingCanColaWeapon : MonoBehaviour
{
    private int per;
    
    private float damage;
    private float moveSpeed;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void InitPoppingCanCola(float damage, int per, Vector3 dir, float moveSpeed)
    {
        this.damage = damage;
        this.per = per;
        this.moveSpeed = moveSpeed;

        Debug.Log(dir);
        Debug.Log(dir * moveSpeed);
        
        rigid.velocity = dir * 15;
        
        Debug.Log(rigid.velocity);
        
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }
        
        other.GetComponent<EnemyBase>().OnDamage(damage);

        var dir = Vector2.Reflect(transform.position.normalized, other.transform.position.normalized);

        rigid.velocity = dir * moveSpeed;
        
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        per--;

        if (per <= -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}