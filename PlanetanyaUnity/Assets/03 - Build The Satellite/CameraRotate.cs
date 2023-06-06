﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Camera cam;
    private Vector3 previousPosition;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        initialPosition = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = new Vector3();

            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            cam.transform.Translate(initialPosition); //îé÷åí äîöìîä

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
