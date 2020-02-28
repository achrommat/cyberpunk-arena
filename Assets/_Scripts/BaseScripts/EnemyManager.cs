using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _spawnTime = 3f;
    [SerializeField] private int _minEnemyCount = 1;
    [SerializeField] private int _maxEnemyCount = 5;
    [SerializeField] private Transform _spawnPoints;
    private List<Transform> _spawnPointsList = new List<Transform>();

    private int _waveCount = 0;
    [SerializeField] private int _numOfTotalWaves = 3;

    private void OnEnable()
    {
        GetListOfSpawnPoints();
    }

    private void GetListOfSpawnPoints()
    {
        Transform[] spawnPoints = _spawnPoints.GetComponentsInChildren<Transform>();
        foreach (Transform spawnPoint in spawnPoints)
        {
            _spawnPointsList.Add(spawnPoint);
        }
    }

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (CanSpawn())
        {            
            if (_waveCount <= _numOfTotalWaves)
            {
                int enemyCount = Random.Range(_minEnemyCount, _maxEnemyCount);
                SpawnWave(enemyCount);
                _waveCount++;
            }
        }
    }

    private bool CanSpawn()
    {
        return GameManager.instance.AliveEnemyCount <= 0;
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = _spawnPointsList[Random.Range(0, _spawnPointsList.Count)].position;
        List<EnemyController> enemies = new List<EnemyController>(_enemy.GetComponentsInChildren<EnemyController>(true));
        int enemyType = Random.Range(0, enemies.Count);
        GameObject newEnemyObj = MF_AutoPool.Spawn(_enemy, enemyType, spawnPosition, Quaternion.identity);
        EnemyController newEnemy = newEnemyObj.GetComponentInChildren<EnemyController>();
        newEnemy.OnSpawned();
    }

    private void SpawnWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }
}
