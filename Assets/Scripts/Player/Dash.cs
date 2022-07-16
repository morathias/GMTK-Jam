using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dash : MonoBehaviour
{
    public Player player;
    public Transform diceView;
    public Rigidbody rb;

    public float dashTime = .25f;
    public float dashForce = 3;
    public float rollCount = 4;
    private PlayerMovement PlayerMovement => player.playerMovement;
    private CharacterController CharController => PlayerMovement.charController;

    public WeaponSwitcher weaponSwitcher;
    
    public void DashRoll()
    {
        player.Block(true);
        rb.isKinematic = false;
        CharController.enabled = false;
        
        Vector3 direction = GetDashDirection();
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashRoll();
        }
    }

}
