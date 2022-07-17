using DG.Tweening;
using UnityEngine;

public class CameraJumpHandler : MonoBehaviour
{
    public PlayerJumper playerJumper;
    public float targetFov;
    private float startingFov;
    [SerializeField] Camera bulletCam;

    private void Start()
    {
        this.playerJumper.OnJumpStarted += this.OnJumpedStartedHandler;
        this.playerJumper.OnLandStarted += this.OnLandStartedHandler;
        this.playerJumper.OnLandEnded += this.OnLandEndedHandler;
    }

    private void OnJumpedStartedHandler(float jumpTime)
    {
        FollowCamera.Instance.pause = true;
        this.startingFov = FollowCamera.Instance.camera.fieldOfView;
        bulletCam.DOFieldOfView(this.targetFov, jumpTime);
        FollowCamera.Instance.camera.DOFieldOfView(this.targetFov, jumpTime);
    }

    private void OnLandStartedHandler(float jumpTime)
    {
        bulletCam.DOFieldOfView(this.startingFov, jumpTime);
        FollowCamera.Instance.camera.DOFieldOfView(this.startingFov, jumpTime);
    }

    private void OnLandEndedHandler()
    {
        FollowCamera.Instance.pause = false;
    }
}
