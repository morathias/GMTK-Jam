using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrenadeLauncher : BaseWeapon
{
    public event Action OnShot;

    public bool CanShot
    {
        get { return (Time.time >= this.currentTimeToShot) && !this.blocked && this.HasAmmo; }
    }

    public bool shouldAimMouse = true;
    public GrenadeLauncherProjectile projectilePrefab;
    public Transform spawnPoint;
    public float shootingForce;
    public float bulletCooldown;
    public ParticleSystem muzzle;

    private Plane plane;
    private bool blocked;
    private float currentTimeToShot;

    public void Shoot()
    {
        this.OnShot?.Invoke();
        this.StartCoroutine(this.AfterFrameShot());
        this.currentTimeToShot = Time.time + this.bulletCooldown;
        this.currentAmmo--;
        if (muzzle != null)
        {
            muzzle.Play();
        }
    }

    private IEnumerator AfterFrameShot()
    {
        yield return new WaitForEndOfFrame();
        GrenadeLauncherProjectile newProjectile = Instantiate(this.projectilePrefab);
        newProjectile.transform.position = this.spawnPoint.transform.position;
        newProjectile.rigidbody.AddForce(this.spawnPoint.transform.forward * this.shootingForce);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.Trigger();
        }

        if (this.shouldAimMouse)
        {
            this.TargetMouse();
        }
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
