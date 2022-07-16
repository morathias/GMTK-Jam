using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12;
    public CharacterController charController;
    public float gravity = -10;
    public Camera camera;

    void Update()
    {
        Vector3 direction =  camera.transform.right * Input.GetAxisRaw("Horizontal") * speed + camera.transform.forward.ProjectUp().normalized * Input.GetAxisRaw("Vertical") * speed;
        direction.y += gravity;
        charController.Move(direction * Time.deltaTime);
    }


    private void OnValidate()
    {
        if(camera == null)
            camera = Camera.main;
    }
    
}
