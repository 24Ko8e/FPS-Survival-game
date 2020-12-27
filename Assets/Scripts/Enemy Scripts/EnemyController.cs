using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    EnemyAnimator enemyAnim;
    NavMeshAgent navAgent;
    EnemyState enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    float currentChaseDist;
    public float attackDist = 1.8f;
    public float chaseAfterAttackDistance = 2f;
    public float patrolRadiusMin = 20f;
    public float patrolRadiusMax = 60f;
    public float patrolDuration = 15f;
    float patrolTimer;
    public float waitBeforeAttack = 2f;
    float attackTimer;

    Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
