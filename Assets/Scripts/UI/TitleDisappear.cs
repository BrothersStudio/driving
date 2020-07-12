using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDisappear : MonoBehaviour
{
    private float pressed = 0;
    private float buffer = 1;

    public void Restart()
    {
        pressed = Time.timeSinceLevelLoad;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > buffer + pressed && Input.anyKey)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
