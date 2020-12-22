using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGeneration : MonoBehaviour
{
    public NavMeshSurface nav;
    public fade f;
    void Start()
    {
        //Starts the navigation mesh coroutine
        StartCoroutine(NavGen());
        f = GameObject.FindGameObjectWithTag("Fader").GetComponent<fade>();
    }

    IEnumerator NavGen()
    {
        //Waits a few seconds for the level to generate then bakes a navmesh for the AI to use
        yield return new WaitForSeconds(0.5f);
        nav.BuildNavMesh();
        f.FadeIn(1);

    }
    
    
}
