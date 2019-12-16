using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    private NavMeshSurface surface;

    void Start()
    {
        surface = FindObjectOfType<NavMeshSurface>();
        //surface.BuildNavMesh();
    }
}
