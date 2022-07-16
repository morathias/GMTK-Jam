using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrenadeLauncher : BaseWeapon
{
    public event Action OnShot;

    public bool CanShot
    {
        get { return (Time.time >= this.currentTimeToShot) && !this.blocked && HasAmmo; }
    }

    public GrenadeLauncherProjectile projectilePrefab;
    public Transform spawnPoint;
    public float shootingForce;
    public float bulletCooldown;
    
    private bool blocked;
    private float currentTimeToShot;

    public void Shoot()
    {
        if (this.CanShot)
        {
            this.OnShot?.Invoke();
            this.StartCoroutine(this.AfterFrameShot());
            this.currentTimeToShot = Time.time + this.bulletCooldown;
            currentAmmo--;
        }
    }

    private IEnumerator AfterFrameShot()
    {
        yield return new WaitForEndOfFrame();
        GrenadeLauncherProjectile newProjectile = Instantiate(this.projectilePrefab);
        newProjectile.transform.position = this.spawnPoint.transform.position;
        newProjectile.rigidbody.AddForce(this.spawnPoint.transform.forward * this.shootingForce);
        newProjectile.rigidbody.AddTorque(Random.insideUnitSphere * 500f);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.Shoot();
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
