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

    public bool IsJumping
    {
        get;
        private set;
    }
    
    public event Action<float> OnJumpStarted;
    public event Action OnJumpEnded;
    public event Action<float> OnLandStarted;
    public event Action OnLandEnded;
    public LayerMask jumpLM;
    public ParticleSystem dustParticles, landingParticles;
    public Animator anim;

    public CharacterController characterController;
    public float jumpHeight;
    public float jumpTime;

    ParticleSystem.EmissionModule em;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelManager.Instance.OnLevelUpStarted += this.OnLevelUpStartedManager;
        LevelManager.Instance.OnLevelUpEnded += this.OnLevelUpEndedManager;
        em = dustParticles.emission;
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
        anim.SetBool("Jumping", true);
        em.enabled = false;
        Vector3 targetJumpingPosition = new Vector3(this.transform.position.x, this.transform.position.y + this.jumpHeight, this.transform.position.z);
        this.characterController.enabled = false;
        this.OnJumpStarted?.Invoke(this.jumpTime);
        IsJumping = true;
        this.transform.DOMove(targetJumpingPosition, this.jumpTime).OnComplete(() => { this.OnJumpEnded?.Invoke(); });
    }

    public void StartJumpPlayerIn()
    {
        anim.SetBool("Jumping", false);
        RaycastHit raycastHit;
        Physics.Raycast(this.transform.position, -Vector3.up, out raycastHit, float.MaxValue, this.jumpLM);
        Vector3 targetJumpingPosition = raycastHit.point;
        this.characterController.enabled = false;
        this.OnLandStarted?.Invoke(this.jumpTime);

        landingParticles.Play();
        this.transform.DOMove(targetJumpingPosition, this.jumpTime)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                IsJumping = false;
                this.OnLandEnded?.Invoke();
                this.characterController.enabled = true;

                em.enabled = true;
            });
    }
}
