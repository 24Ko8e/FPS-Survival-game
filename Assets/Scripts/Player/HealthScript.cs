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

        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
