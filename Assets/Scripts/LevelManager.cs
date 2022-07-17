 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
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
    public event Action OnEnemiesSpawn;

    public NavMeshSurface navMeshSurface;

    public List<WorldList> worldsToUnlock;
    public List<int> worldUnlockWaves;
    public List<Enemy> allEnemies;
    public List<World> allWorlds;
    public float timeBeforeSpawn;
    public float levelUpTime;

    private bool leveling;
    private int deadEnemyCount;
    public int currentWave;

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
        this.currentWave++;
        this.leveling = true;
        this.OnLevelUpStarted?.Invoke();
        this.OpenNewWalls();
        this.StartCoroutine(this.WaitAndSpawnWorlds());
        this.StartCoroutine(this.WaitAndEndLevelUp());
    }

    public void EnemyDied()
    {
        this.deadEnemyCount++;
    }

    private IEnumerator WaitAndSpawnWorlds()
    {
        //Sin esto no vivimos
        yield return new WaitForSeconds(0.9f);
        if (this.worldUnlockWaves.Contains(this.currentWave))
        {
            foreach (World world in this.worldsToUnlock[this.worldUnlockWaves.IndexOf(this.currentWave)].wordList)
            {
                world.InstaScaleDown();
                world.IsActive = true;
                world.gameObject.SetActive(true);
            }
        }
    }

    private void OpenNewWalls()
    {
        if (this.worldUnlockWaves.Contains(this.currentWave))
        {
            this.UnlockWallsForNewWorld();
        }
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
        this.ReBakeNavMesh();
    }

    public void ReBakeNavMesh()
    {
        this.navMeshSurface.BuildNavMesh();
    }

    private void UnlockWallsForNewWorld()
    {
        foreach (World world in this.allWorlds.Where(x => x.IsActive))
        {
            world.UnlockWalls();
        }
    }

    private void SpawnEnemies()
    {
        this.allEnemies.Clear();

        allEnemies = new List<Enemy>();

        List<Enemy> allNewEnemies = new List<Enemy>();
        foreach (World world in this.allWorlds.Where(x => x.IsActive))
        {
            allNewEnemies.AddRange(world.enemySpawner.Spawn(world.CurrentFace + 1));
        }

        this.allEnemies = allNewEnemies;
        this.leveling = false;

        OnEnemiesSpawn?.Invoke();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.J))
        {
            this.LevelUp();
        }
#endif

        this.CheckForCurrentEnemyCount();
    }
}

[Serializable]
public class WorldList
{
    public List<World> wordList;
}
