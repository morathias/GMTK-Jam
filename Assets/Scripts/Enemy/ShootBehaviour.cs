using UnityEngine;

public class ShootBehaviour : MonoBehaviour, EnemyBehaviour
{
    public BaseWeapon weapon;

    public float distanceToShoot;

    private BaseWeapon baseWeapon;
    private Player player;

    private void Awake()
    {
        this.baseWeapon = this.weapon.GetComponent<BaseWeapon>();
    }

    public void Setup(Player player)
    {
        this.player = player;
    }

    private void Update()
    {
        if (this.player == null)
        {
            return;
        }

        if (Vector3.Distance(this.transform.position, this.player.transform.position) <= this.distanceToShoot)
        {
            this.transform.forward = Vector3.Lerp(this.transform.forward, this.player.transform.position - this.transform.position, Time.deltaTime * 3f);
            this.baseWeapon.Trigger();
        }
    }
}
