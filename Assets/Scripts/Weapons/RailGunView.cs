using UnityEngine;

public class RailGunView : MonoBehaviour
{
    public Animator animator;
    public RailGun railGun;

    private void Start()
    {
        this.Bind();
    }

    public void Bind()
    {
        this.railGun.OnShot += this.OnShotHandler;
    }

    public void Unbind()
    {
        this.railGun.OnShot -= this.OnShotHandler;
    }

    private void OnShotHandler()
    {
        this.animator.Play("Shoot");
    }

}
