using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Space(10)]
    [Header("카메라 관련")]
    [SerializeField] private CameraShake cameraShake;

    [Space(10)]
    [Header("UI 관련")]
    [SerializeField] private Text dieCountText;
    [SerializeField] private Text timerText;

    [HideInInspector] public int enemyDieCount;

    [HideInInspector] public float playerInputX;
    [HideInInspector] public float playerInputY;

    [HideInInspector] public bool isStop = false;

    private int minTime = 0;
    private int secTime = 0;

    private float gameTimer = 0f;

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

    private void Start()
    {
        dieCountText.text = enemyDieCount.ToString();
    }

    private void Update()
    {
        TimerUpdate();
    }

    private void TimerUpdate()
    {
        if (isStop)
        {
            return;
        }

        gameTimer += Time.deltaTime;

        int minTime = Mathf.FloorToInt(gameTimer / 60);
        int secTime = Mathf.FloorToInt(gameTimer % 60);

        timerText.text = string.Format("{0:D2}:{1:D2}", minTime, secTime);
    }

    public void DieCountUp()
    {
        enemyDieCount++;

        dieCountText.text = enemyDieCount.ToString();
    }

    public void CameraShake(float intensity, float time)
    {
        cameraShake.ShakeCamera(intensity, time);
    }
}