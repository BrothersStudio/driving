using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryMove : MonoBehaviour
{
    private float speed = 50;

    private EnemyCarSpawner car_spawn;

    private void Awake()
    {
        car_spawn = FindObjectOfType<EnemyCarSpawner>();
    }

    private void Update()
    {
        if (car_spawn.started)
        {
            speed += Time.deltaTime / 2f;
        }

        transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
        if (transform.position.z < -800)
        {
            float zPosOffset = transform.position.z + 800; 
            transform.position = new Vector3(transform.position.x, transform.position.y, 800 + zPosOffset);
        }
    }
}
