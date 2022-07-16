using System;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    public abstract void AddCooldown(float time);
    public abstract void Block();
    public abstract void Resume();
    
    public int ammo = 5;
    protected int currentAmmo = 5;
    public float speedMultiplier = 1;
    
    protected virtual void OnEnable()
    {
        currentAmmo = ammo;
    }

    public virtual bool HasAmmo => currentAmmo > 0;
    public int CurrentAmmo => currentAmmo;

    public void OnGUI()
    {
        GUI.Label(new Rect(100,0,600,600), $"{CurrentAmmo}");
    }
}
