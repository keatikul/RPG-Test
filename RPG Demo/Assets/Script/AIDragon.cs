using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIDragon : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;
    public float attackRange = 20f;
    public int attackDamage = 10;
    public float attackRate = 2f;
    public float flySpeed = 5f;
    public float flyHeight = 15f;
    public GameObject fireBreathPrefab;
    public Transform fireBreathSpawnPoint;
    public float fleeHealthThreshold = 20f;
    public float fleeSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private HealthAI health;
    private float nextAttackTime;
    public bool isFlying = false;
    public bool isFleeing = false;
    public int currentWaypoint = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthAI>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else if (!isFlying && !isFleeing)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else if (health.currentHealth <= health.maxHealth * fleeHealthThreshold / 100 && !isFleeing)
            {
                StartFleeing();
            }
            else if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 1f)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                    StopFleeing();
                }
                navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
            }
        }
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (!isFlying)
            {
                //StartFlying();
            }
            else
            {

                player.GetComponent<CharacterInfo>().TakeDamage(attackDamage);
                nextAttackTime = Time.time + 1f / attackRate;
                //Instantiate(fireBreathPrefab, fireBreathSpawnPoint.position, fireBreathSpawnPoint.rotation);
            }
        }
    }

    void StartFlying()
    {
        navMeshAgent.speed = flySpeed;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.baseOffset = flyHeight;
        navMeshAgent.SetDestination(player.position);
        isFlying = true;
    }

    void StartFleeing()
    {
        isFleeing = true;
        navMeshAgent.speed = fleeSpeed;
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
    }

    void StopFleeing()
    {
        isFleeing = false;
        navMeshAgent.speed = flySpeed;
        navMeshAgent.SetDestination(player.position);
    }
}
