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

    private WaitForSeconds waitForSeconds;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        waitForSeconds = new WaitForSeconds(10f);
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyRoutine());
    }

    public void InitPoppingCanCola(float damage, int per, Vector3 dir, float moveSpeed)
    {
        this.damage = damage;
        this.per = per;
        this.moveSpeed = moveSpeed;
        
        rigid.velocity = dir * 15;
        
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.isStop)
        {
            return; 
        }
        
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

    IEnumerator DestroyRoutine()
    {
        yield return waitForSeconds;
        
        gameObject.SetActive(false);
    }
}