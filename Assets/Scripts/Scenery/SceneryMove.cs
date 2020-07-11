using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryMove : MonoBehaviour
{
    private float speed = 50;

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
        if (transform.position.z < -400)
        {
            Destroy(gameObject);
        }
    }
}
