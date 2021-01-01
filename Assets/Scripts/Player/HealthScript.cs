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
    }
}
