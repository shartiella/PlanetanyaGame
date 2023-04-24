using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.5f;
    private float X;
    private float Y;
    public float zoomOutMin;
    public float zoomOutMax;
    public float morescroll;

    private Animator camAnimator;
    public static string deviceClicked = "";

    private void Start()
    {
        camAnimator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Debug.Log("i'm being clicked");
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector3 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector3 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMaginitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMaginitude - prevMagnitude;
            zoom(difference * 0.01f);


        }
        if (Input.GetAxis("Mouse ScrollWheel")!=0)
        {
         
            zoom( Input.GetAxis("Mouse ScrollWheel")* morescroll);
        }

        //if (deviceClicked == "Phone")
        //{
        //    Debug.Log("phone clicked");
        //    OnPhoneClick();
        //}
        //else if (deviceClicked == "TV")
        //{
        //    Debug.Log("TV clicked");
        //}
        //else if (deviceClicked == "Computer")
        //{
        //    Debug.Log("Computer clicked");
        //}

    }

     public void zoom (float increment)
    {
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - increment, zoomOutMin, zoomOutMax);
    }




    public void OnDeviceClick()
    {
        camAnimator.SetTrigger(deviceClicked);
        deviceClicked = "";

    }

}
