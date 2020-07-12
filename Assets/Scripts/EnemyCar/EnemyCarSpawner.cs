 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarSpawner : MonoBehaviour
{
    private float cooldown = 3.5f;
    private float last_spawn = -5;
    private float time_difficulty_increment = 30f;
    private float difficulty_increase = 0.3f;

    private float difficulty_timer = 0.0f;

    public GameObject enemy_car;
    private float[] spawn_x = { -6.5f, 0, 6.5f };
    private List<GameObject> all_cars = new List<GameObject>();

    private float speed_increase = 0;

    public void Restart()
    {
        foreach (GameObject car in all_cars)
        {
            Destroy(car);
        }
        all_cars.Clear();

        speed_increase = 0;
        last_spawn = -5;

        Start();
    }

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
        speed_increase += Time.deltaTime;
        difficulty_timer += Time.deltaTime;

        if(difficulty_timer >= time_difficulty_increment)
        {
            cooldown -= difficulty_increase;
            difficulty_timer = 0.0f;
        }

        if (Time.timeSinceLevelLoad > last_spawn + cooldown)
        {
            SpawnCars(800);
        }
    }

    private void SpawnCars(float dist)
    {
        last_spawn = Time.timeSinceLevelLoad;

        GameObject new_car = Instantiate(enemy_car, new Vector3(spawn_x[Random.Range(0, spawn_x.Length)], 0, dist), Quaternion.identity, transform);
        new_car.GetComponent<EnemyCar>().SetSpeedIncrease(speed_increase);
        all_cars.Add(new_car);
        if (Random.Range(0f, 1f) < 0.5f)
        {
            GameObject second_car = Instantiate(enemy_car, new Vector3(spawn_x[Random.Range(0, spawn_x.Length)], 0, dist), Quaternion.identity, transform);
            second_car.GetComponent<EnemyCar>().SetSpeedIncrease(speed_increase);
            all_cars.Add(second_car);
        }
    }
}
