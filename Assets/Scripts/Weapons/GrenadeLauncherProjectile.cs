using DG.Tweening;
using UnityEngine;

public class GrenadeLauncherProjectile : MonoBehaviour
{
    public ParticleSystem onHitVFXPrefab;
    public Rigidbody rigidbody;

    private bool collided;

    public float radius = 4;
    public float damage = 3;
    
    private void OnGroundCollisionHandler()
    {
        //explosion, trigger particles
        ParticleSystem newVFX = Instantiate(this.onHitVFXPrefab);
        newVFX.transform.position = this.transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.collided)
        {
            return;
        }

        this.OnGroundCollisionHandler();
        this.collided = true;
        this.transform.DOScale(0f, 0.5f).OnComplete(() => Destroy(this.gameObject));
        Collider[] all = Physics.OverlapSphere(other.contacts[0].point, radius, Physics.AllLayers);
        for (int i = 0; i < all.Length; i++)
        {
            Collider hit = all[i];
            if (hit.TryGetComponent(out HP hp))
            {
                hp.TakeDamage(damage);
            }
        }
    }
}
