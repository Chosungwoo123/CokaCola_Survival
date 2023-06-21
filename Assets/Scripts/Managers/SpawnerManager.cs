using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private SpawnData[] spawnDatas;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int curLevel = 0;

    private float spawnTimer = 0f;
    private float levelUpTimer = 30f;

    private void Update()
    {
        if(GameManager.Instance.isStop || GameManager.Instance.minTime >= 5)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnDatas[Mathf.Min(curLevel, spawnDatas.Length - 1)].spawnTime)
        {
            Spawn();
            spawnTimer = 0f;
        }

        levelUpTimer -= Time.deltaTime;

        if (levelUpTimer <= 0)
        {
            curLevel++;
            levelUpTimer = 30f;
        }
    }

    private void Spawn()
    {
        var enemy = PoolManager.Instance.GetGameObejct(enemies[UnityEngine.Random.Range(0, enemies.Length)],
                    spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        
        enemy.GetComponent<EnemyBase>().InitEnemy(spawnDatas[Mathf.Min(curLevel, spawnDatas.Length)].health, spawnDatas[Mathf.Min(curLevel, spawnDatas.Length)].speed);

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