using System;
using DG.Tweening;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    public event Action<float> OnJumpStarted;
    public event Action<float> OnLandStarted;
    public event Action OnLandEnded;

    public CharacterController characterController;
    public float jumpHeight;
    public float jumpTime;

    private Vector3 targetJumpingPosition;
    private Vector3 startingJumpPosition;
    private float jumpT;
    private bool jumping;

    public void StartJumpPlayerOut()
    {
        this.jumping = true;
        this.jumpT = 0f;
        this.startingJumpPosition = this.transform.position;
        this.targetJumpingPosition = new Vector3(this.startingJumpPosition.x, this.startingJumpPosition.y + this.jumpHeight, this.startingJumpPosition.z);
        this.characterController.enabled = false;

        this.OnJumpStarted?.Invoke(this.jumpTime);

        DOTween.To(() => this.jumpT, x => this.jumpT = x, 1, this.jumpTime)
            .OnComplete(() => this.jumping = false);
    }

    public void StartJumpPlayerIn()
    {
        this.jumping = true;
        this.jumpT = 0f;
        this.startingJumpPosition = this.transform.position;
        RaycastHit raycastHit;
        Physics.Raycast(this.transform.position, -Vector3.up, out raycastHit, float.MaxValue);
        this.targetJumpingPosition = raycastHit.point;
        this.characterController.enabled = false;

        this.OnLandStarted?.Invoke(this.jumpTime);

        DOTween.To(() => this.jumpT, x => this.jumpT = x, 1, this.jumpTime)
            .OnComplete(() =>
            {
                this.OnLandEnded?.Invoke();
                this.jumping = false;
                this.characterController.enabled = true;
            });
    }

    private void JumpPlayer()
    {
        Vector3 jumpLerp = Vector3.Lerp(this.startingJumpPosition, this.targetJumpingPosition, this.jumpT);
        this.transform.position = jumpLerp;
    }

    private void Update()
    {
        if (this.jumping)
        {
            this.JumpPlayer();
        }

        if (!this.jumping)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                this.StartJumpPlayerOut();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                this.StartJumpPlayerIn();
            }
        }
    }
}
