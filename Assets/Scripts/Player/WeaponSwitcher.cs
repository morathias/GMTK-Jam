using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> allWeapons;
    public int startingWeaponIndex;

    private GameObject currentWeapon;

    private void Start()
    {
        this.SwitchWeapon(this.startingWeaponIndex);
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if ((weaponIndex >= this.allWeapons.Count) ||
            (this.currentWeapon == this.allWeapons[weaponIndex]))
        {
            return;
        }

        for (int i = 0; i < this.allWeapons.Count; i++)
        {
            this.allWeapons[i].SetActive(i == weaponIndex);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            this.SwitchWeapon(0);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            this.SwitchWeapon(1);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            this.SwitchWeapon(2);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            this.SwitchWeapon(3);
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            this.SwitchWeapon(4);
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            this.SwitchWeapon(5);
        }
    }
}
