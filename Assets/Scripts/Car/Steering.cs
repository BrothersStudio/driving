using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject wheel;
    private Quaternion orig_rot;

    public GameObject car;
    private float lateral_velocity = 20;
    private float x_bounds = 8.8f;

    private float drift = 0.0f;
    private float drift_min = -1.0f;
    private float drift_max = 1.0f;

    private Transform cam;
    private Quaternion target_cam_loc;
    private Quaternion driving_cam_loc;
    private Quaternion texting_cam_loc;
    float animation_t = 0;

    float horn_cooldown = 0.5f;
    float last_horn = 0;

    public void Restart()
    {
        animation_t = 0;
        cam.transform.rotation = driving_cam_loc;
    }

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
            drift = 0.0f;
            animation_t = Mathf.Clamp01(animation_t - Time.deltaTime);
            if (Input.GetAxis("Mouse X") < 0 && car.transform.position.x > -x_bounds)
            {
                wheel.transform.Rotate(new Vector3(0, -80 * Time.deltaTime, 0));
                car.transform.Translate(new Vector3(-lateral_velocity * Time.deltaTime, 0, 0));
            }
            if (Input.GetAxis("Mouse X") > 0 && car.transform.position.x < x_bounds)
            {
                wheel.transform.Rotate(new Vector3(0, 80 * Time.deltaTime, 0));
                car.transform.Translate(new Vector3(lateral_velocity * Time.deltaTime, 0, 0));
            }

            if (Input.GetKey(KeyCode.Space) && Time.timeSinceLevelLoad > last_horn + horn_cooldown)
            {
                last_horn = Time.timeSinceLevelLoad;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            animation_t = Mathf.Clamp01(animation_t + Time.deltaTime);
            wheel.transform.rotation = orig_rot;
            if (drift == 0.0f)
            {
                drift = Random.Range(drift_min, drift_max);
            }
            if (car.transform.position.x > -x_bounds && car.transform.position.x < x_bounds)
            {
                car.transform.Translate(new Vector3(drift * Time.deltaTime, 0, 0));
            }
        }
    }

    private float SmoothStart(float t)
    {
        return t * t * t * t;
    }
}
