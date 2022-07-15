using DG.Tweening;
using UnityEngine;

public class GrenadeLauncherProjectile : MonoBehaviour
{
    public ParticleSystem onHitVFXPrefab;
    public Rigidbody rigidbody;

    private bool collided;

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
    }
}
