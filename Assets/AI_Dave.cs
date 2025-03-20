using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Dave : MonoBehaviour
{
    public float deplacement = 10f; 
    public Transform Player;
    public float chaseRange = 10f;
    public float attackRange = 2f;

    private NavMeshAgent agent;
    private Animator animator;

    private bool isChasing = false;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    
        Transform daveTransform = transform.Find("Dave");
        if (daveTransform != null)
        {
            animator = daveTransform.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("L'enfant 'Dave' n'a pas été trouvé dans la hiérarchie !");
        }

        MoveToRandomPosition();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);

        if (distance <= attackRange)
        {
            StartAttack();
        }
        else if (distance <= chaseRange)
        {
            StartChase();
        }
        else
        {
            StopChase();
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isChasing && !isAttacking)
        {
            MoveToRandomPosition();
        }
    }

    void StartChase()
    {
        if (!isChasing)
        {
            isChasing = true;
            isAttacking = false;

            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isIdle", false);

            agent.isStopped = false;
        }

        agent.SetDestination(Player.position);
    }

    void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            isChasing = false;

            animator.SetBool("isAttacking", true);
            animator.SetBool("isChasing", false);
            animator.SetBool("isIdle", false);

            agent.isStopped = true;
        }
    }

    void StopChase()
    {
        if (isChasing || isAttacking)
        {
            isChasing = false;
            isAttacking = false;

            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isIdle", true); 

            agent.isStopped = false;
            MoveToRandomPosition(); 
        }
    }

    void MoveToRandomPosition()
    {
        Vector3 randomPoint = RandomNavSphere(transform.position, deplacement, -1);
        agent.SetDestination(randomPoint);
        agent.isStopped = false;
        animator.SetBool("isIdle", false);
    }

    Vector3 RandomNavSphere(Vector3 origin, float radius, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;
        
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, radius, layermask))
        {
            return navHit.position;
        }

        return transform.position;
}}
