using System;
using System.Collections;
using UnityEngine;

public class Sword : BaseWeapon
{
    public event Action OnShot;
    public Collider collider;

    public bool CanShot
    {
        get { return (Time.time >= this.currentTimeToShot) && !this.blocked && HasAmmo; }
    }

    public Animator anim;
    public float colliderTimeToActive = 0.1f;
    public float colliderActiveTime = 0.1f;
    public float bulletCooldown;

    private float currentTimeToShot;
    private bool blocked;
    private Coroutine hitColliderCoroutine;

    public void Shoot()
    {
        if (this.CanShot)
        {
            this.collider.enabled = false;

            if (this.hitColliderCoroutine != null)
            {
                this.StopCoroutine(this.hitColliderCoroutine);
            }

            this.OnShot?.Invoke();
            this.anim.SetTrigger("hit");
            this.currentTimeToShot = Time.time + this.bulletCooldown;

            this.hitColliderCoroutine = this.StartCoroutine(this.HitCollider());
        }
    }

    private IEnumerator HitCollider()
    {
        yield return new WaitForSeconds(this.colliderTimeToActive);
        this.collider.enabled = true;
        yield return new WaitForSeconds(this.colliderActiveTime);
        this.collider.enabled = false;
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

    public override bool HasAmmo => true;
}
