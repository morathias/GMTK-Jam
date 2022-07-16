using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 12;
    public Camera camera;
    public Transform view;
    public Vector3 rotationOffset;

    Plane mousePlane = new Plane();
    
    void FixedUpdate()
    {
        Vector3 mouseInWorldPos = GetMouseInWorldPoint();
        Quaternion rot = Quaternion.LookRotation((mouseInWorldPos - transform.position).ProjectUp());
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.fixedDeltaTime);
        view.localRotation = Quaternion.identity;
        view.Rotate(rotationOffset, Space.World);
    }

    Vector3 GetMouseInWorldPoint()
    {
        mousePlane.SetNormalAndPosition(Vector3.up, transform.position);
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (mousePlane.Raycast(ray, out float dist))
        {
            return ray.GetPoint(dist);
        }
        return transform.position + transform.forward;
    }

    private void OnValidate()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }
}
