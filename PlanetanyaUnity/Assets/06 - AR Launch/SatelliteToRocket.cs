using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class SatelliteToRocket : MonoBehaviour
{

    public float speed;
    private float X;
    private float Y;
    private float Z;

    public float time = 0.5f;
    public float delay = 0;

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
                //Debug.Log(transform.position.y);
                ARcanvasManager.counter = 6;
            }
        }

    }

    private void OnEnable()
    {
        if (!WebXRManager.Instance.isSupportedAR)
        {
            transform.localPosition = new Vector3(-1, 2.2f, 4.1f);
        }
        Vector3 winScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.LeanScale(winScale, time).setDelay(delay).setEaseOutElastic();
    }

}
