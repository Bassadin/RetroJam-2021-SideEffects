using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Animator enemyAnimator;

    private Transform playerTransform;
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

        Vector3 playerPositionTranslatedToSameLevel = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

        transform.LookAt(playerPositionTranslatedToSameLevel);

        if (!alreadyAttacked) {
            enemyAnimator.SetTrigger("Shoot");
            enemyController.ShootWeapon();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack() {
        alreadyAttacked = false;
    }
}
