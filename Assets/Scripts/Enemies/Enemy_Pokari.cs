using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pokari : EnemyBase
{
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip dieSound;
    
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
            SoundManager.Instance.PlaySound(hitSound, 1f);
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
            SoundManager.Instance.PlaySound(dieSound, 1f);
            isLive = false;
        }
    }
}