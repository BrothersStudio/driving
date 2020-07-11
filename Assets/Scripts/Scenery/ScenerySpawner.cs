using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    private float last_spawn = 0;
    private float spawn_cooldown = 0.2f;
    public GameObject tree;

    private void Update()
    {
        if (Time.timeSinceLevelLoad > last_spawn + spawn_cooldown)
        {
            last_spawn = Time.timeSinceLevelLoad;

            if (Random.Range(0f, 1f) < 0.50f)
            {
                float roll = Random.Range(0f, 1f);
                if (roll < 0.4f)
                {
                    // Left tree
                    Instantiate(tree, transform);
                }
                else if (roll < 0.8f)
                {
                    // Right tree
                    GameObject new_tree = Instantiate(tree, transform);
                    new_tree.transform.position = new Vector3(-new_tree.transform.position.x, new_tree.transform.position.y, new_tree.transform.position.z);
                }
                else
                {
                    // Both
                    Instantiate(tree, transform);
                    GameObject new_tree = Instantiate(tree, transform);
                    new_tree.transform.position = new Vector3(-new_tree.transform.position.x, new_tree.transform.position.y, new_tree.transform.position.z);
                }
            }
        }
    }
}
