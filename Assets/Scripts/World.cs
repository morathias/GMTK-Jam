using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class World : MonoBehaviour
{
    public AudioSource clip;
    public int CurrentFace
    {
        get;
        private set;
    }

    public EnemySpawner enemySpawner;

    public bool IsActive
    {
        get { return this.isActive; }
        set { this.isActive = value; }
    }

    public List<WallList> wallsToUnlock;

    [SerializeField]
    private Vector3[] eulers;
    [SerializeField]
    private Transform world;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float minScale;
    [SerializeField]
    private float scaleSpeed;
    [SerializeField]
    private bool isActive;

    private int wallUnlockTier;

    public List<SpawnPoints> collectableSpawnPoints;

    [Serializable]
    public class SpawnPoints
    {
        public List<Transform> collectablesSpawnPoint = new List<Transform>();
    }
    public void Rotate()
    {
        clip.Play();
        clip.pitch = Random.Range(0.8f, 1.2f);
        int faceIndex = Random.Range(0, this.eulers.Length);
        Vector3 toFace = this.eulers[faceIndex];
        this.CurrentFace = faceIndex;
        this.StartCoroutine(this.RotateCoroutine(toFace));
    }

    public void UnlockWalls()
    {
        if (this.wallUnlockTier < this.wallsToUnlock.Count)
        {
            foreach (GameObject wall in this.wallsToUnlock[this.wallUnlockTier].walls)
            {
                wall.SetActive(false);
            }

            this.wallUnlockTier++;
        }
    }

    public void ScaleDown()
    {
        this.StartCoroutine(this.ScaleDownCoroutine());
    }

    public void InstaScaleDown()
    {
        this.world.localScale = Vector3.one * this.minScale;
    }

    private IEnumerator ScaleDownCoroutine()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            this.world.localScale = Vector3.Lerp(Vector3.one, Vector3.one * this.minScale, this.curve.Evaluate(t));
            yield return null;
        }
    }

    private IEnumerator RotateCoroutine(Vector3 toFace)
    {
        Vector3 startingForward = this.world.localEulerAngles;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            this.world.localEulerAngles = Vector3.Lerp(startingForward, toFace, this.curve.Evaluate(t));
            yield return null;
        }

        for (float t = 0; t < 1f; t += Time.deltaTime * this.scaleSpeed)
        {
            this.world.localScale = Vector3.Lerp(Vector3.one * this.minScale, Vector3.one, this.curve.Evaluate(t));
            yield return null;
        }

        clip.Stop();

        this.world.localEulerAngles = toFace;
    }
}

[Serializable]
public class WallList
{
    public List<GameObject> walls;
}
