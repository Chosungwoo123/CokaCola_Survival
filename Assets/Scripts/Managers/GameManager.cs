using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    [Space(10)]
    [Header("플레이어 관련")]
    public GameObject curPlayer;

    [HideInInspector] public float playerInputX;
    [HideInInspector] public float playerInputY;

    [HideInInspector] public bool isStop = false;

    [Space(10)]
    [Header("카메라 관련")]
    [SerializeField] private CameraShake cameraShake;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CameraShake(float intensity, float time)
    {
        cameraShake.ShakeCamera(intensity, time);
    }
}