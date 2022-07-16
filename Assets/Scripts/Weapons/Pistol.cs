using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class Pistol : MonoBehaviour
{
    public Transform spawnPoint;
    public float cadence;
    
    [Header("Bullet")]
    public Bullet bulletPrefab;
    public float bulletLifetime = 3;
    
    [Header("Animations")]
    public Animator animator;
    
    private float cooldown;
    private ObjectPool<Bullet> pool;
    private bool CanShoot => Time.time > cooldown;


    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    private void Awake()
    {
        pool = new ObjectPool<Bullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool);
    }

    private void Shoot()
    {
        cooldown = Time.time + cadence;
        Bullet bullet = pool.Get();
        bullet.transform.position = spawnPoint.position;
        bullet.transform.forward = spawnPoint.forward;
        bullet.AddForce(spawnPoint.forward);
        StartCoroutine(WaitAndStore(bullet));
        animator.SetTrigger(Shoot1);
    }
    
    private bool TriggerPressed => Input.GetKey(KeyCode.Mouse0);
    
    private void Update()
    {
        if (CanShoot && TriggerPressed)
        {
            Shoot();
        }
    }

    private IEnumerator WaitAndStore(Bullet bullet)
    {
        yield return new WaitForSeconds(bulletLifetime);
        pool.Release(bullet);
    }

    private void OnReturnedToPool(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        obj.rigidbody.velocity = Vector3.zero;
    }

    private void OnTakeFromPool(Bullet obj)
    {
        obj.gameObject.SetActive(true);
        obj.transform.forward = spawnPoint.forward;
    }

    private Bullet CreatePooledItem()
    {
        return Instantiate(bulletPrefab);
    }

    private void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }
}