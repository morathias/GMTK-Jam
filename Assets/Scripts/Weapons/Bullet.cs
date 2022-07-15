using UnityEngine;

public class Bullet : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float force;
    
    public void AddForce(Vector3 direction)
    {
        rigidbody.AddForce(direction * force);
    }

    private void OnValidate()
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }
}
