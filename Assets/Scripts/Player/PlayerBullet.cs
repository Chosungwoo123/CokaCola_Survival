using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int count;

    private void Update()
    {
        if (GameManager.Instance.isStop)
        {
            return;
        }

        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    public void InitPlayerBullet(float damage, int count, Vector3 size)
    {
        this.damage = damage;
        this.count = count;
        this.transform.localScale = size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().OnDamage(damage);
            count--;

            if(count <= 0)
            {
                // »èÁ¦
                gameObject.SetActive(false);
            }
        }    
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}