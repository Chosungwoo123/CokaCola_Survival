using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    [SerializeField] private Slider expBar;

    private void LateUpdate()
    {
        ExpUpdate();
    }

    private void ExpUpdate()
    {
        float curExp = GameManager.Instance.curExp;
        float maxExp = GameManager.Instance.nextExp[Mathf.Min(GameManager.Instance.playerLevel, GameManager.Instance.nextExp.Length)];

        if (expBar.value > curExp / maxExp) expBar.value = 0.03f;
        expBar.value = Mathf.Lerp(expBar.value, Mathf.Max(curExp / maxExp, 0.03f), Time.deltaTime * 12f );
    }
}