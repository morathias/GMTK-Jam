using System.Collections;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private Vector3[] eulers;
    [SerializeField]
    private Transform world;
    [SerializeField]
    private AnimationCurve curve;

    public void Rotate()
    {
        Vector3 toFace = this.eulers[Random.Range(0, this.eulers.Length)];
        this.StartCoroutine(this.RotateCoroutine(toFace));
    }

    private IEnumerator RotateCoroutine(Vector3 toFace)
    {
        Vector3 startingForward = this.world.localEulerAngles;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            this.world.localEulerAngles = Vector3.Lerp(startingForward, toFace, this.curve.Evaluate(t));
            yield return null;
        }

        this.world.localEulerAngles = toFace;
    }
}
