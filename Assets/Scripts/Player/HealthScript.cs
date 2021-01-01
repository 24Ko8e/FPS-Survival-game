using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    EnemyAnimator enemyAnim;
    NavMeshAgent navAgent;
    EnemyController enemyController;

    public float health = 100f;
    public bool isPlayer, isBoar, isCannibal;
    bool isDead;

    private void Awake()
    {
        if (isBoar || isCannibal)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        if (isPlayer)
        {

        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void applyDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;
        if (isPlayer)
        {

        }
        if (isBoar || isCannibal)
        {
            if (enemyController.EnemyState == EnemyState.PATROL)
            {
                enemyController.chaseDistance = 50f;
            }
        }
        if (health <= 0)
        {
            playerDied();
            isDead = true;
        }
    }

    private void playerDied()
    {
        if (isCannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;

        }

        if (isBoar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemyController.enabled = false;
            enemyAnim.Dead();

        }
        if (isPlayer)
        {
            // disable enemy controller for all enemies

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().getSelectedWeapon().gameObject.SetActive(false);

            // show game over screen and restart the game

        }
    }
}
