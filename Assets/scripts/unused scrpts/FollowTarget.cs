using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public Transform target; 
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to the FollowTarget script.");
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position); 
        }
    }
}
