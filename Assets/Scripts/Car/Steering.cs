using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject wheel;
    private Quaternion orig_rot;

    public GameObject car;
    private Direction direction_track = Direction.None;
    private float laternal_velocity_lerp = 0;
    private float lateral_velocity = 20;
    private float x_bounds = 8f;

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
        direction_track = 0;
        laternal_velocity_lerp = 0;

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
        laternal_velocity_lerp = Mathf.Clamp01(laternal_velocity_lerp + (Time.deltaTime * 2));

        if (Input.GetMouseButton(0))
        {
            drift = 0.0f;
            animation_t = Mathf.Clamp01(animation_t - Time.deltaTime);

            if (Input.GetAxis("Mouse X") < 0 && car.transform.position.x > -x_bounds)
            {
                if (direction_track != Direction.Left)
                {
                    laternal_velocity_lerp = 0;
                }

                wheel.transform.Rotate(new Vector3(0, -80 * Time.deltaTime, 0));
                car.transform.Translate(new Vector3(-Mathf.Lerp(0, lateral_velocity, laternal_velocity_lerp) * Time.deltaTime, 0, 0));

                direction_track = Direction.Left;
            }
            else if (Input.GetAxis("Mouse X") > 0 && car.transform.position.x < x_bounds)
            {
                if (direction_track != Direction.Right)
                {
                    laternal_velocity_lerp = 0;
                }

                wheel.transform.Rotate(new Vector3(0, 80 * Time.deltaTime, 0));
                car.transform.Translate(new Vector3(Mathf.Lerp(0, lateral_velocity, laternal_velocity_lerp) * Time.deltaTime, 0, 0));

                direction_track = Direction.Right;
            }
            else
            {
                wheel.transform.rotation = orig_rot;
                direction_track = Direction.None;
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

        // Force in-bounds
        if (car.transform.position.x > x_bounds)
        {
            Vector3 pos = car.transform.position;
            pos.x = x_bounds;
            car.transform.position = pos;
        }
        else if (car.transform.position.x < -x_bounds)
        {
            Vector3 pos = car.transform.position;
            pos.x = -x_bounds;
            car.transform.position = pos;
        }
    }

    private float SmoothStart(float t)
    {
        return t * t * t * t;
    }

    private float SmoothStep(float t)
    {
        return 3 * t * t - 2 * t * t * t;
    }

    private enum Direction
    {
        None,
        Left,
        Right
    }
}
