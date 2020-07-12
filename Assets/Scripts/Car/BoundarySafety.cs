using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundarySafety : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x > 10.5f || transform.position.x < -10.5f)
        {
            transform.position = Vector3.zero;
        }
    }
}
