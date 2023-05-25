using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private Transform[] spawnPoints;
    public SpawnData[] spawnDatas;

    public int level = 1;

    private float timer = 0;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnDatas[level].spawnTime)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject g;
    }
}

[Serializable]
public class SpawnData
{
    public float spawnTime;
    public int health;
    public float speed;
}