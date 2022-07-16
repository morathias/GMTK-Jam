using System;
using System.Collections;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    public event Action OnShot;

    public bool CanShot
    {
        get { return (Time.time >= this.currentTimeToShot) && !this.blocked  && HasAmmo; }
    }

    public AudioSource shootAS;
    public AudioSource reloadAS;
    public Pellet projectilePrefab;
    public Transform spawnPoint;
    public float ammoCountPerShot;
    public float bulletCooldown;
    public float spreadAngle;

    private float currentTimeToShot;
    private bool blocked;

    public void Shoot()
    {
        if (this.CanShot)
        {
            this.OnShot?.Invoke();

            Vector3 leftDirection = Vector3.Lerp(-this.spawnPoint.right, this.transform.forward, this.spreadAngle.Remap(90, 0, 0, 1));
            Vector3 rightDirection = Vector3.Lerp(this.spawnPoint.right, this.transform.forward, this.spreadAngle.Remap(90, 0, 0, 1));

            for (int i = 0; i < this.ammoCountPerShot / 2f; i++)
            {
                Vector3 direction = Vector3.Lerp(this.spawnPoint.forward, leftDirection, i / (this.ammoCountPerShot / 2f));
                Pellet pellet = this.GetBulletInstance();
                pellet.transform.position = this.spawnPoint.position;
                pellet.transform.forward = this.spawnPoint.forward;
                pellet.AddForce(direction);

                this.currentTimeToShot = Time.time + this.bulletCooldown;
            }

            for (int i = 0; i < this.ammoCountPerShot / 2f; i++)
            {
                Vector3 direction = Vector3.Lerp(this.spawnPoint.forward, rightDirection, i / (this.ammoCountPerShot / 2f));
                Pellet pellet = this.GetBulletInstance();
                pellet.transform.position = this.spawnPoint.position;
                pellet.transform.forward = this.spawnPoint.forward;
                pellet.AddForce(direction);
                this.currentTimeToShot = Time.time + this.bulletCooldown;
            }

            this.shootAS.Play();
            this.StartCoroutine(this.AfterFrameSFX());
            currentAmmo--;
        }
    }


    private IEnumerator AfterFrameSFX()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        this.reloadAS.Play();
    }


    private Pellet GetBulletInstance()
    {
        Pellet instance = Instantiate(this.projectilePrefab);
        instance.damage.OnDamage += () => { instance.gameObject.SetActive(false); };
        return instance;
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
