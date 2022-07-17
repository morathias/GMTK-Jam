using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private RectTransform bulletsUI = null;
    [SerializeField] private GameObject bulletUIPrefab = null;

    [SerializeField] List<Sprite> bulletSprites;

    private int currentAmmo = 0;

    private Camera cam = null;

    private void Start()
    {
        cam = Camera.main;
    }

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

    public void SetupBulletsUI(int bulletAmount, int bulletType) 
    {
        currentAmmo = bulletAmount;
        for (int i = 0; i < bulletsUI.childCount; i++)
        {
            Destroy(bulletsUI.GetChild(i).gameObject);
        }
        for (int i = 0; i < bulletAmount; i++)
        {
            Image bulletUI = Instantiate(bulletUIPrefab, bulletsUI).GetComponent<Image>();
            bulletUI.sprite = bulletSprites[bulletType];
        }
    }

    public void UpdateBulletsUI(int ammo) 
    {
        bulletsUI.parent.transform.LookAt(cam.transform);
        if (ammo >= currentAmmo) return;

        Destroy(bulletsUI.GetChild(0).gameObject);
        currentAmmo = ammo;
    }
}
