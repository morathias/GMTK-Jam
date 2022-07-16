using System;
using UnityEngine;

public class Minigun : BaseWeapon
{
    public Transform spawnPoint;
    public float cadence;

    [Header("Bullet")]
    public Bullet bulletPrefab;

    [Header("Animations")]
    public Animator animator;

    private float currentTimeToShot;

    private bool CanShoot
    {
        get { return (Time.time > this.currentTimeToShot) && !this.blocked  && HasAmmo; }
    }

    private bool blocked;
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    
    private void Shoot()
    {
        this.currentTimeToShot = Time.time + this.cadence;
        Bullet bullet = this.GetBulletInstance();
        bullet.transform.position = this.spawnPoint.position;
        bullet.transform.forward = this.spawnPoint.forward;
        bullet.AddForce(this.spawnPoint.forward);
        this.animator.SetTrigger(Shoot1);
        currentAmmo--;
    }

    private bool TriggerPressed
    {
        get { return Input.GetKey(KeyCode.Mouse0); }
    }

    private void Update()
    {
        if (this.CanShoot &&
            this.TriggerPressed)
        {
            this.Shoot();
        }

        this.animator.SetBool("shoot", this.TriggerPressed);
    }

    private Bullet GetBulletInstance()
    {
        Bullet instance = Instantiate(this.bulletPrefab);
        instance.damage.OnDamage += () => { instance.gameObject.SetActive(false); };
        return instance;
    }

    private void OnValidate()
    {
        if (this.animator == null)
        {
            this.animator = this.GetComponent<Animator>();
        }
    }

    public override void AddCooldown(float time)
    {
        this.currentTimeToShot = Mathf.Max(this.currentTimeToShot, Time.time + time);
    }

    public override void Block()
    {
        this.blocked = true;
    }

    public override void Resume()
    {
        this.blocked = false;
    }

}
