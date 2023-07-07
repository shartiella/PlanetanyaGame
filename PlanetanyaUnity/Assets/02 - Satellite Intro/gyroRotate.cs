using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroRotate : MonoBehaviour
{
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        rot = Vector3.zero;
        Input.gyro.enabled= true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.rotation = Input.gyro.attitude;
        rot.y = -Input.gyro.rotationRateUnbiased.y;
        transform.Rotate(rot);
    }
}
