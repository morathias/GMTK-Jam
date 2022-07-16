using System;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnFinishedBouncing;
    public float bounceDownTime;

    private EnemyBehaviour[] enemyBehaviours;

    private void Awake()
    {
        this.enemyBehaviours = this.GetComponentsInChildren<EnemyBehaviour>();
    }

    public void BounceDown()
    {
        RaycastHit raycastHit;
        Physics.Raycast(this.transform.position, -this.transform.up, out raycastHit, float.MaxValue);
        Vector3 targetPos = raycastHit.point;
        this.transform.DOMove(targetPos, this.bounceDownTime).SetEase(Ease.OutBounce).OnComplete(() => this.OnFinishedBouncing?.Invoke(this));
    }

    public void SetupBehaviours(Player player)
    {
        foreach (EnemyBehaviour item in this.enemyBehaviours)
        {
            item.Setup(player);
        }
    }
}
