using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    public Transform spawnPoint;
    public float cadence;

    [Header("Bullet")]
    public Bullet bulletPrefab;

    [Header("Animations")]
    public Animator animator;

    private float cooldown;
    private bool CanShoot => Time.time > cooldown;

    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    private void Shoot()
    {
        cooldown = Time.time + cadence;
        Bullet bullet = GetBulletInstance();
        bullet.transform.position = spawnPoint.position;
        bullet.transform.forward = spawnPoint.forward;
        bullet.AddForce(spawnPoint.forward);
        animator.SetTrigger(Shoot1);
    }

    private bool TriggerPressed => Input.GetKey(KeyCode.Mouse0);

    private void Update()
    {
        if (CanShoot && TriggerPressed)
        {
            Shoot();
        }

        animator.SetBool("shoot", TriggerPressed);
    }

    private Bullet GetBulletInstance()
    {
        Bullet instance = Instantiate(bulletPrefab);
        instance.damage.OnDamage += () =>
        {
            instance.gameObject.SetActive(false);
        };
        return instance;
    }

    private void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }
}
