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
    [SerializeField]
    private float minScale;
    [SerializeField]
    private float scaleSpeed;

    public void Rotate()
    {
        Vector3 toFace = this.eulers[Random.Range(0, this.eulers.Length)];
        this.StartCoroutine(this.RotateCoroutine(toFace));
    }

    public void ScaleDown()
    {
        this.StartCoroutine(this.ScaleDownCoroutine());
    }

    private IEnumerator ScaleDownCoroutine()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            this.world.localScale = Vector3.Lerp(Vector3.one, Vector3.one * this.minScale, this.curve.Evaluate(t));
            yield return null;
        }
    }

    private IEnumerator RotateCoroutine(Vector3 toFace)
    {
        Vector3 startingForward = this.world.localEulerAngles;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            this.world.localEulerAngles = Vector3.Lerp(startingForward, toFace, this.curve.Evaluate(t));
            yield return null;
        }

        for (float t = 0; t < 1f; t += Time.deltaTime * this.scaleSpeed)
        {
            this.world.localScale = Vector3.Lerp(Vector3.one * this.minScale, Vector3.one, this.curve.Evaluate(t));
            yield return null;
        }

        this.world.localEulerAngles = toFace;
    }
}
