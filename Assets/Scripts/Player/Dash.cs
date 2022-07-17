using DG.Tweening;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public Player player;
    public Transform diceView;
    public Rigidbody rb;

    public float dashCooldown;

    public float dashTime = .25f;
    public float dashForce = 3;
    public float rollCount = 4;

    public AudioSource dashSfx;

    private PlayerMovement PlayerMovement
    {
        get { return this.player.playerMovement; }
    }

    private CharacterController CharController
    {
        get { return this.PlayerMovement.charController; }
    }

    public WeaponSwitcher weaponSwitcher;
    public PlayerJumper playerJumper;
    private Sequence s;
    private Vector3 lastRot;
    private float cooldown;

    public bool CanDash
    {
        get { return Time.time > this.cooldown; }
    }

    private void Start()
    {
        this.playerJumper.OnJumpStarted += (f) =>
        {
            this.enabled = false;
            this.rb.isKinematic = true;
            this.player.Block(true);
        };
        this.playerJumper.OnLandEnded += () =>
        {
            this.enabled = true;
            this.rb.isKinematic = false;
            this.player.Block(false);
        };
        this.cooldown = this.dashCooldown;
    }

    public void DashRoll(Vector3 direction)
    {
        dashSfx.Play();
        this.player.Block(true);
        this.rb.isKinematic = false;
        this.CharController.enabled = false;

        this.rb.AddForce(direction * this.dashForce, ForceMode.Impulse);
        this.s = DOTween.Sequence();
        Vector3Int rndRot = Vector3Int.FloorToInt(Random.insideUnitSphere.normalized * 360 * this.rollCount / 90) * 90;
        this.lastRot = rndRot;
        this.s.Append(this.diceView.DORotate(this.transform.localEulerAngles + rndRot, this.dashTime).SetEase(Ease.Linear));
        this.s.AppendCallback(() =>
        {
            if (!this.playerJumper.IsJumping)
            {
                this.player.Block(false);
            }

            this.rb.isKinematic = true;
            this.rb.velocity = Vector3.zero;
            this.CharController.enabled = true;
            this.diceView.Rotate(this.player.playerRotation.rotationOffset, Space.World);
            this.weaponSwitcher.SetWeaponDependingOnFaceOrientation();
        });
        this.cooldown = Time.time + this.dashCooldown;
    }

    private Vector3 GetDashDirection()
    {
        Vector3 direction = this.PlayerMovement.Direction;
        if (direction == Vector3.zero)
        {
            direction = this.transform.forward;
        }

        return direction;
    }

    private void Update()
    {
        if (this.CanDash &&
            Input.GetKeyDown(KeyCode.Space))
        {
            this.DashRoll(this.GetDashDirection());
        }

        if (!this.weaponSwitcher.currentIWeapon.HasAmmo)
        {
            this.DashRoll(Vector3.zero);
        }
    }


    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 800, 800), $"CanDash {this.CanDash}");
    }
}
