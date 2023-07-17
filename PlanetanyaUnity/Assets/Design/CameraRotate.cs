using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Camera cam;
    private Vector3 previousPosition;
    private Vector3 initialPosition;
    public Transform TargetForCam;
    public bool moveCamWithDrag = false;
    public bool moveCamByDeviceRotation = false;
    public bool rotateAroundTheTarget = false;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
        initialPosition = cam.transform.position;
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (moveCamWithDrag)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

                if (rotateAroundTheTarget)
                {
                    cam.transform.position = TargetForCam.position; //new Vector3();

                    cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                    cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);

                    cam.transform.Translate(initialPosition);
                }
                else
                {
                    cam.transform.Rotate(new Vector3(1, 0, 0), -direction.y * 180);
                    cam.transform.Rotate(new Vector3(0, 1, 0), direction.x * 180, Space.World);
                }

                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveCamByDeviceRotation)
        {
            if (Input.gyro.attitude.x != 0 && Input.gyro.attitude.y != 0 && Input.gyro.attitude.z != 0 && Input.gyro.attitude.w != 1)
            {
                if (rotateAroundTheTarget)
                {
                    transform.position = TargetForCam.position; //new Vector3();

                    cam.transform.Rotate(new Vector3(1, 0, 0), -Input.gyro.rotationRate.x);
                    cam.transform.Rotate(new Vector3(0, 1, 0), -Input.gyro.rotationRate.y, Space.World);

                    cam.transform.Translate(initialPosition);
                }
                else
                {
                    cam.transform.Rotate(new Vector3(1, 0, 0), -Input.gyro.rotationRate.x);
                    cam.transform.Rotate(new Vector3(0, 1, 0), -Input.gyro.rotationRate.y, Space.World);
                }
            }
        }
    }
}
