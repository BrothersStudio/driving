using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Vector3 orig_pos;

    public void Restart()
    {
        transform.position = orig_pos;
    }

    private void Awake()
    {
        orig_pos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyCar")
        {
            FindObjectOfType<GameController>().GameOver();
        }
    }
}
