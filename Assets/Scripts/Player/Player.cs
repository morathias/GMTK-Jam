using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerRotation playerRotation;
    public PlayerView playerView;
    public Dash dash;
    
    public void Block(bool blocked)
    {
        playerMovement.enabled = !blocked;
        playerRotation.enabled = !blocked;
        dash.enabled = !blocked;
    }

    private void Update()
    {
        playerView.IsRunning(playerMovement.Direction.magnitude, playerMovement.Direction.normalized);
    }

}
