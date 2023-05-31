using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private SpawnData[] spawnDatas;

    [SerializeField] private Transform[] spawnPoints;

    public int level = 1;

    private float timer = 0f;

    private void Update()
    {
        if(GameManager.Instance.isStop)
        {
            return;
        }

        timer += Time.deltaTime;

        Debug.Log(spawnDatas[level].spawnTime);

        if (timer > spawnDatas[level].spawnTime)
        {
            Spawn();
            timer = 0f;
            Debug.Log("AA");
        }
    }

    private void Spawn()
    {
        var enemy = PoolManager.Instance.GetGameObejct(enemies[UnityEngine.Random.Range(0, enemies.Length)],
                    spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

        enemy.SetActive(true);
    }
}

[Serializable]
public class SpawnData
{
    public float spawnTime;
    public int health;
    public float speed;
}