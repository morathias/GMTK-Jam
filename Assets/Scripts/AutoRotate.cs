using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    Vector3 direction;
    private void Start()
    {
        direction = Random.onUnitSphere * 15;
    }

    void Update()
    {
        transform.Rotate(direction * Time.deltaTime);
    }
}
