using UnityEngine;

public class Bullet : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float force;
    public Damage damage;
    public float bulletLifetime = 3;

    private void Awake()
    {
        damage.OnDamage += OnDamageHandler;
        Destroy(gameObject, bulletLifetime);
    }

    private void OnDamageHandler()
    {
        gameObject.SetActive(false);
    }

    public void AddForce(Vector3 direction)
    {
        rigidbody.AddForce(direction * force);
    }

    private void OnValidate()
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        if (damage == null)
        {
            damage = GetComponent<Damage>();
        }
    }
}
