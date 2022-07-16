using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds;

    private void Start()
    {
        Destroy(this.gameObject, this.seconds);
    }
}
