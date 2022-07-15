using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyBehaviour[] enemyBehaviours;

    private void Awake()
    {
        enemyBehaviours = GetComponentsInChildren<EnemyBehaviour>();
    }

    public void SetupBehaviours(Player player)
    {
        foreach (var item in enemyBehaviours)
        {
            item.Setup(player);
        }
    }

}
