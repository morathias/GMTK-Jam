using UnityEngine;

public class ShotgunView : MonoBehaviour
{
    public Animator animator;
    public Shotgun shotgun;

    private void Start()
    {
        this.Bind();
    }

    public void Bind()
    {
        this.shotgun.OnShot += this.OnShotHandler;
    }

    public void Unbind()
    {
        this.shotgun.OnShot -= this.OnShotHandler;
    }

    private void OnShotHandler()
    {
        this.animator.Play("Shoot");
    }
}
