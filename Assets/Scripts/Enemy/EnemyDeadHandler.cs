using UnityEngine;

public class EnemyDeadHandler : MonoBehaviour
{
    public Bullet bulletPrefab;
    public HP EnemyHP;
    public int bulletCount;

    private void Start()
    {
        this.EnemyHP.OnDead += this.OnDeadHandler;
    }

    private void OnDeadHandler()
    {
        this.EnemyHP.OnDead -= this.OnDeadHandler;
        LevelManager.Instance.EnemyDied();
        this.SpawnBullets();
        Destroy(this.gameObject);
    }

    private void SpawnBullets()
    {
        for (int i = 0; i < this.bulletCount; i++)
        {
            int anglePiece = 360 / this.bulletCount;
            float angle = anglePiece * i * Mathf.Deg2Rad;
            float xPoint = 1f * Mathf.Cos(angle);
            float yPoint = 1f * Mathf.Sin(angle);

            Vector3 point = this.transform.position + new Vector3(xPoint, 0, yPoint);

            Bullet newBullet = Instantiate(this.bulletPrefab);
            newBullet.transform.position = this.transform.position;
            newBullet.AddForce(point - this.transform.position);
        }
    }
}
