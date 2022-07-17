using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class CollectableSpawner : MonoBehaviour
    {
        public LevelManager levelManager;
        public PlayerJumper playerJumper;
        
        public float minTime = 10;
        public float maxTime = 20;

        public int maxCollectables = 2;
        public Medkit medkit;

        public List<Medkit> instances;
        
        private void Awake()
        {
            StartCoroutine(SpawnCollectable());
            playerJumper.OnJumpStarted += OnLandEnded;
            playerJumper.OnLandEnded += ClearAll;
        }

        private void OnLandEnded(float obj)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            instances.ForEach(m => Destroy(m.gameObject));
            instances.Clear();
        }

        private IEnumerator SpawnCollectable()
        {
            while (true)
            {
                yield return new WaitWhile(() => instances.Count == maxCollectables);
                yield return new WaitForSeconds(Random.Range(minTime, maxTime));
                Medkit instance = Instantiate(medkit);
                instance.OnDestroy += (m) =>
                {
                    instances.Remove(m);
                };
                instances.Add(instance);
                World world = levelManager.allWorlds.Where(w => w.IsActive).ToList().Random();
                instance.transform.position = world.collectableSpawnPoints[world.CurrentFace].collectablesSpawnPoint.Random().position + Random.insideUnitSphere.ProjectUp().normalized;
            }
        }
    }
}