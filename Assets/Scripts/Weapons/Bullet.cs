﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float force;
    public Damage damage;
    public float bulletLifetime = 3;

    private void Awake()
    {
        this.damage.OnDamage += this.OnDamageHandler;
        Destroy(this.gameObject, this.bulletLifetime);
    }

    private void OnDamageHandler()
    {
        this.gameObject.SetActive(false);
    }

    public void AddForce(Vector3 direction)
    {
        this.rigidbody.AddForce(direction * this.force);
    }

    private void OnValidate()
    {
        if (this.rigidbody == null)
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
        }

        if (this.damage == null)
        {
            this.damage = this.GetComponent<Damage>();
        }
    }
}
