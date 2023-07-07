using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public float speed = 1;
    private float X;
    private float Y;
    public float zoomOutMin;
    public float zoomOutMax;
    public float morescroll;

    public static string deviceClicked = "";
    public static bool finalPart = false;

    //[SerializeField] private CinemachineVirtualCamera follower;

    //[SerializeField] private GameObject cineMachine;

    //[SerializeField] private GameObject pointerPrefab;

    //[SerializeField] private TextMeshProUGUI fingers1;
    //[SerializeField] private TextMeshProUGUI fingers2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deviceClicked == "")
        {
            if (Input.touchCount == 2)
            {
                //Debug.Log(Input.touchCount);
                //            fingers1.text = Input.touchCount.ToString();

                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector3 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector3 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMaginitude = (touchZero.position - touchOne.position).magnitude;
                float difference = currentMaginitude - prevMagnitude;

                //fingers2.text = difference.ToString();

                zoom(difference * -0.05f);
            }
            else if (Input.GetMouseButton(0))
            {
                float FOVfactor = Camera.main.fieldOfView / 60;
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed * FOVfactor, -Input.GetAxis("Mouse X") * speed * FOVfactor, 0));
                X = transform.rotation.eulerAngles.x;
                Y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(X, Y, 0);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                zoom(Input.GetAxis("Mouse ScrollWheel") * morescroll);
            }
        }


        //if (RoomCamera.deviceClicked != "")
        //{
        //    follower.enabled = true;
        //}
    }

    public void zoom(float increment)
    {
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - increment, zoomOutMin, zoomOutMax);
    }
}
