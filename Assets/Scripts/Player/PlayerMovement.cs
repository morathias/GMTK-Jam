using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

    public Vector3 Direction => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    
    void FixedUpdate()
    {
        transform.position += Direction * speed * Time.fixedDeltaTime;
    }
    
}
