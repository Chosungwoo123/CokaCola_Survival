using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float moveSpeed;

    private float curHealth;
    
    private bool animStop = false;
    private bool isLive = true;

    private Transform targetPlayer;

    [SerializeField] private GameObject expPrefab;

    Material material;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer sr;
    private static readonly int HitAnimation = Animator.StringToHash("Hit");

    protected virtual void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
        targetPlayer = GameManager.Instance.curPlayer.transform;
    }

    protected virtual void OnEnable()
    {
        if (material != null)
        {
            material.SetFloat("_DissolveAmount", 0);
        }

        curHealth = maxHealth;
        isLive = true;
    }

    protected virtual void Update()
    {
        #region 게임이 멈췄는지 체크하는 로직
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
        #endregion

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

        FilpUpdate();
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

    private IEnumerator KnockBack()
    {
        yield return null;
        Vector3 dirVec = transform.position - GameManager.Instance.curPlayer.transform.position;
        rigid.AddForce(dirVec.normalized * 2.5f, ForceMode2D.Impulse);
    }

    private IEnumerator DissolveStart()
    {
        float count = 0;

        while (count < 1)
        {
            material.SetFloat("_DissolveAmount", count);
            count += Time.deltaTime * 1.5f;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void FilpUpdate()
    {
        Vector2 dir = GameManager.Instance.curPlayer.transform.position - transform.position;

        if (dir.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (dir.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void InitEnemy(float health, float moveSpeed)
    {
        maxHealth = health;
        curHealth = health;
        this.moveSpeed = moveSpeed;
    }
}