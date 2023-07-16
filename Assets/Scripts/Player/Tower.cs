using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    [SerializeField] private Image hpImage;

    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private AudioClip hitSound;

    private float curHealth;

    private void Start()
    {
        curHealth = maxHealth;

        hpImage.fillAmount = curHealth / maxHealth;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;
        
        GameManager.Instance.CameraShake(2, 0.1f);

        hitEffect.Emit(1);
        
        SoundManager.Instance.PlaySound(hitSound, Random.Range(0.7f, 1.1f));
        
        hpImage.fillAmount = curHealth / maxHealth;

        if (curHealth <= 0)
        {
            GameManager.Instance.ShowGameOverWindow();
        }
    }
}