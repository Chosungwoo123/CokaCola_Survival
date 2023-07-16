using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicator;

    [SerializeField] private LayerMask layerMask;

    private new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (renderer.isVisible == false)
        {
            if (indicator.activeSelf == false)
            {
                indicator.SetActive(true);
            }

            Vector2 direction = GameManager.Instance.curPlayer.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);
            
            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
                float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
                indicator.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            }
        }
        else
        {
            if (indicator.activeSelf)
            {
                indicator.SetActive(false);
            }
        }
    }
}