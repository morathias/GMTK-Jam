using UnityEngine;

public class GrenadeLauncherView : MonoBehaviour
{
    public GrenadeLauncher grenadeLauncher;
    public Animator animator;

    private void Start()
    {
        this.Bind();
    }

    public void Bind()
    {
        this.grenadeLauncher.OnShot += this.OnShotHandler;
    }

    public void Unbind()
    {
        this.grenadeLauncher.OnShot -= this.OnShotHandler;
    }

    private void OnShotHandler()
    {
        this.animator.Play("Shoot");
    }
}
