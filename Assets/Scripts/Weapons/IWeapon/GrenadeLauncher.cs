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

    public GrenadeLauncherProjectile projectilePrefab;
    public Transform spawnPoint;
    public float shootingForce;
    public float bulletCooldown;

    private Plane plane;
    private bool blocked;
    private float currentTimeToShot;

    public void Shoot()
    {
        this.OnShot?.Invoke();
        this.StartCoroutine(this.AfterFrameShot());
        this.currentTimeToShot = Time.time + this.bulletCooldown;
        this.currentAmmo--;
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
