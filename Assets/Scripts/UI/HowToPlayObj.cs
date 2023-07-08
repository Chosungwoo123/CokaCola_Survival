using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayObj : MonoBehaviour
{
    [SerializeField] private GameObject[] contents;

    private int index;

    private void Awake()
    {
        index = 0;
        
        contents[index].SetActive(true);
    }

    public void LeftArrow()
    {
        contents[index].SetActive(false);
        
        index--;

        if (index < 0)
        {
            index = contents.Length - 1;
        }
        
        contents[index].SetActive(true);
    }

    public void RightArrow()
    {
        contents[index].SetActive(false);

        index++;

        if (index >= contents.Length)
        {
            index = 0;
        }
        
        contents[index].SetActive(true);
    }
}