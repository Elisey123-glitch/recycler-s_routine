using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    public Transform target; // ����, � ������� �������� NavMeshAgent
    public NavMeshAgent agent; // NavMeshAgent ���������

    public float stoppingDistance = 2f; // ����������, �� ������� �������� ������ ������������ ����� �����

    void Update()
    {
        // ��������� ���������� ����� ���������� � �����
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // ���� ���������� ������ ��� ����� stoppingDistance
        if (distanceToTarget <= stoppingDistance)
        {
            // ������������� NavMeshAgent
            agent.isStopped = true;
        }
        else
        {
            // � ��������� ������ ��������� � ����
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }
}
