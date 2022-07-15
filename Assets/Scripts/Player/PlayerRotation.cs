using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 12;
    public Camera camera;
    
    void FixedUpdate()
    {
        Vector3 mouseInWorldPos = GetMouseInWorldPoint();
        Quaternion rot = Quaternion.LookRotation((mouseInWorldPos - transform.position).ProjectUp());
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.fixedDeltaTime);
    }

    Vector3 GetMouseInWorldPoint()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycast, 100))
        {
            return raycast.point;
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
