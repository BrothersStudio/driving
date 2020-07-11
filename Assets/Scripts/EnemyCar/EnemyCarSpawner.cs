using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarSpawner : MonoBehaviour
{
    private float cooldown = 5;
    private float last_spawn = -5;
    public GameObject enemy_car;
    private float[] spawn_x = { -5, 0, 5 };

    private void Start()
    {
        SpawnCars(200f);
        SpawnCars(300f);
        SpawnCars(400f);
        SpawnCars(500f);
        SpawnCars(600f);
        SpawnCars(700f);
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > last_spawn + cooldown)
        {
            SpawnCars(800);
        }
    }

    private void SpawnCars(float dist)
    {
        last_spawn = Time.timeSinceLevelLoad;
        Instantiate(enemy_car, new Vector3(spawn_x[Random.Range(0, spawn_x.Length)], 0, dist), Quaternion.identity, transform);
        if (Random.Range(0f, 1f) < 0.5f)
        {
            Instantiate(enemy_car, new Vector3(spawn_x[Random.Range(0, spawn_x.Length)], 0, dist), Quaternion.identity, transform);
        }
    }
}
