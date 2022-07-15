using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour , EnemyBehaviour
{
    private Player player;
    private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Setup(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        rb.transform.forward = Vector3.Lerp(transform.forward, (player.transform.position - transform.position).normalized, speed * Time.deltaTime);
    }
}
