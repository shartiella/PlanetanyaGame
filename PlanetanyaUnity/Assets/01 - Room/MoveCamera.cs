using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    private float X;
    private float Y;
    public float zoomOutMin;
    public float zoomOutMax;
    public float morescroll;

    public static string deviceClicked = "";
    public static bool finalPart = false;

    [SerializeField] private GameObject window1;
    [SerializeField] private GameObject window2;
    [SerializeField] private GameObject window3;

    [SerializeField] private GameObject cineMachine;

    [SerializeField] private GameObject pointerPrefab;
    private GameObject pointer1;
    private GameObject pointer2;

    private void Start()
    {
        window1.SetActive(true);
    }

    void Update()
    {

        //if (Input.touchCount > 0)
        //{
        //    Debug.Log(Input.touchCount);

        //    Touch touchZero = Input.GetTouch(0);
        //    Touch touchOne = Input.GetTouch(1);

        //    Vector3 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        //    Vector3 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
        //    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        //    float currentMaginitude = (touchZero.position - touchOne.position).magnitude;
        //    float difference = currentMaginitude - prevMagnitude;

        //    pointer1 = Instantiate(pointerPrefab);
        //    pointer1.transform.position = touchZero.position;

        //    pointer2 = Instantiate(pointerPrefab);
        //    pointer2.transform.position = touchOne.position;

        //    zoom(difference * 0.01f);
        //}
        //else
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoom(Input.GetAxis("Mouse ScrollWheel") * morescroll);
        }
        else if (Input.GetMouseButton(0))
        {
            float FOVfactor = Camera.main.fieldOfView/60;
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed* FOVfactor, -Input.GetAxis("Mouse X") * speed* FOVfactor, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }



        if (deviceClicked != "")
        {
            //OnDeviceClick();
            window1.SetActive(false);
            window2.SetActive(true);

        }
        if (finalPart == true)
        {
            window2.SetActive(false);
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
        window3.SetActive(true);
    }
}
