using System;
using DG.Tweening;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    public static PlayerJumper Instance
    {
        get;
        private set;
    }

    public event Action<float> OnJumpStarted;
    public event Action OnJumpEnded;
    public event Action<float> OnLandStarted;
    public event Action OnLandEnded;
    public LayerMask jumpLM;

    public CharacterController characterController;
    public float jumpHeight;
    public float jumpTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelManager.Instance.OnLevelUpStarted += this.OnLevelUpStartedManager;
        LevelManager.Instance.OnLevelUpEnded += this.OnLevelUpEndedManager;
    }

    private void OnLevelUpStartedManager()
    {
        this.StartJumpPlayerOut();
    }

    private void OnLevelUpEndedManager()
    {
        this.StartJumpPlayerIn();
    }

    public void StartJumpPlayerOut()
    {
        Vector3 targetJumpingPosition = new Vector3(this.transform.position.x, this.transform.position.y + this.jumpHeight, this.transform.position.z);
        this.characterController.enabled = false;
        this.OnJumpStarted?.Invoke(this.jumpTime);
        this.transform.DOMove(targetJumpingPosition, this.jumpTime).OnComplete(() => { this.OnJumpEnded?.Invoke(); });
    }

    public void StartJumpPlayerIn()
    {
        RaycastHit raycastHit;
        Physics.Raycast(this.transform.position, -Vector3.up, out raycastHit, float.MaxValue, this.jumpLM);
        Vector3 targetJumpingPosition = raycastHit.point;
        this.characterController.enabled = false;
        this.OnLandStarted?.Invoke(this.jumpTime);

        this.transform.DOMove(targetJumpingPosition, this.jumpTime)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                this.OnLandEnded?.Invoke();
                this.characterController.enabled = true;
            });
    }
}
