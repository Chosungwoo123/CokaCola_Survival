using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TransitionSettings transition;
    
    public void LoadGameScene(string sceneName)
    {
        TransitionManager.Instance().Transition(sceneName, transition, 0f);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}