using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float maxHealth;
    private float curHealth;

    public float moveSpeed;

    private bool animStop = false;
    private bool isLive = true;

    private Transform targetPlayer;

    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer sr;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        targetPlayer = GameManager.Instance.curPlayer.transform;
    }

    protected virtual void OnEnable()
    {
        curHealth = maxHealth;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.isStop)
        {
            rigid.velocity = Vector2.zero;
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

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("HitAnimation"))
        {
            return;
        }

        MoveUpdate();
    }

    protected virtual void LateUpdate()
    {
        if (GameManager.Instance.isStop)
        {
            return;
        }

        if (!isLive)
        {
            return;
        }

        sr.flipX = !(targetPlayer.position.x < transform.position.x);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerDir = new Vector3(GameManager.Instance.playerInputX, GameManager.Instance.playerInputY);
        
        transform.Translate(playerDir * 35 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
    }

    private void MoveUpdate()
    {
        rigid.velocity = Vector2.zero;
        Vector3 dir = GameManager.Instance.curPlayer.transform.position - transform.position;
        Vector3 nextPos = dir.normalized * moveSpeed * Time.deltaTime;
        transform.position += nextPos;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;

        if(curHealth > 0)
        {
            StartCoroutine(KnockBack());
            anim.SetTrigger("Hit");
        }
        else
        {
            // 죽는 로직
            gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockBack()
    {
        yield return null;
        Vector3 dirVec = transform.position - GameManager.Instance.curPlayer.transform.position;
        rigid.AddForce(dirVec.normalized * 2.5f, ForceMode2D.Impulse);
    }
}