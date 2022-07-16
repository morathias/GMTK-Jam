using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dash : MonoBehaviour
{
    public Player player;
    public Transform diceView;
    public Rigidbody rb;

    public float dashCooldown;
    
    public float dashTime = .25f;
    public float dashForce = 3;
    public float rollCount = 4;
    private PlayerMovement PlayerMovement => player.playerMovement;
    private CharacterController CharController => PlayerMovement.charController;

    public WeaponSwitcher weaponSwitcher;
    public PlayerJumper playerJumper;

    private float cooldown;
    
    public bool CanDash => Time.time > cooldown;
    
    private void Start()
    {
        this.playerJumper.OnJumpStarted += (f) => enabled = false;
        this.playerJumper.OnLandEnded += () => enabled = true;
        cooldown = dashCooldown;
    }

    public void DashRoll(Vector3 direction)
    {
        player.Block(true);
        rb.isKinematic = false;
        CharController.enabled = false;
        
        rb.AddForce(direction * dashForce, ForceMode.Impulse);
        Sequence s = DOTween.Sequence();
        Vector3Int rndRot = Vector3Int.FloorToInt((Random.insideUnitSphere.normalized*360*rollCount)/90) * 90;
        s.Append(diceView.DORotate( transform.localEulerAngles + rndRot, dashTime).SetEase(Ease.Linear));
        s.AppendCallback(() =>
        {
            player.Block(false);
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            CharController.enabled = true;
            diceView.Rotate(player.playerRotation.rotationOffset, Space.World);
            weaponSwitcher.SetWeaponDependingOnFaceOrientation();
        });
        cooldown = Time.time + dashCooldown;
    }
  
    private Vector3 GetDashDirection()
    {
        Vector3 direction = PlayerMovement.Direction;
        if (direction == Vector3.zero)
            direction = transform.forward;
        return direction;
    }

    private void Update()
    {
        if (CanDash && Input.GetKeyDown(KeyCode.Space))
        {
            DashRoll(GetDashDirection());
        }

        if (!weaponSwitcher.currentIWeapon.HasAmmo)
        {
            DashRoll(Vector3.zero);
        }
    }


    private void OnGUI()
    {
        GUI.Label(new Rect(0,0,800,800), $"CanDash {CanDash}");
    }
}
