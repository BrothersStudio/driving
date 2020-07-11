using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject wheel;
    private Quaternion orig_rot;

    public GameObject car;
    private float lateral_velocity = 20;

    private Transform cam;
    private Quaternion target_cam_loc;
    private Quaternion driving_cam_loc;
    private Quaternion texting_cam_loc;
    float animation_t = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        orig_rot = wheel.transform.rotation;

        // Camera positions
        cam = Camera.main.transform;
        driving_cam_loc = cam.transform.rotation;
        texting_cam_loc = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    private void Update()
    {
        cam.rotation = Quaternion.Lerp(driving_cam_loc, texting_cam_loc, SmoothStart(animation_t));

        if (Input.GetMouseButton(0))
        {
            animation_t = Mathf.Clamp01(animation_t - Time.deltaTime);
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
            animation_t = Mathf.Clamp01(animation_t + Time.deltaTime);
            wheel.transform.rotation = orig_rot;
        }
    }

    private float SmoothStart(float t)
    {
        return t * t * t * t;
    }
}
