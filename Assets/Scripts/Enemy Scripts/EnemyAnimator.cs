using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Walk(bool walk)
    {
        anim.SetBool("Walk", walk);
    }

    public void Run(bool run)
    {
        anim.SetBool("Run", run);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Dead()
    {
        anim.SetTrigger("Dead");
    }
}
