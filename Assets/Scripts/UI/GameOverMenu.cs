using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public TransitionSettings transition;

    public void LoadScene(string sceneName)
    {
        TransitionManager.Instance().Transition(sceneName, transition, 0f);
    }
}