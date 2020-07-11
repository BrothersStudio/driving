using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private void Awake()
    {
        Invoke("StartTurn", 5);
    }

    private void StartTurn()
    {
        StartCoroutine(Turn());
    }

    private IEnumerator Turn()
    {
        Quaternion orig_rotation = transform.rotation;
        Quaternion new_rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(orig_rotation, new_rotation, i);
            yield return null;
        }
    }
}
