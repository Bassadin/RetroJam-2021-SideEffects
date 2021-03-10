using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour {
    private Transform playerTransform;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private Animator enemyAnimator;

    public Vector3 walkPoint;
    private bool walkPointSet;
    private float distanceFromPlayer;
    public float walkPointRange;

    public float sightRange;
    public bool playerInSightRange;

    private void Awake()
    {
        playerTransform = GameObject.Find("FirstPersonPlayer").transform;
    }

    void Update()
    {
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("Walking", false);
        enemyAnimator.SetBool("Chasing", false);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange)
        {
            Patrolling();
        }
        if (playerInSightRange && distanceFromPlayer > 5)
        {
            ChasePlayer();
        }
    }

    private void FixedUpdate() {
        distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            enemyAnimator.SetBool("Idle", false);
            enemyAnimator.SetBool("Walking", true);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceWalkPoint = transform.position - walkPoint;

        if (distanceWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("Chasing Player");
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("Chasing", true);
        enemyAnimator.SetBool("Walking", true);
        agent.SetDestination(playerTransform.position);
    }
}