using UnityEngine;

public class Pellet : MonoBehaviour
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
}
