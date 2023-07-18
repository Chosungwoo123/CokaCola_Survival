using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject enemyTarget;

    [Space(10)]
    [Header("ī�޶� ���� ������Ʈ")]
    [SerializeField] private CameraShake cameraShake;

    #region UI ���� ������Ʈ

    [Space(10)]
    [Header("UI ���� ������Ʈ")]
    [SerializeField] private Text dieCountText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text levelText;
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject stopMenuUI;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private TextMeshProUGUI gameOverKillCountText;
    [SerializeField] private TextMeshProUGUI gameOverLifeTimeText;
    [SerializeField] private GameObject stageClearObj;
    [SerializeField] private TextMeshProUGUI stageClearScoreText;
    
    #endregion

    [Space(10)]
    [Header("���� ���� ����")]
    public int[] nextExp;

    [Space(10)] [Header("����� ����")] 
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip stageClearSound;

    [Space(10)] [Header("��������")] 
    [SerializeField] private Vector2 border;
    
    [HideInInspector] public int enemyDieCount;
    [HideInInspector] public int curExp = 0;
    [HideInInspector] public int playerLevel = 0;
    [HideInInspector] public int minTime = 0;
    [HideInInspector] public int secTime = 0;

    [HideInInspector] public float playerInputX;
    [HideInInspector] public float playerInputY;

    [HideInInspector] public bool isStop = false;

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
        SoundManager.Instance.PlayMusic(bgm);
        
        dieCountText.text = enemyDieCount.ToString();
    }

    private void Update()
    {
        TimerUpdate();
        StopMenuUpdate();
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

        if (minTime == 5)
        {
            StageClear();
        }

        timerText.text = $"{minTime:D2}:{secTime:D2}";
    }

    private void StopMenuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isStop)
        {
            ShowStopMenu();
        }
    }

    public void ShowStopMenu()
    {
        if (!isStop)
        {
            isStop = true;
            stopMenuUI.SetActive(true);
        }
        else
        {
            isStop = false;
            stopMenuUI.SetActive(false);
        }
    }

    private void LevelUpdate()
    {
        levelText.text = $"Lv.{playerLevel:F0}";
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

        if(curExp >= nextExp[Mathf.Min(playerLevel, nextExp.Length - 1)])
        {
            playerLevel++;
            curExp = 0;
            LevelUpdate();
            LevelUpManager.Instance.ShowLevelUpUI();
        }
    }

    public void TimeStop()
    {
        isStop = true;
        Time.timeScale = 0f;
    }

    public void TimePlay()
    {
        isStop = false;
        Time.timeScale = 1f;
    }

    public void SetHpBar(float amount)
    {
        hpBar.value = amount;
    }

    public void ShowGameOverWindow()
    {
        isStop = true;
        gameOverKillCountText.text = $"���ھ� : {enemyDieCount}";
        gameOverLifeTimeText.text = $"���� �ð� : {minTime:D2} : {secTime:D2}";
        gameOverWindow.SetActive(true);
    }

    public void StageClear()
    {
        isStop = true;
        SoundManager.Instance.PlaySound(stageClearSound,1f);
        stageClearScoreText.text = $"���ھ� : {enemyDieCount}";
        stageClearObj.SetActive(true);
    }
    
    public bool CheckBorderX(float x, float radius)
    {
        if (border.x == 0)
        {
            return true;
        }
        
        return border.x / 2 > x + radius && -border.x / 2 < x - radius;
    }

    public bool CheckBorderY(float y, float radius)
    {
        if (border.y == 0)
        {
            return true;
        }
        
        return border.y / 2 > y + radius && -border.y / 2 < y - radius;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireCube(Vector2.zero, border);
    }
}