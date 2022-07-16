using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance
    {
        get;
        private set;
    }

    public event Action OnLevelUpStarted;
    public event Action OnLevelUpEnded;

    public List<World> allWorlds;
    public float timeBeforeSpawn;
    public float levelUpTime;

    private void Awake()
    {
        Instance = this;
    }

    public void LevelUp()
    {
        this.OnLevelUpStarted?.Invoke();
        this.StartCoroutine(this.WaitAndEndLevelUp());
    }

    private IEnumerator WaitAndEndLevelUp()
    {
        yield return new WaitForSeconds(this.levelUpTime);
        this.OnLevelUpEnded?.Invoke();
        yield return new WaitForSeconds(this.timeBeforeSpawn);
        this.SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (World world in this.allWorlds)
        {
            world.enemySpawner.Spawn(world.CurrentFace + 1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            this.LevelUp();
        }
    }
}
