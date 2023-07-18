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
    [Header("플레이어 관련 오브젝트")]
    public GameObject curPlayer;
    public GameObject enemyTarget;

    [Space(10)]
    [Header("카메라 관련 오브젝트")]
    [SerializeField] private CameraShake cameraShake;

    #region UI 관련 오브젝트

    [Space(10)]
    [Header("UI 관련 오브젝트")]
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
    [Header("게임 관련 변수")]
    public int[] nextExp;

    [Space(10)] [Header("오디오 관련")] 
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip stageClearSound;

    [Space(10)] [Header("영역제한")] 
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
        gameOverKillCountText.text = $"스코어 : {enemyDieCount}";
        gameOverLifeTimeText.text = $"생존 시간 : {minTime:D2} : {secTime:D2}";
        gameOverWindow.SetActive(true);
    }

    public void StageClear()
    {
        isStop = true;
        SoundManager.Instance.PlaySound(stageClearSound,1f);
        stageClearScoreText.text = $"스코어 : {enemyDieCount}";
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