using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Enemy[] enemyToSpawn;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Spawn(3);
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy enemy = Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)]);
            enemy.transform.position = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), 0, Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));
            enemy.SetupBehaviours(player);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxCollider.size);
    }
}
