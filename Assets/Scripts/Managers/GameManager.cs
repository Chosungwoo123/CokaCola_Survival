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
    [Header("�÷��̾� ���� ������Ʈ")]
    public GameObject curPlayer;

    [Space(10)]
    [Header("ī�޶� ���� ������Ʈ")]
    [SerializeField] private CameraShake cameraShake;

    [Space(10)]
    [Header("UI ���� ������Ʈ")]
    [SerializeField] private Text dieCountText;
    [SerializeField] private Text timerText;
    [SerializeField] private Slider expSlider;

    [Space(10)]
    [Header("���� ���� ����")]
    public int[] nextExp;

    [HideInInspector] public int enemyDieCount;
    [HideInInspector] public int curExp = 0;
    [HideInInspector] public int playerLevel = 0;

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

        // Test
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetExp();
        }
    }

    private void TimerUpdate()
    {
        if (isStop)
        {
            return;
        }

        gameTimer += Time.deltaTime;

        minTime = Mathf.FloorToInt(gameTimer / 60);
        secTime = Mathf.FloorToInt(gameTimer % 60);

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

    public void GetExp()
    {
        curExp++;

        if(curExp >= nextExp[Mathf.Min(playerLevel, nextExp.Length)])
        {
            playerLevel++;
            curExp = 0;
            LevelUpManager.Instance.ShowLevelUpUI();
        }
    }
}