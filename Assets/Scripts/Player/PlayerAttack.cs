using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponManager weaponManager;
    public float fireRate = 15f;
    float nxtTimeToFire;
    float damage = 20f;

    public Animator camZoomAnimator;
    bool isZoomed;
    bool isAiming;
    Camera mainCam;
    public GameObject crosshair;

    [SerializeField]
    GameObject arrow, spear;

    [SerializeField]
    Transform arrow_spearStartPosition;

    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        mainCam = Camera.main;
        damage = weaponManager.currentWeaponDamage;
    }

    void Start()
    {
        
    }

    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        damage = weaponManager.currentWeaponDamage;

        if (weaponManager.getSelectedWeapon().fireType == weaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > nxtTimeToFire)
            {
                nxtTimeToFire = Time.time + 1f / fireRate;
                weaponManager.getSelectedWeapon().shootAnimation();
                BullerFired();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weaponManager.getSelectedWeapon().tag == "Axe")
                {
                    weaponManager.getSelectedWeapon().shootAnimation();
                }
                if (weaponManager.getSelectedWeapon().bulletType == weaponBulletType.BULLET)
                {
                    weaponManager.getSelectedWeapon().shootAnimation();
                    BullerFired();
                }
                else
                {
                    if (isAiming)
                    {
                        weaponManager.getSelectedWeapon().shootAnimation();
                        if (weaponManager.getSelectedWeapon().bulletType == weaponBulletType.ARROW)
                        {
                            throwArrowOrSpear(true);
                        }
                        if (weaponManager.getSelectedWeapon().bulletType == weaponBulletType.SPEAR)
                        {
                            throwArrowOrSpear(false);
                        }
                    }
                }
            }
        }
    }

    private void BullerFired()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<HealthScript>().applyDamage(damage);
            }
        }
    }

    private void ZoomInAndOut()
    {
        if (weaponManager.getSelectedWeapon().weapon_Aim == weaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                camZoomAnimator.Play("ZoomIn");
                crosshair.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                camZoomAnimator.Play("ZoomOut");
                crosshair.SetActive(true);
            }
        }

        if (weaponManager.getSelectedWeapon().weapon_Aim == weaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.getSelectedWeapon().Aim(true);
                isAiming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.getSelectedWeapon().Aim(false);
                isAiming = false;
            }
        }
    }

    void throwArrowOrSpear(bool isArrow)
    {
        if (isArrow)
        {
            GameObject arrowInst = Instantiate(arrow);
            arrowInst.transform.position = arrow_spearStartPosition.position;
            arrowInst.GetComponent<BowArrow>().Launch(mainCam);
        }
        else
        {
            GameObject spearInst = Instantiate(spear);
            spearInst.transform.position = arrow_spearStartPosition.position;
            spearInst.GetComponent<BowArrow>().Launch(mainCam);
        }
    }
}
