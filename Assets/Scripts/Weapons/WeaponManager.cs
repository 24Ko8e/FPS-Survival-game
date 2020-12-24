using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    WeaponHandler[] weapons;

    int currentWeaponIndex;

    void Start()
    {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setSelectedWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            setSelectedWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            setSelectedWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            setSelectedWeapon(5);
        }
    }

    void setSelectedWeapon(int weaponIndex)
    {
        if (currentWeaponIndex == weaponIndex)
            return;

        weapons[currentWeaponIndex].gameObject.SetActive(false);

        currentWeaponIndex = weaponIndex;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public WeaponHandler getSelectedWeapon()
    {
        return weapons[currentWeaponIndex];
    }
}
