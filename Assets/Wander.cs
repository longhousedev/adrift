using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : HsmState
{
    private Vector3 _destination;
    
    private NavMeshAgent _agent;
    private bool _reached = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (_reached)
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
            var x = (Random.Range(2, 18) * 10) + 5;
            var z = (Random.Range(2, 18) * 10) + 5;
            _destination = new Vector3(x, 0, z);
            _agent.SetDestination(_destination);
            _reached = false;
        }
        int layerMask = 1 << 4;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50, layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.CompareTag("Player")) Debug.Log("BOB");
            else Debug.Log("NO BOB");
        }
    }

    public override void Behaviour()
    {
        if (_reached)
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
            var x = (Random.Range(2, 18) * 10) + 5;
            var z = (Random.Range(2, 18) * 10) + 5;
            _destination = new Vector3(x, 0, z);
            _agent.SetDestination(_destination);
            _reached = false;
        }
        int layerMask = 1 << 4;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50, layerMask, QueryTriggerInteraction.Collide))
        {
            print("BOB");
        }
    }
    
}
