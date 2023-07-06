using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PoppingCanCola : MonoBehaviour
{
    [SerializeField] private float scanRange;

    [SerializeField] private LayerMask targetLayer;

    private int curLevel;
    private int weaponPer;
    
    public float fireRate;
    public float damage;
    public float moveSpeed;
    private float fireTimer;

    public GameObject poppingCanColaPrefab;
    
    private RaycastHit2D[] targets;

    public Transform nearestTarget;

    private ItemData weaponData;

    private void Update()
    {
        FireCanCola();
    }

    private void FixedUpdate()
    {
        CheckNearestTarget();
    }

    private void CheckNearestTarget()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);

        float diff = 100f;

        Transform temp = null;
        
        foreach (var item in targets)
        {
            float curDiff = Vector3.Distance(transform.position, item.transform.position);

            if (curDiff < diff)
            {
                diff = curDiff;
                temp = item.transform;
            }
        }

        nearestTarget = temp;
    }

    public void Init(ItemData data)
    {
        weaponData = data;

        gameObject.name = "PoppingCola";

        curLevel = 0;
        
        fireRate = weaponData.levelData[curLevel].fireRate;
        damage = weaponData.levelData[curLevel].damage;
        weaponPer = weaponData.levelData[curLevel].itemCount;
        moveSpeed = weaponData.levelData[curLevel].weaponMoveSpeed;
        
        poppingCanColaPrefab = weaponData.projectile;

        fireTimer = fireRate;
    }

    public void LevelUp()
    {
        curLevel++;
        
        fireRate = weaponData.levelData[curLevel].fireRate;
        damage = weaponData.levelData[curLevel].damage;
        weaponPer = weaponData.levelData[curLevel].itemCount;
        moveSpeed = weaponData.levelData[curLevel].weaponMoveSpeed;
    }

    private void FireCanCola()
    {
        if (fireTimer <= 0 && nearestTarget != null)
        {
            GameObject canCola = PoolManager.Instance.GetGameObejct(poppingCanColaPrefab, transform.position, quaternion.identity);

            canCola.transform.position = transform.position;
            
            Vector3 dir = nearestTarget.position - transform.position;
            
            canCola.SetActive(true);

            canCola.GetComponent<PoppingCanColaWeapon>().InitPoppingCanCola(damage, weaponPer, dir.normalized, 10);

            fireTimer = fireRate;
        }

        fireTimer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}