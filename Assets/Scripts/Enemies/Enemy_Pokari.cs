using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pokari : EnemyBase
{
    public override void OnDamage(float damage)
    {
        if (!isLive)
        {
            return;
        }

        curHealth -= damage;

        if(curHealth > 0)
        {
            StartCoroutine(KnockBack());
            anim.SetTrigger(HitAnimation);
        }
        else
        {
            // 죽는 로직
            PoolManager.Instance.GetGameObejct(expPrefab, transform.position, Quaternion.identity).SetActive(true);
            StartCoroutine(DissolveStart());
            GameManager.Instance.DieCountUp();
            rigid.velocity = Vector2.zero;
            anim.StartPlayback();
            isLive = false;
        }
    }
}