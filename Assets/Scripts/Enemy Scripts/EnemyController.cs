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
    [SerializeField]
    EnemyAnimator enemyAnim;
    [SerializeField]
    NavMeshAgent navAgent;
    EnemyState enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    float currentChaseDist;
    public float attackDist = 1.8f;
    public float RunawayDistanceBuffer = 2f;
    public float patrolRadiusMin = 20f;
    public float patrolRadiusMax = 60f;
    public float patrolDuration = 15f;
    float patrolTimer;
    public float waitBeforeAttack = 2f;
    float attackTimer;

    Transform target;
    public GameObject attackPoint;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolDuration;
        attackTimer = waitBeforeAttack;
        currentChaseDist = chaseDistance;
    }

    void Update()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        else if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        else if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    private void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforeAttack)
        {
            enemyAnim.Attack();
            attackTimer = 0;
        }

        if (Vector3.Distance(transform.position, target.position) > attackDist + RunawayDistanceBuffer)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    private void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        navAgent.SetDestination(target.position);

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Run(true);
        }
        else
        {
            enemyAnim.Run(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDist)
        {
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
            enemyState = EnemyState.ATTACK;

            if (chaseDistance != currentChaseDist)
            {
                chaseDistance = currentChaseDist;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            enemyAnim.Run(false);
            enemyState = EnemyState.PATROL;
            patrolTimer = patrolDuration;
            if (chaseDistance != currentChaseDist)
            {
                chaseDistance = currentChaseDist;
            }
        }
    }

    private void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolDuration)
        {
            SetNewRandomDestination();
            patrolTimer = 0;
        }
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnim.Walk(false);
            enemyState = EnemyState.CHASE;
        }
    }

    private void SetNewRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);

        navAgent.SetDestination(navHit.position);
    }

    void turnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void turnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
            attackPoint.SetActive(false);
    }
}
