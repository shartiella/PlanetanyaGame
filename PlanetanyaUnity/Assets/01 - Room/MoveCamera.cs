using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class MoveCamera : MonoBehaviour
{
    public float speed = 1;
    private float X;
    private float Y;
    public float zoomOutMin;
    public float zoomOutMax;
    public float morescroll;

    public static string deviceClicked = "";
    public static bool finalPart = false;

    //[SerializeField] private GameObject Story1;
    //[SerializeField] private GameObject Story2;

    //[SerializeField] private GameObject Inst1;
    //[SerializeField] private GameObject window2;
    //[SerializeField] private GameObject window3;

    [SerializeField] private GameObject cineMachine;

    [SerializeField] private GameObject pointerPrefab;


    [SerializeField] private TextMeshProUGUI fingers1;
    [SerializeField] private TextMeshProUGUI fingers2;

    private void Start()
    {
        //Inst1.SetActive(true);
    }

    void Update()
    {
        //var fingerCount = 0;
        //foreach (Touch touch in Input.touches)
        //{
        //    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
        //    {
        //        fingerCount++;
        //    }
        //}
        //if (fingerCount > 0)
        //{
        //    //print("User has " + fingerCount + " finger(s) touching the screen");
        //    fingers1.text=fingerCount.ToString();
        //}

        if (Input.touchCount == 2)
        {
            //Debug.Log(Input.touchCount);
            fingers1.text = Input.touchCount.ToString();

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector3 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector3 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMaginitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMaginitude - prevMagnitude;

            fingers2.text = difference.ToString();

            zoom(difference * -0.01f);
        }
        else if (Input.touchCount == 1 || Input.GetMouseButton(0))
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




        if (deviceClicked != "")
        {
            //OnDeviceClick();
            //Inst1.SetActive(false);
            //window2.SetActive(true);

        }
        if (finalPart == true)
        {
            //window2.SetActive(false);
        }
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

    //public void OnDeviceClick()
    //{

    //    cineMachine.SetActive(true);
    //    deviceClicked = "";

    //}

    public void towindow3()
    {
        finalPart = true;
        //window2.SetActive(false);
       // window3.SetActive(true);
    }
}
