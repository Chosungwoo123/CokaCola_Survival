using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExpCell : MonoBehaviour
{
    [SerializeField] private Ease easeType;

    [SerializeField] private float moveSpeed;

    [SerializeField] private AudioClip sound;

    private bool isTargetting = false;

    private void OnEnable()
    {
        isTargetting = false;
    }

    private void Update()
    {
        if (GameManager.Instance.isStop)
        {
            return;
        }
        
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
            SoundManager.Instance.PlaySound(sound, 1f);
            gameObject.SetActive(false);
        }
    }
}