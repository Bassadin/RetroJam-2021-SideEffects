using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {
    [SerializeField] private NavMeshAgent agent;
    private Transform playerTransform;
    [SerializeField] private LayerMask whatIsPlayer;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    public float attackRange;
    private bool playerInAttackRange;

    private void Awake() {
        playerTransform = GameObject.Find("FirstPersonPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update() {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    
        if(playerInAttackRange) {
            AttackPlayer();
        }
    }
    private void AttackPlayer() {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(playerTransform);

        if (!alreadyAttacked) {
            

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack() {
        alreadyAttacked = false;
    }
}
