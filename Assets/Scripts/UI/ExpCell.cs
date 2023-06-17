using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExpCell : MonoBehaviour
{
    [SerializeField] private Ease easeType;

    [SerializeField] private float moveSpeed;

    private bool isTargetting = false;

    private void OnEnable()
    {
        isTargetting = false;
    }

    private void Update()
    {
        MoveUpdate();
    }
    
    public void Targetting()
    {
        if (!isTargetting)
        {
            Vector3 temp = transform.position - GameManager.Instance.curPlayer.transform.position;
                
            transform.DOMove((transform.position + temp.normalized), 0.5f);
        }
        
        isTargetting = true;
    }

    private void MoveUpdate()
    {
        if (!isTargetting)
        {
            return;
        }
        
        Vector3 dir = GameManager.Instance.curPlayer.transform.position - transform.position;
        
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
}