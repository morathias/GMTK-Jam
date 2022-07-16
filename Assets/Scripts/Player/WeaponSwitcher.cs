using System.Collections.Generic;
using DG.Tweening;
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

        GameObject currentWeapon = allWeapons[weaponIndex];
        RunActivateAnimation(currentWeapon);
    }

    private static void RunActivateAnimation(GameObject currentWeapon)
    {
        currentWeapon.transform.localScale = Vector3.zero;
        currentWeapon.GetComponent<IWeapon>().AddCooldown(.2f);
        Sequence s = DOTween.Sequence();
        s.Append(currentWeapon.transform.DOScale(1, .15f));
        s.Append(currentWeapon.transform.DOShakeScale(.5f, 1));
    }

    public void SetRandomWeapon()
    {
        SwitchWeapon(allWeapons.RandomIndex());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.SwitchWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.SwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.SwitchWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.SwitchWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.SwitchWeapon(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            this.SwitchWeapon(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            this.SetRandomWeapon();
        }
    }
}
