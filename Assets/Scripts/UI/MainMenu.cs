using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TransitionSettings transition;

    public void LoadGameScene()
    {
        TransitionManager.Instance().Transition("PistolStage1", transition, 0f);
    }
}