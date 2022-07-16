using System;
using System.Collections;
using UnityEngine;

public class RailGun : MonoBehaviour
{
    public event Action OnShot;

    public bool CanShot
    {
        get { return Time.time >= this.currentTimeToShot; }
    }

    public ParticleSystem projectilePrefab;
    public Transform spawnPoint;
    public float bulletCooldown;

    private float currentTimeToShot;

    public void Shoot()
    {
        if (this.CanShot)
        {
            this.OnShot?.Invoke();
            this.StartCoroutine(this.AfterFrameShot());
            this.currentTimeToShot = Time.time + this.bulletCooldown;
            //raycast shit
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
        newProjectile.transform.parent = this.spawnPoint;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.Shoot();
        }
    }
}
