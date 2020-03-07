using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public int AliveEnemyCount = 0;
    public int Score = 0;

    [HideInInspector] public EnemyManager CurrentEnemyManager;

    [SerializeField] private Text _scoreText;

    [SerializeField] private MMFeedbacks _freezeTimeFeedback;

    private NavMeshSurface surface;

    void Start()
    {
        surface = FindObjectOfType<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    private void Update()
    {
        _scoreText.text = Score.ToString();
    }

    public void AddScore(int cost)
    {
        Score += cost;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void FreezeTime()
    {
        if (CurrentEnemyManager._waveCount == CurrentEnemyManager._numOfTotalWaves)
        {
            _freezeTimeFeedback.PlayFeedbacks();
        }
    }
}
