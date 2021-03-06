using UnityEngine;

public class Pistol : BaseWeapon
{
    public Transform spawnPoint;
    public float cadence;

    [Header("Bullet")]
    public Bullet bulletPrefab;

    [Header("Animations")]
    public Animator animator;

    private float currentTimeToShot;
    public bool shouldAimMouse = true;

    public ParticleSystem muzzle;

    public bool CanShoot
    {
        get { return (Time.time > this.currentTimeToShot) && !this.blocked && this.HasAmmo; }
    }

    private bool blocked;
    private Plane plane;

    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    public override void Trigger()
    {
        if (this.CanShoot)
        {
            this.Shoot();
        }
    }

    private void Shoot()
    {
        this.currentTimeToShot = Time.time + this.cadence;
        Bullet bullet = this.GetBulletInstance();
        bullet.transform.position = this.spawnPoint.position;
        bullet.transform.forward = this.spawnPoint.forward;
        bullet.AddForce(this.spawnPoint.forward);
        this.animator.SetTrigger(Shoot1);
        this.currentAmmo--;
        if (muzzle != null)
        {
            muzzle.Play();
        }
    }

    private bool TriggerPressed
    {
        get { return Input.GetKey(KeyCode.Mouse0); }
    }

    private void Update()
    {
        if (this.TriggerPressed)
        {
            this.Trigger();
        }

        if (this.shouldAimMouse)
        {
            this.TargetMouse();
        }
    }

    private void TargetMouse()
    {
        this.plane.SetNormalAndPosition(Vector3.up, this.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        this.plane.Raycast(ray, out float distance);
        Vector3 worldPosMouse = ray.GetPoint(distance);
        this.transform.forward = worldPosMouse - this.transform.position;
    }

    private Bullet GetBulletInstance()
    {
        Bullet instance = Instantiate(this.bulletPrefab);
        instance.damage.OnDamage += () => { instance.gameObject.SetActive(false); };
        return instance;
    }

    private void OnValidate()
    {
        if (this.animator == null)
        {
            this.animator = this.GetComponent<Animator>();
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
