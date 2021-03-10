using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour {
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    public bool playerInSightRange;

    private void Awake() {
        playerTransform = GameObject.Find("First Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
       

        if (!playerInSightRange) {
            Patrolling();
        }
        if (playerInSightRange) {
            ChasePlayer();
        }
    }

    private void Patrolling() {
        if (!walkPointSet) {
            SearchWalkPoint();
        }
        else {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceWalkPoint = transform.position - walkPoint;

        if (distanceWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void ChasePlayer() {
        agent.SetDestination(playerTransform.position);
    }
}