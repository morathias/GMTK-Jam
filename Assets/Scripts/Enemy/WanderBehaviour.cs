using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : MonoBehaviour, EnemyBehaviour
{
    private Player player;
    private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float timeToChangeDirection;
    [SerializeField] private float percentToStayIdle;

    private Vector3 currentDiretion;
    private float currentTime;

    private bool isIdle = false;

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
        currentTime += Time.deltaTime;
        if(currentTime > timeToChangeDirection)
        {
            float random = Random.Range(0, 100);
            isIdle = random <= percentToStayIdle;
            currentDiretion = Random.insideUnitSphere.ProjectUp();
            currentTime = 0;
        }
        else
        {
            if(!isIdle)
            {
                rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
                rb.transform.forward = Vector3.Lerp(transform.forward, currentDiretion.normalized, speed * Time.deltaTime);
            }
        }
    }
}
