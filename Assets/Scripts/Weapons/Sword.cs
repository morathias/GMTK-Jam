using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public event Action OnShot;
    public Collider collider;

    public bool CanShot
    {
        get { return Time.time >= this.currentTimeToShot; }
    }

    public Animator anim;
    public float colliderTimeToActive = 0.1f;
    public float colliderActiveTime = 0.1f;
    public float bulletCooldown;

    private float currentTimeToShot;

    private Coroutine hitColliderCoroutine;

    public void Shoot()
    {
        if (this.CanShot)
        {
            collider.enabled = false;

            if (hitColliderCoroutine != null)
            {
                StopCoroutine(hitColliderCoroutine);
            }

            this.OnShot?.Invoke();
            anim.SetTrigger("hit");
            this.currentTimeToShot = Time.time + this.bulletCooldown;

            hitColliderCoroutine = StartCoroutine(HitCollider());
        }
    }

    private IEnumerator HitCollider()
    {
        yield return new WaitForSeconds(colliderTimeToActive);
        collider.enabled = true;
        yield return new WaitForSeconds(colliderActiveTime);
        collider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.Shoot();
        }
    }
}
