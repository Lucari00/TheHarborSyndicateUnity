using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    public float sightRange;
    public bool playerInSightRange;

    public float attackRange;
    public bool playerInAttackRange;

    private Enemy enemy;

    public float timeToShoot;
    private bool canShoot;
 
    public void Awake() {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        canShoot = true;
    }

    private void Update() {
        if (enemy.isHit) {
            agent.isStopped = true;
            return;
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        if (!playerInSightRange) enemy.anim.SetInteger("state", 1);
    }

    private void ChasePlayer() {
        agent.SetDestination(player.position);
        enemy.anim.SetInteger("state", 3);
    }

    private void AttackPlayer() {
        enemy.AimAt(player);
        if (canShoot) {
            canShoot = false;
            enemy.Shoot();
            Invoke("ResetShoot", timeToShoot);
        }
        enemy.anim.SetInteger("state", 0);
        agent.SetDestination(transform.position);
        transform.LookAt(player);
    }

    private void ResetShoot() {
        canShoot = true;
    }
}
