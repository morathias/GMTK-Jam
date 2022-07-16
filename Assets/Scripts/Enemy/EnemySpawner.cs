using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Enemy[] enemyToSpawn;
    public int spawnCount;

    private Player player;

    private void Start()
    {
        this.player = FindObjectOfType<Player>();
        //this.Spawn(this.spawnCount);
    }

    public List<Enemy> Spawn(int count)
    {
        List<Enemy> enemiesToSpawn = new List<Enemy>();

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = Instantiate(this.enemyToSpawn[Random.Range(0, this.enemyToSpawn.Length)]);
            enemy.transform.position = new Vector3(Random.Range(this.boxCollider.bounds.min.x, this.boxCollider.bounds.max.x), this.transform.position.y, Random.Range(this.boxCollider.bounds.min.z, this.boxCollider.bounds.max.z));
            enemy.BounceDown();
            enemy.OnFinishedBouncing += this.OnEnemyFinishedBouncing;
            enemiesToSpawn.Add(enemy);
        }

        return enemiesToSpawn;
    }

    private void OnEnemyFinishedBouncing(Enemy enemy)
    {
        enemy.OnFinishedBouncing -= this.OnEnemyFinishedBouncing;
        enemy.SetupBehaviours(this.player);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, this.boxCollider.size);
    }
}
