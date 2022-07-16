using UnityEngine;

public class WorldRotationLevelUpHandler : MonoBehaviour
{
    public World world;

    private void Start()
    {
        PlayerJumper.Instance.OnJumpStarted += this.OnLevelUpStartedHandler;
        PlayerJumper.Instance.OnJumpEnded += this.OnLevelUpEndedHandler;
    }

    private void OnLevelUpEndedHandler()
    {
        this.world.Rotate();
    }

    private void OnLevelUpStartedHandler(float t)
    {
        this.world.ScaleDown();
    }
}
