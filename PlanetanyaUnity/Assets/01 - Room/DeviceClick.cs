using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceClick : MonoBehaviour
{
    [SerializeField] private string thisDevice;
    //[SerializeField] private GameObject regularCam;
    //[SerializeField] private GameObject CineCam;
    [SerializeField] private GameObject LookAtTarget;
    [SerializeField] private Globals _globals;


    [SerializeField] private GameObject Cam;
    [SerializeField] private Vector3 CamPositionAfterClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void stopMovingCamera()
    {
        Cam.GetComponent<CinemachineVirtualCamera>().enabled = false;
        Cam.GetComponent<CameraRotate>().enabled = true;
    }

    //לחיצה על אובייקט
    public void OnMouseDown()
    {
        //Vector3 camPos = regularCam.transform.position;
        //Quaternion camRot = regularCam.transform.rotation;
        //CineCam.transform.position = camPos;
        //CineCam.transform.rotation = camRot;
        //CineCam.GetComponent<Animator>().enabled = true; //הפעלת אנימציית המצלמה
        //CineCam.GetComponent<CinemachineVirtualCamera>().enabled = true; //הפעלת מעקב המצלמה אחרי האובייקט
    }
    public void OnMouseUp()
    {
        if (CanvasManager.counter == 5)
        {
            CanvasManager.counter++;

            RoomCamera.deviceClicked = thisDevice; //הגדרת האובייקט המסומן
            BlinkColor.glowOn = false;
            Cam.GetComponent<CinemachineVirtualCamera>().enabled = true;
            Cam.GetComponent<CameraRotate>().enabled = false;

            foreach (Satellite sat in _globals.SatellitesList)
            {
                if (sat.Object == thisDevice)
                {
                    Globals.ChosenSatellite = sat;
                }
            }

            Vector3 myPos = transform.position;

            if (RoomCamera.deviceClicked == thisDevice)
            {
                LookAtTarget.transform.position = transform.position; //הגדרת מיקום המעקב כמיקום האובייקט הלחוץ
                                                                      //LookAtTarget.transform.LeanMove(myPos, 1);
                Cam.transform.LeanMove(CamPositionAfterClick, 2).setEaseInOutQuad().setOnComplete(stopMovingCamera);
            }
        }
    }

}
