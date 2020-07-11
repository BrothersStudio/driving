using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    private float last_spawn = 0;
    private float spawn_cooldown = 0.2f;
    public GameObject tree;

    public GameObject road;

    private void Start()
    {
        SpawnRoads();
        SpawnTrees();
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > last_spawn + spawn_cooldown)
        {
            last_spawn = Time.timeSinceLevelLoad;


        }
    }

    private void SpawnRoads()
    {
        for (float i = -800; i <= 803; i += 8.8f)
        {
            Debug.Log(i);
            Vector3 vector3 = new Vector3(0, 0, i);
            Instantiate(road, vector3, road.transform.rotation, transform);
        }
    }

    private void SpawnTrees()
    {
        for (int i = -800; i < 800; i += 2)
        {
            if (Random.Range(0f, 1f) < 0.50f)
            {
                float roll = Random.Range(0f, 1f);
                if (roll < 0.4f)
                {
                    // Left tree
                    SpawnTree(true, i);
                }
                else if (roll < 0.8f)
                {
                    // Right tree
                    SpawnTree(false, i);
                }
                else
                {
                    // Both
                    SpawnTree(true, i);
                    SpawnTree(false, i);
                }
            }
        }

    }

    private void SpawnTree(bool left, float z)
    { 
        GameObject new_tree = Instantiate(tree, transform);

        Vector3 position = new Vector3(Random.Range(10f, 200f), new_tree.transform.position.y, z);
        if (left)
        {
            position.x = -position.x;
        }
        new_tree.transform.position = position;
    }
}
