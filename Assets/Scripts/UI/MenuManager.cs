using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class MenuManager : MonoBehaviour
{
    public TransitionSettings transition;

    public void LoadScene(string sceneName)
    {
        TransitionManager.Instance().Transition(sceneName, transition, 0f);
    }

    public void PlaySound(AudioClip sound)
    {
        SoundManager.Instance.PlaySound(sound);
    }
}