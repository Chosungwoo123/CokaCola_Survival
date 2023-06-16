using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExpCell : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Ease easeType;

    [SerializeField] private float moveSpeed;

    private bool isTargetting = false;

    private void OnEnable()
    {
        isTargetting = false;
    }

    private void Update()
    {
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        Vector3 dir = GameManager.Instance.curPlayer.transform.position - transform.position;

        if (!isTargetting)
        {
            isTargetting = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

            if (isTargetting)
            {
                Vector3 temp = transform.position - GameManager.Instance.curPlayer.transform.position;

                Debug.Log(temp.normalized);

                transform.DOMove((transform.position + temp.normalized), 0.5f);
            }

            return; 
        }

        transform.position += dir.normalized * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GetExp();
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}