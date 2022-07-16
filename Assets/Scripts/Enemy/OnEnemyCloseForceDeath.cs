using UnityEngine;

public class OnEnemyCloseForceDeath : MonoBehaviour, EnemyBehaviour
{
    public HP hp;
    public float distanceToExplode;

    private Player player;

    private void CheckForExplosion()
    {
        if (this.player == null)
        {
            return;
        }

        if (Vector3.Distance(this.transform.position, this.player.transform.position) <= this.distanceToExplode)
        {
            this.hp.TakeDamage(this.hp.currentHP);
        }
    }

    public void Setup(Player player)
    {
        this.player = player;
    }

    private void Update()
    {
        this.CheckForExplosion();
    }
}
