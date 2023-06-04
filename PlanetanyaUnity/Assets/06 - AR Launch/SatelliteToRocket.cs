using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteToRocket : MonoBehaviour
{

    public float speed;
    private float X;
    private float Y;
    private float Z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.rocketStatus == "connectSat")
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 moveby = new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
                X = transform.position.x;
                Y = transform.position.y;
                Z = transform.position.z;
                transform.transform.position = new Vector3(X + moveby.x, Y + moveby.y / 2.1f, Z + moveby.y);
            }

            if (transform.position.y > 3 & transform.position.x > -1 & transform.position.x < 1)
            {
                Globals.rocketStatus = "ToLaunch";
            }
        }

    }


}
