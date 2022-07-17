using UnityEngine;
using UnityEngine.AI;

public class FollowBehaviour : MonoBehaviour, EnemyBehaviour
{
    public NavMeshAgent navMeshAgent;

    private Player player;

    public Transform view;
    public Vector3 rotationOffset;

    public void Setup(Player player)
    {
        this.player = player;
        this.navMeshAgent.enabled = true;
    }

    public void Update()
    {
        if (this.player == null)
        {
            return;
        }

        this.navMeshAgent.destination = this.player.transform.position;
        view.localRotation = Quaternion.identity;
        view.Rotate(rotationOffset, Space.World);
    }
}
