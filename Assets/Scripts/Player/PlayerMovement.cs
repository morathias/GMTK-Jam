using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12;
    public CharacterController charController;
    public float gravity = -10;
    public Camera camera;
    public Vector3 Direction =>  camera.transform.right * Input.GetAxisRaw("Horizontal") * speed + camera.transform.forward.ProjectUp().normalized * Input.GetAxisRaw("Vertical") * speed;

    public WeaponSwitcher weaponSwitcher;

    public Animator animator;
    private static readonly int Multiplier = Animator.StringToHash("Multiplier");

    void Update()
    {
        Vector3 direction = Direction;
        direction.y += gravity;
        animator.SetFloat(Multiplier, weaponSwitcher.currentIWeapon.speedMultiplier);
        charController.Move(direction * Time.deltaTime * weaponSwitcher.currentIWeapon.speedMultiplier);
    }


    private void OnValidate()
    {
        if(camera == null)
            camera = Camera.main;
    }
    
}
