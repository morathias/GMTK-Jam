using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownAndDestroy : MonoBehaviour
{
    void Update()
    {
        transform.localScale -= Vector3.one * 2f * Time.deltaTime;

        if( transform.localScale.x < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
