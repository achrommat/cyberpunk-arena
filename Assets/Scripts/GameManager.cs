using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject enemyPistolBulletPool;

    private NavMeshSurface surface;

    void Start()
    {
        surface = FindObjectOfType<NavMeshSurface>();
        surface.BuildNavMesh();
    }
}
