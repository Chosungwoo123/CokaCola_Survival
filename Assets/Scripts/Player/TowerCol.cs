using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCol : MonoBehaviour
{
    public SpriteRenderer towerSR;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {       
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        float a = 1f;

        while (a > 0.4f)
        {
            towerSR.color = new Vector4(towerSR.color.r, towerSR.color.g, towerSR.color.b, a);
            a -= Time.deltaTime * 3;
            yield return null;
        }
        
        towerSR.color = new Vector4(towerSR.color.r, towerSR.color.g, towerSR.color.b, 0.4f);
    }
    
    private IEnumerator FadeIn()
    {
        float a = 0.4f;

        while (a < 1f)
        {
            towerSR.color = new Vector4(towerSR.color.r, towerSR.color.g, towerSR.color.b, a);
            a += Time.deltaTime * 3;
            yield return null;
        }
        
        towerSR.color = new Vector4(towerSR.color.r, towerSR.color.g, towerSR.color.b, 1);
    }
}