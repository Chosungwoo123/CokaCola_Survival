using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffect : MonoBehaviour
{
    private void AnimationEnd()
    {
        gameObject.SetActive(false);
    }
}