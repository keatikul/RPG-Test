using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 10f;
    public float fleeHealth = 20f;
    public float moveSpeed = 15f;
    public float rotationSpeed = 5f;
    public int attackDamage = 10;
    public float attackRate = 1f;
    private float nextAttackTime;
    private float currentHealth;
    private bool isAttacking = false;
    public bool isFleeing = false;
    private Animator anim;
    private Rigidbody rb;
    public GameObject fireBreathPrefab;
    public Transform fireBreathSpawnPoint;

    public Vector3[] fleePoints;
    public Transform[] targets;
    public Transform initialPosition;
    public float fleeHealthThreshold = 20f;
    public float fleeeSpeed = 10f;
    private HealthAI health;

    private bool isReturning = false;
    public NavMeshAgent agent;
    public int currentTarget;
    public int currentFleePoint;
    public int round;
    public int index;

    Rigidbody rigidbody;
    public bool Dying = false;
    void Start()
    {
        health = GetComponent<HealthAI>();
        currentHealth = GetComponent<HealthAI>().currentHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 15f;
        rigidbody = GetComponent<Rigidbody>();
        //fireBreathPrefab.SetActive(false);
        
        
    }

    void Update()
    {
        fireBreathPrefab.transform.rotation = fireBreathSpawnPoint.transform.rotation;
        fireBreathPrefab.transform.position = fireBreathSpawnPoint.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && !isFleeing && Dying == false)
        {
            isAttacking = true;
            anim.SetBool("Walk", false);
            //anim.SetBool("ClawAttack", true);
            Attack();
        }
        
        if (health.currentHealth == 20 && !isFleeing)
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
                round++;
                
            }
            agent.SetDestination(targets[currentTarget].position);
        }
        else
        {
            isAttacking = false;
            Fly();
            //anim.SetBool("Walk", true);
            //agent.SetDestination(player.position);
        }
        if (round == 1)
        {
            ReturnToFight();
            
        }
        if (health.currentHealth <= 0)
        {
            Dying = true;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            //agent.SetDestination(transform.position);
            anim.SetBool("Walk", false);
            anim.SetBool("BasicAttack", false);
            anim.SetBool("ClawAttack", false);
            anim.SetBool("FlameAttack", false);
            anim.SetBool("Fly", false);
            fireBreathPrefab.SetActive(false);
        }
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            
            player.GetComponent<CharacterInfo>().TakeDamage(attackDamage);
            nextAttackTime = Time.time + 1f / attackRate;
            index = Random.Range(1, 4);
            Debug.Log(index);
            if (index == 1)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("BasicAttack", true);
                anim.SetBool("ClawAttack", false);
                anim.SetBool("FlameAttack", false);
                fireBreathPrefab.SetActive(false);
            }
            else if (index == 2)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("BasicAttack", false);
                anim.SetBool("ClawAttack", true);
                anim.SetBool("FlameAttack", false);
                fireBreathPrefab.SetActive(false);
            }
            else
            {
                fireBreathPrefab.SetActive(true);
                anim.SetBool("Walk", false);
                anim.SetBool("BasicAttack", false);
                anim.SetBool("ClawAttack", false);
                anim.SetBool("FlameAttack", true);
            }
            
            Debug.Log("Take Damage 10");
        }
        //fireBreathPrefab.SetActive(false);
            
    }

    /*public void Flee()
    {
        Debug.Log("Fleeing");
        agent.speed = 20f;
        agent.destination = fleePoints[currentFleePoint];
        agent.SetDestination(fleePoints[currentFleePoint]);
    }*/

    void StartFleeing()
    {
        anim.SetBool("Fly",true);
        anim.SetBool("Walk", false);
        anim.SetBool("BasicAttack", false);
        anim.SetBool("ClawAttack", false);
        anim.SetBool("FlameAttack", false);
        fireBreathPrefab.SetActive(false);
        isFleeing = true;
        agent.speed = fleeeSpeed;
        agent.SetDestination(targets[currentTarget].position);
        //navMeshAgent.SetDestination(initialPosition.position);
    }

    void ReturnToFight()
    {
        round = 0;
        isFleeing = false;
        //agent.destination = player.position;
        agent.SetDestination(initialPosition.position);
        anim.SetBool("Fly", false);
        
        //agent.SetDestination(player.position);
        //Fly();
        if (Vector3.Distance(transform.position, player.position) < 1f)
        {
            isReturning = false;
        }
    }

    void Fly()
    {
        //anim.SetBool("Walk", true);
        Vector3 flyDirection = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(flyDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.velocity = transform.forward * moveSpeed;
        //Debug.Log(flyDirection);
        //agent.SetDestination(player.position);
    }

    
}