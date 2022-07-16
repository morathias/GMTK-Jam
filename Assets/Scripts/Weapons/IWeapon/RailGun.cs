using System;
using System.Collections;
using UnityEngine;

public class RailGun : BaseWeapon
{
    public event Action OnShot;

    public bool CanShot
    {
        get { return (Time.time >= this.currentTimeToShot) && !this.blocked  && HasAmmo; }
    }

    public ParticleSystem projectilePrefab;
    public Transform spawnPoint;
    public float bulletCooldown;

    private float currentTimeToShot;
    private bool blocked;

    public float damage = 5;
    public float raycastRadius = .5f;
    public void Shoot()
    {
        if (this.CanShot)
        {
            this.OnShot?.Invoke();
            this.StartCoroutine(this.AfterFrameShot());
            this.currentTimeToShot = Time.time + this.bulletCooldown;
            //raycast shit
            RaycastHit[] all = Physics.SphereCastAll(spawnPoint.position, raycastRadius, spawnPoint.forward, 100, Physics.AllLayers);
            for (int i = 0; i < all.Length; i++)
            {
                RaycastHit hit = all[i];
                if (hit.collider.TryGetComponent(out HP hp))
                {
                    hp.TakeDamage(damage);
                }
            }
            currentAmmo--;
        }
    }

    private IEnumerator AfterFrameShot()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        this.TriggerVFX();
    }

    private void TriggerVFX()
    {
        ParticleSystem newProjectile = Instantiate(this.projectilePrefab);
        newProjectile.transform.position = this.spawnPoint.position;
        //newProjectile.transform.parent = this.spawnPoint;
        newProjectile.transform.forward = this.spawnPoint.forward;
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
