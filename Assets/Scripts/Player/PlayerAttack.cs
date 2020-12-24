using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponManager weaponManager;
    public float fireRate = 15f;
    float nxtTimeToFire;
    public float damage = 20f;

    public Animator camZoomAnimator;
    bool isZoomed;
    Camera mainCam;
    public GameObject crosshair;

    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        mainCam = Camera.main;
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

                }
            }
        }
    }

    private void BullerFired()
    {
        
    }

    private void ZoomInAndOut()
    {

    }
}
