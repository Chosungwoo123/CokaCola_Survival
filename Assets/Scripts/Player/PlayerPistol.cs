using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPistol : MonoBehaviour
{
    public float moveSpeed;

    [Space(10)]
    [Header("게임 오브젝트")]
    public GameObject bulletPrefab;
    public GameObject shotPos;

    [Space(10)]
    [Header("공격 관련")]
    public float fireRate;
    private float fireTimer;

    [Space(10)]
    [Header("이펙트 관련")]
    [SerializeField] private ParticleSystem fireEffect;

    private bool isRun;

    private Animator anim;
    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveUpdate();
        AnimationUpdate();
        ShotUpdate();
        FlipUpdate();
    }

    private void MoveUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

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

        Vector3 movePos = new Vector2(x, y) * moveSpeed * Time.deltaTime;

        transform.position += movePos;
    }

    private void AnimationUpdate()
    {
        anim.SetBool("isRun", isRun);
    }

    private void ShotUpdate()
    {
        if (fireTimer >= fireRate && Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPos.transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var bullet = PoolManager.Instance.GetGameObejct(bulletPrefab, shotPos.transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90)));

            bullet.SetActive(true);

            fireEffect.Play();

            GameManager.Instance.CameraShake(4f, 0.1f);

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
}