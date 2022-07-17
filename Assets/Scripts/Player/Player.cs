using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerRotation playerRotation;
    public PlayerView playerView;
    public Dash dash;
    public WeaponSwitcher weaponSwitcher;

    private void Awake()
    {
        weaponSwitcher.OnWeaponSwitch += UpdateBulletsUI;
    }

    public void Block(bool blocked)
    {
        playerMovement.enabled = !blocked;
        playerRotation.enabled = !blocked;
        dash.enabled = !blocked;
    }

    private void Update()
    {
        playerView.IsRunning(playerMovement.Direction.magnitude, playerMovement.Direction.normalized);
        playerView.UpdateBulletsUI(weaponSwitcher.currentIWeapon.currentAmmo);
    }

    private void UpdateBulletsUI() 
    {
        playerView.SetupBulletsUI(weaponSwitcher.currentIWeapon.ammo, weaponSwitcher.currentIWeapon.transform.GetSiblingIndex());
    }

}
