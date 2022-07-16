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

    public List<Enemy> allEnemies;
    public List<World> allWorlds;
    public float timeBeforeSpawn;
    public float levelUpTime;

    private bool leveling;
    private int deadEnemyCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.Init();
    }

    public void Init()
    {
        this.SpawnEnemies();
    }

    public void LevelUp()
    {
        this.deadEnemyCount = 0;
        this.leveling = true;
        this.OnLevelUpStarted?.Invoke();
        this.StartCoroutine(this.WaitAndEndLevelUp());
    }

    public void EnemyDied()
    {
        this.deadEnemyCount++;
    }

    private void CheckForCurrentEnemyCount()
    {
        if ((this.deadEnemyCount >= this.allEnemies.Count) &&
            !this.leveling)
        {
            this.LevelUp();
        }
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
        this.allEnemies.Clear();
        List<Enemy> allNewEnemies = new List<Enemy>();
        foreach (World world in this.allWorlds)
        {
            allNewEnemies.AddRange(world.enemySpawner.Spawn(world.CurrentFace + 1));
        }

        this.allEnemies = allNewEnemies;
        this.leveling = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            this.LevelUp();
        }

        this.CheckForCurrentEnemyCount();
    }
}
