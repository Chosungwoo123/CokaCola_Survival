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
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
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
}