using System;
using System.Collections;
using UnityEngine;

public class RailGun : BaseWeapon
{
    public event Action OnShot;

    public bool CanShot
    {
        get { return (Time.time >= this.currentTimeToShot) && !this.blocked && this.HasAmmo; }
    }

    public ParticleSystem projectilePrefab;
    public Transform spawnPoint;
    public float bulletCooldown;

    private float currentTimeToShot;
    private bool blocked;

    public float damage = 5;
    public float raycastRadius = .5f;

    private Plane plane;

    public void Shoot()
    {
        this.OnShot?.Invoke();
        this.StartCoroutine(this.AfterFrameShot());
        this.currentTimeToShot = Time.time + this.bulletCooldown;
        //raycast shit
        RaycastHit[] all = Physics.SphereCastAll(this.spawnPoint.position, this.raycastRadius, this.spawnPoint.forward, 100, Physics.AllLayers);
        for (int i = 0; i < all.Length; i++)
        {
            RaycastHit hit = all[i];
            if (hit.collider.TryGetComponent(out HP hp))
            {
                hp.TakeDamage(this.damage);
            }
        }

        this.currentAmmo--;
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
            this.Trigger();
        }

        this.TargetMouse();
    }

    private void TargetMouse()
    {
        this.plane.SetNormalAndPosition(Vector3.up, this.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        this.plane.Raycast(ray, out float distance);
        Vector3 worldPosMouse = ray.GetPoint(distance);
        this.transform.forward = worldPosMouse - this.transform.position;
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

    public override void Trigger()
    {
        if (this.CanShot)
        {
            this.Shoot();
        }
    }
}
