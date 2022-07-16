using UnityEngine;

public class FollowBehaviour : MonoBehaviour, EnemyBehaviour
{
    private Player player;
    private Rigidbody rb;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotSpeed;


    private void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    public void Setup(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        if (this.player == null)
        {
            return;
        }

        this.rb.MovePosition(this.transform.position + this.transform.forward * this.speed * Time.deltaTime);
        this.rb.transform.forward = Vector3.Lerp(this.transform.forward, (this.player.transform.position - this.transform.position).normalized, this.speed * Time.deltaTime);
    }
}
