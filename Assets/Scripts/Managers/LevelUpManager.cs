using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    private static LevelUpManager instance;

    public GameObject levelUpBG;
    public LevelUpItemHandler levelUpHandler;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            ShowLevelUpUI();
        }
    }

    public void ShowLevelUpUI()
    {
        GameManager.Instance.isStop = true;
        levelUpBG.SetActive(true);
        levelUpHandler.LevelItemRoutine();
    }

    public void CloseLevelUpUI()
    {
        GameManager.Instance.isStop = false;
        levelUpBG.SetActive(false);
        levelUpHandler.CloseLevelUPUI();
    }
}
