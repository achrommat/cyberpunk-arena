using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _spawnTime = 3f;
    [SerializeField] private int _minEnemyCount = 5;
    [SerializeField] private int _maxEnemyCount = 10;
    [SerializeField] private Transform[] _spawnPoints;
    private List<GameObject> enemies = new List<GameObject>();
    private int waveIndex = 0;

    private int wave = 0;
    private int numOfTotalWaves = 3;
    public int AliveEnemies = 0;

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (AliveEnemies <= 0)
        {
            if (wave <= numOfTotalWaves)
            {
                wave++;
                SpawnWave();
            }
        }
    }

    private bool CanSpawn()
    {
        enemies = enemies.Where(e => e != null).ToList();
        return enemies.Count == 0;
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        GameObject newEnemy = MF_AutoPool.Spawn(_enemy, spawnPosition, Quaternion.identity);
        enemies.Add(newEnemy);
        AliveEnemies++;
    }

    private void SpawnWave()
    {
        for (int i = 0; i < Random.Range(_minEnemyCount, _maxEnemyCount); i++)
        {
            SpawnEnemy();
        }
    }
}
