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
        //פה תהיה הצגה של חלקי הלוויין שרלוונטיים ללוויין שנבחר
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.rocketStatus == "connectSat"&&ARcanvasManager.counter==5)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 moveby = new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
                X = transform.position.x;
                Y = transform.position.y;
                Z = transform.position.z;
                transform.transform.position = new Vector3(X + moveby.x, Y + moveby.y * 1.3f, Z + moveby.y);
            }

            if (transform.position.y > 9.6f & transform.position.x > -1 & transform.position.x < 1)
            {
                Debug.Log(transform.position.y);
                Globals.rocketStatus = "ToLaunch";
                ARcanvasManager.counter = 6;
            }
        }

    }


}
