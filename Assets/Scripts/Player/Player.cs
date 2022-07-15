using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;

        public void Block(bool blocked)
        {
            playerMovement.enabled = !blocked;
            playerRotation.enabled = !blocked;
        }
    }
}