using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    private Camera cam;

    //Orient the camera after all movement is completed this frame to avoid jittering
    private void Awake()
    {
        cam = Camera.main;   
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up);
    }
}