using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpawnDice : MonoBehaviour
{
    [SerializeField] private Rigidbody rbToSpawn;
    void Start()
    {
        GetComponent<HP>().OnDead += OnDeath;
    }

    void OnDeath()
    {
        Rigidbody rb = Instantiate(rbToSpawn);
        rb.transform.position = transform.position;
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        rb.angularVelocity = Random.onUnitSphere * 5;
    }

}
