using System;
using System.Collections;
using UnityEngine;

public class Sword : BaseWeapon
{
    public event Action OnShot;
    public Collider collider;
    public TrailRenderer trailRenderer;
    public bool CanShot
    {
        get { return (Time.time >= this.currentTimeToShot) && !this.blocked && this.HasAmmo; }
    }

    public Animator anim;
    public float colliderTimeToActive = 0.1f;
    public float colliderActiveTime = 0.1f;
    public float bulletCooldown;

    private float currentTimeToShot;
    private bool blocked;
    private Coroutine hitColliderCoroutine;

    private void Slash()
    {
        this.collider.enabled = false;

        if (this.hitColliderCoroutine != null)
        {
            this.StopCoroutine(this.hitColliderCoroutine);
        }

        this.OnShot?.Invoke();
        this.anim.SetTrigger("hit");
        trailRenderer.gameObject.SetActive(true);
        this.currentTimeToShot = Time.time + this.bulletCooldown;

        this.hitColliderCoroutine = this.StartCoroutine(this.HitCollider());
    }

    private IEnumerator HitCollider()
    {
        yield return new WaitForSeconds(this.colliderTimeToActive);
        this.collider.enabled = true;
        yield return new WaitForSeconds(this.colliderActiveTime);
        this.collider.enabled = false;

        trailRenderer.gameObject.SetActive(false);
        trailRenderer.Clear();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.Trigger();
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

    public override bool HasAmmo
    {
        get { return true; }
    }

    public override void Trigger()
    {
        if (this.CanShot)
        {
            this.Slash();
        }
    }
}
