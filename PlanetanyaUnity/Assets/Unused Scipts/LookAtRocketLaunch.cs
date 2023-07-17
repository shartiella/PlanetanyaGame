using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtRocketLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.5f;
    private float X;
    private float Y;

   void Update()
    {
        if (Globals.rocketStatus != "connectSat" && Globals.rocketStatus != "satConnected") {
            if (Input.GetMouseButton(0))
            {
                float FOVfactor = Camera.main.fieldOfView / 60;
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed * FOVfactor, -Input.GetAxis("Mouse X") * speed * FOVfactor, 0));
                X = transform.rotation.eulerAngles.x;
                Y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(X, Y, 0);
                //Debug.Log(transform.rotation.eulerAngles);
            }

            //Debug.Log(Globals.rocketStatus);
            //if((transform.rotation.eulerAngles.y>178 || transform.rotation.eulerAngles.y<182)&& (transform.rotation.eulerAngles.x > 350 || transform.rotation.eulerAngles.x < 0))
            //{
            //    Debug.Log("in view");
            //    //Globals.rocketStatus = "LookAtRocket";
            //}
        }

        //if (Globals.rocketStatus == "launching")
        //{

        //}
    }
}
