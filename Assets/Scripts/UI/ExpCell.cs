using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExpCell : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private bool isTargetting = false;
    public Transform target;

    private void Update()
    {
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        if (isTargetting == false)
        {
            isTargetting = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

            if (isTargetting)
            {
                target = GameManager.Instance.curPlayer.transform;
            }
        }

        if (isTargetting)
        {
            transform.DOMove(target.position, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}