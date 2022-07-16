using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> allWeapons;
    public int startingWeaponIndex;

    private GameObject currentWeapon;
    public int currentIndex;
    public List<Transform> faceOrientations;

    private void Start()
    {
        this.SwitchWeapon(this.startingWeaponIndex);
    }

    private void SwitchWeapon(int weaponIndex)
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

        currentIndex = weaponIndex;
        currentWeapon = allWeapons[weaponIndex];
        RunActivateAnimation(currentWeapon);
    }

    public void DeactivateCurrentWeapon()
    {
        currentWeapon.transform.DOScale(0, .1f);
    }

    public void SetWeaponDependingOnFaceOrientation()
    {
        Transform maxDotResult = faceOrientations.OrderBy(face => Vector3.Dot(face.up, Vector3.up)).Last();
        Debug.Log(maxDotResult.name);
        SwitchWeapon(faceOrientations.IndexOf(maxDotResult));
    }
    
    private static void RunActivateAnimation(GameObject currentWeapon)
    {
        currentWeapon.transform.localScale = Vector3.zero;
        currentWeapon.GetComponent<IWeapon>().AddCooldown(.2f);
        Sequence s = DOTween.Sequence();
        s.Append(currentWeapon.transform.DOScale(1, .15f));
        s.Append(currentWeapon.transform.DOShakeScale(.5f, 1));
    }

#if UNITY_EDITOR
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
    }
#endif

}
