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


    private bool isRun;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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

            var bullet = Instantiate(bulletPrefab, shotPos.transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 7, ForceMode2D.Impulse);

            fireTimer = 0f;
        }

        fireTimer += Time.deltaTime;
    }

    private void FlipUpdate()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPos.transform.position;

        if(dir.x >= 1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (dir.x <= -1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}