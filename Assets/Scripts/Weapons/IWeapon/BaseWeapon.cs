using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    public abstract void AddCooldown(float time);
    public abstract void Block();
    public abstract void Resume();

    public bool usesAmmo = true;
    public int ammo = 5;
    public int currentAmmo = 5;
    public float speedMultiplier = 1;

    protected virtual void OnEnable()
    {
        this.currentAmmo = this.ammo;
    }

    public virtual bool HasAmmo
    {
        get { return (this.currentAmmo > 0) || !this.usesAmmo; }
    }

    public int CurrentAmmo
    {
        get { return this.currentAmmo; }
    }

    public abstract void Trigger();

    /*public void OnGUI()
    {
        GUI.Label(new Rect(100, 0, 600, 600), $"{this.CurrentAmmo}");
    }*/
}
