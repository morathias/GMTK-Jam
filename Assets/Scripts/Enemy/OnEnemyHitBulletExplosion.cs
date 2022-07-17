using UnityEngine;

public class OnEnemyHitBulletExplosion : MonoBehaviour
{
    public Bullet bulletPrefab;
    public HP hp;
    public int bulletCount;

    private void Start()
    {
        this.hp.OnDamage += this.OnDamageHandler;
    }

    private void OnDamageHandler()
    {
        this.SpawnBullets();
    }

    private void SpawnBullets()
    {
        float randomAngle = Random.Range(0, 2f);

        for (int i = 0; i < this.bulletCount; i++)
        {
            int anglePiece = 360 / this.bulletCount;
            float angle = anglePiece * i * Mathf.Deg2Rad;
            float xPoint = Mathf.Cos(angle + randomAngle);
            float yPoint = Mathf.Sin(angle + randomAngle);

            Vector3 point = this.transform.position + new Vector3(xPoint, 0, yPoint);

            Bullet newBullet = Instantiate(this.bulletPrefab);
            newBullet.transform.position = this.transform.position;
            newBullet.AddForce(point - this.transform.position);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.SpawnBullets();
        }
    }
}
