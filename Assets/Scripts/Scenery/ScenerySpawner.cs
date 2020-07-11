using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    public GameObject tree;
    public GameObject road;

    private void Start()
    {
        SpawnRoads();
        SpawnTrees();
    }

    private void SpawnRoads()
    {
        float i;
        for (i = -800; i <= 803; i += 8.8f)
        {
            Instantiate(road, new Vector3(0, 0, i), road.transform.rotation, transform);
        }
        Instantiate(road, new Vector3(0, 0, i), road.transform.rotation, transform);
    }

    private void SpawnTrees()
    {
        for (int i = -800; i < 800; i += 1)
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

        Vector3 position = new Vector3(Random.Range(10f, 400f), new_tree.transform.position.y, z);
        if (left)
        {
            position.x = -position.x;
        }
        new_tree.transform.position = position;
    }
}
