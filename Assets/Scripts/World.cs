using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour
{
    [SerializeField] Vector3[] eulers;
    [SerializeField] Transform world;
    [SerializeField] AnimationCurve curve;

    public void Rotate()
    {
        Vector3 toFace = eulers[Random.Range(0, eulers.Length)];
        StartCoroutine(RotateCoroutine(toFace));
    }

    private IEnumerator RotateCoroutine(Vector3 toFace)
    {
        Vector3 startingForward = world.localEulerAngles;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            world.localEulerAngles = Vector3.Lerp(startingForward, toFace, curve.Evaluate(t));
            yield return null;
        }
        world.localEulerAngles = toFace;
    }

}
