using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    public Transform target; // ÷ель, к которой движетс€ NavMeshAgent
    public NavMeshAgent agent; // NavMeshAgent персонажа

    public float stoppingDistance = 2f; // –ассто€ние, на котором персонаж должен остановитьс€ перед целью

    void Update()
    {
        // ѕровер€ем рассто€ние между персонажем и целью
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // ≈сли рассто€ние меньше или равно stoppingDistance
        if (distanceToTarget <= stoppingDistance)
        {
            // ќстанавливаем NavMeshAgent
            agent.isStopped = true;
        }
        else
        {
            // ¬ противном случае двигаемс€ к цели
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }
}
