using System.Collections.Generic;
using UnityEngine;

public class JumpWeaponsBlocker : MonoBehaviour
{
    public PlayerJumper PlayerJumper;
    public List<GameObject> allWeapons;

    private void Start()
    {
        this.PlayerJumper.OnJumpStarted += this.OnPlayerJumpStartedHandler;
        this.PlayerJumper.OnLandEnded += this.OnPlayerLandEndedHandler;
    }

    private void OnPlayerJumpStartedHandler(float obj)
    {
        foreach (GameObject weapon in this.allWeapons)
        {
            weapon.GetComponent<IWeapon>().Block();
        }
    }

    private void OnPlayerLandEndedHandler()
    {
        foreach (GameObject weapon in this.allWeapons)
        {
            weapon.GetComponent<IWeapon>().Resume();
        }
    }
}
