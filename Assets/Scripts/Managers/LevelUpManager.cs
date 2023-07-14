using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    private static LevelUpManager instance;

    public GameObject levelUpBG;
    public LevelUpItemHandler levelUpHandler;
    public AudioClip levelUpSound;

    private bool isOpenLevelUI = false;

    public static LevelUpManager Instance
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

    public void ShowLevelUpUI()
    {
        if (isOpenLevelUI)
        {
            return;
        }
        isOpenLevelUI = true;
        GameManager.Instance.TimeStop();
        SoundManager.Instance.PlaySound(levelUpSound, 1);
        levelUpBG.SetActive(true);
        levelUpHandler.LevelItemRoutine();
    }

    public void CloseLevelUpUI()
    {
        isOpenLevelUI = false;
        GameManager.Instance.TimePlay();
        levelUpBG.SetActive(false);
        levelUpHandler.CloseLevelUPUI();
    }
}
