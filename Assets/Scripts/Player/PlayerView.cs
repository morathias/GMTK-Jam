using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    public void IsRunning(float speed, Vector3 dir) 
    {
        animator.SetBool("Running", speed > 0);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = (Camera.main.transform.position - transform.position).magnitude;

        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x -= playerScreenPosition.x;
        mousePosition.y -= playerScreenPosition.y;
        Vector2 view = new Vector2(mousePosition.x, mousePosition.y);
        view.Normalize();
        float dotViewDir = Vector2.Dot(dir, view);
        float dotViewUp = Vector2.Dot(view, Vector2.up);
        float dotViewRight = Vector2.Dot(view, Vector2.right);

        dotViewUp *= dir.x;
        dotViewRight *= dir.y;

        animator.SetFloat("BlendX", dotViewUp - dotViewRight);
        animator.SetFloat("BlendY", dotViewDir);
    }
}
