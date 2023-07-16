using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerPistol : MonoBehaviour
{
    [Space(10)] [Header("기본 스탯 관련 변수")] [SerializeField]
    private float maxHealth;
    [SerializeField] private float moveSpeed;

    [Space(10)] [Header("게임 오브젝트")] 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shotPos;
    [SerializeField] private GameObject magnetObj;

    [Space(10)] [Header("공격 관련")] [SerializeField]
    private float fireRate;

    [Space(10)] [Header("이펙트 관련")]
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private ParticleSystem hitEffect;

    [Space(10)] [Header("사운드 관련")] 
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip damageSound;
    
    private int bulletCount;
    private bool isRun;
    private bool animStop = false;

    private float fireTimer;
    private float curHealth;
    private float curMoveSpeed;
    private float bulletDamage;
    private float hitSoundLength;
    private float hitSoundTimer;
    
    private Vector3 bulletSize;
    
    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D rigid;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        hitSoundLength = 0.3f;
        hitSoundTimer = hitSoundLength;

        curMoveSpeed = moveSpeed;

        curHealth = maxHealth;
    }

    private void Start()
    {
        GameManager.Instance.SetHpBar(curHealth / maxHealth);
    }

    private void Update()
    {
        #region 게임이 멈췄는지 체크하는 로직
        if (GameManager.Instance.isStop)
        {
            if (!animStop)
            {
                anim.StartPlayback();
                animStop = true;
            }
            return;
        }
        else if (!GameManager.Instance.isStop)
        {
            if (animStop)
            {
                anim.StopPlayback();
                animStop = false;
            }
        }
        #endregion

        MoveUpdate();
        AnimationUpdate();
        ShotUpdate();
        FlipUpdate();
        HitSoundUpdate();
    }

    private void MoveUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0)
        {
            
        }
        
        if (y != 0)
        {
        }
        
        GameManager.Instance.playerInputX = x;
        GameManager.Instance.playerInputY = y;

        if (x != 0 || y != 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        Vector3 movePos = new Vector2(x, y) * (curMoveSpeed * Time.deltaTime);

        transform.position += movePos;
    }

    private void AnimationUpdate()
    {
        anim.SetBool("isRun", isRun);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ShotUpdate()
    {
        if (fireTimer >= fireRate && Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPos.transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var bullet = PoolManager.Instance.GetGameObejct(bulletPrefab, shotPos.transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
            
            bullet.GetComponent<PlayerBullet>().InitPlayerBullet(bulletDamage, bulletCount, bulletSize);

            SoundManager.Instance.PlaySound(shotSound, Random.Range(0.65f, 1.2f));
            
            fireEffect.SetActive(true);
            bullet.SetActive(true);

            GameManager.Instance.CameraShake(3f, 0.1f);

            fireTimer = 0f;
        }

        fireTimer += Time.deltaTime;
    }

    private void FlipUpdate()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if(dir.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (dir.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void HitSoundUpdate()
    {
        hitSoundTimer -= Time.deltaTime;
    }

    public void BulletLevelUp(int bulletCount, float bulletDamage, Vector3 bulletSize, float fireRate)
    {
        this.bulletCount = bulletCount;
        this.bulletDamage = bulletDamage;
        this.bulletSize = bulletSize;
        this.fireRate = fireRate;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;
        
        GameManager.Instance.SetHpBar(curHealth / maxHealth);
        
        GameManager.Instance.CameraShake(2, 0.1f);
        
        hitEffect.Emit(1);
        
        SoundManager.Instance.PlaySound(damageSound, Random.Range(0.7f, 1.1f));
        
        if (curHealth <= 0)
        {
            // 게임 오버 로직
            GameManager.Instance.ShowGameOverWindow();
        }
    }

    public void HealHP(float hp)
    {
        curHealth = Mathf.Min(curHealth + hp, maxHealth);

        GameManager.Instance.SetHpBar(curHealth / maxHealth);
    }

    public void LevelUpMagnet(float size)
    {
        magnetObj.transform.localScale = new Vector3(size, size, 1);
    }

    public void UpgradeSpeed(float rate)
    {
        curMoveSpeed = curMoveSpeed + (moveSpeed / rate);
    }
}