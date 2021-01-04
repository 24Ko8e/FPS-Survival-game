using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    EnemyAnimator enemyAnim;
    NavMeshAgent navAgent;
    EnemyController enemyController;
    [SerializeField] EnemyAudio enemyAudio;
    [SerializeField] Image healthBar;

    public float health = 100f;
    public bool isPlayer, isBoar, isCannibal;
    public bool isDead;

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
            setPlayerHealthBar(health);
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

    void setPlayerHealthBar(float health)
    {
        health /= 100f;
        healthBar.fillAmount = health;
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
            StartCoroutine(deathSound());
            EnemyManager.instance.disableEnemy(this.gameObject);
            EnemyManager.instance.enemyDied(true);
        }

        if (isBoar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemyController.enabled = false;
            enemyAnim.Dead();
            StartCoroutine(deathSound());
            EnemyManager.instance.disableEnemy(this.gameObject);
            EnemyManager.instance.enemyDied(false);
        }
        if (isPlayer)
        {
            // disable enemy controller for all enemies
            //EnemyManager.instance.disableEnemyControllers();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().getSelectedWeapon().gameObject.SetActive(false);
            EnemyManager.instance.stopSpawning();
            // show game over screen and restart the game

        }
    }

    IEnumerator deathSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeadSound();
    }
}
