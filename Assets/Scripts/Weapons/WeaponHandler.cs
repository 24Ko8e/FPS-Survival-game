using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum weaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum weaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    Animator anim;
    public weaponAim weapon_Aim;

    [SerializeField]
    GameObject muzzleFlash;
    public float weaponDamage;
    [SerializeField]
    AudioSource shootSound, reloadSound;
    public weaponFireType fireType;
    public weaponBulletType bulletType;
    public GameObject attackPoint;

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

    public void shootAnimation()
    {
        anim.SetTrigger("Shoot");
    }

    public void Aim(bool canAim)
    {
        anim.SetBool("Aim", canAim);
    }

    void turnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void turnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void playShootSound()
    {
        shootSound.Play();
    }

    void playReloadSound()
    {
        reloadSound.Play();
    }

    void turnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void turnOffAttackPoint()
    {
        if(attackPoint.activeInHierarchy)
            attackPoint.SetActive(false);
    }
}
