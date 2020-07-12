using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    public List<GameObject> trees;
    public GameObject road;
    public GameObject road_line;
    public GameObject street_light;

    public List<GameObject> all_scenery = new List<GameObject>();

    public void Restart()
    {
        foreach (GameObject thing in all_scenery)
        {
            Destroy(thing);
        }
        all_scenery.Clear();

        Start();
    }

    private void Start()
    {
        SpawnRoads();
        SpawnLines();
        SpawnTrees();
        SpawnStreetlights();
    }

    private void SpawnRoads()
    {
        float i;
        for (i = -800; i <= 803; i += 8.8f)
        {
            all_scenery.Add(Instantiate(road, new Vector3(0, 0, i), road.transform.rotation, transform));
        }
        all_scenery.Add(Instantiate(road, new Vector3(0, 0, i), road.transform.rotation, transform));
    }

    private void SpawnLines()
    {
        for (float i = -800; i <= 800; i += 10f)
        {
            all_scenery.Add(Instantiate(road_line, new Vector3(2.8f, 0, i), road.transform.rotation, transform));
            all_scenery.Add(Instantiate(road_line, new Vector3(-2.8f, 0, i), road.transform.rotation, transform));
        }
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
        GameObject new_tree = Instantiate(trees[Random.Range(0, trees.Count)], transform);

        Vector3 position = new Vector3(Random.Range(20f, 800f), new_tree.transform.position.y, z);
        if (left)
        {
            position.x = -position.x;
        }
        new_tree.transform.position = position;

        all_scenery.Add(new_tree);
    }

    private void SpawnStreetlights()
    {
        for (float i = -800; i <= 800; i += 80f)
        {
            all_scenery.Add(Instantiate(street_light, new Vector3(11.3f, 4.4f, i), street_light.transform.rotation, transform));

            GameObject left_light = Instantiate(street_light, new Vector3(-11.3f, 4.4f, i), street_light.transform.rotation, transform);
            left_light.transform.Rotate(new Vector3(0, 0, 180));

            all_scenery.Add(left_light);
        }
    }
}
