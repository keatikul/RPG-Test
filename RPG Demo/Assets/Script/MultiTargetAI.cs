using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MultiTargetAI : MonoBehaviour
{
    public Transform[] targets;
    public Transform initialPosition;
    public float initialSpeed = 20f;
    public float fleeHealthThreshold = 20f;
    public float fleeSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private HealthAI health;
    public int currentTarget = 0;
    public bool isFleeing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthAI>();
        //navMeshAgent.SetDestination(targets[currentTarget].position);
    }

    void Update()
    {
        if (health.currentHealth <= health.maxHealth * fleeHealthThreshold / 100 && !isFleeing)
        {
            Debug.Log("Working");
            StartFleeing();
        }
        else if (Vector3.Distance(transform.position, targets[currentTarget].position) < 1f)
        {
            currentTarget++;
            if (currentTarget >= targets.Length)
            {
                currentTarget = 0;
                StopFleeing();
            }
            navMeshAgent.SetDestination(targets[currentTarget].position);
        }
        Debug.Log("current" + health.currentHealth);
        Debug.Log(health.maxHealth * fleeHealthThreshold / 100);
    }

    void StartFleeing()
    {
        isFleeing = true;
        navMeshAgent.speed = fleeSpeed;
        navMeshAgent.SetDestination(targets[currentTarget].position);
        //navMeshAgent.SetDestination(initialPosition.position);
    }

    void StopFleeing()
    {
        isFleeing = false;
        navMeshAgent.speed = initialSpeed;
        navMeshAgent.SetDestination(initialPosition.position);
    }
}
