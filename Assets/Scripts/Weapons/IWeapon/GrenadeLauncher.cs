using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrenadeLauncher : MonoBehaviour, IWeapon
{
    public event Action OnShot;

    public bool CanShot
    {
        get { return Time.time >= this.currentTimeToShot; }
    }

    public GrenadeLauncherProjectile projectilePrefab;
    public Transform spawnPoint;
    public float shootingForce;
    public float bulletCooldown;

    private float currentTimeToShot;

    public void Shoot()
    {
        if (this.CanShot)
        {
            this.OnShot?.Invoke();
            this.StartCoroutine(this.AfterFrameShot());
            this.currentTimeToShot = Time.time + this.bulletCooldown;
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
        if (Input.GetButtonDown("Fire1"))
        {
            this.Shoot();
        }
    }


    public void AddCooldown(float time)
    {
        currentTimeToShot = Mathf.Max(currentTimeToShot, Time.time + time);
    }
}
