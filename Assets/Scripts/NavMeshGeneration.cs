using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGeneration : MonoBehaviour
{
    public NavMeshSurface nav;
    void Start()
    {
        //Starts the navigation mesh coroutine
        StartCoroutine(NavGen());
    }

    IEnumerator NavGen()
    {
        //Waits a few seconds for the level to generate then bakes a navmesh for the AI to use
        yield return new WaitForSeconds(2);
        nav.BuildNavMesh();
    }
    
    
}
