using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject wheel;
    private Quaternion orig_rot;

    public GameObject car;
    private float lateral_velocity = 10;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        orig_rot = wheel.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                wheel.transform.Rotate(new Vector3(0, -80 * Time.deltaTime, 0));
                car.transform.Translate(new Vector3(-lateral_velocity * Time.deltaTime, 0, 0));
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                wheel.transform.Rotate(new Vector3(0, 80 * Time.deltaTime, 0));
                car.transform.Translate(new Vector3(lateral_velocity * Time.deltaTime, 0, 0));
            }
        }
        else
        {
            wheel.transform.rotation = orig_rot;
        }
    }
}
