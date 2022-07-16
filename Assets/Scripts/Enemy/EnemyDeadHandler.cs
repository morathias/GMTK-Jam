using UnityEngine;

public class EnemyDeadHandler : MonoBehaviour
{
    public HP EnemyHP;

    private void Start()
    {
        this.EnemyHP.OnDead += this.OnDeadHandler;
    }

    private void OnDeadHandler()
    {
        this.EnemyHP.OnDead -= this.OnDeadHandler;
        LevelManager.Instance.EnemyDied();
        Destroy(this.gameObject);
    }
}
