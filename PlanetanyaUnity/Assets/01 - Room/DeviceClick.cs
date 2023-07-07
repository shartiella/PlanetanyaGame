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
    }

    //����� �� �������
    public void OnMouseDown()
    {
        //Vector3 camPos = regularCam.transform.position;
        //Quaternion camRot = regularCam.transform.rotation;
        //CineCam.transform.position = camPos;
        //CineCam.transform.rotation = camRot;
        //CineCam.GetComponent<Animator>().enabled = true; //����� �������� ������
        //CineCam.GetComponent<CinemachineVirtualCamera>().enabled = true; //����� ���� ������ ���� ��������
    }
    public void OnMouseUp()
    {
        CanvasManager.counter++;

        RoomCamera.deviceClicked = thisDevice; //����� �������� ������
        Cam.GetComponent<CinemachineVirtualCamera>().enabled = true;

        foreach (Satellite sat in _globals.SatellitesList)
        {
            if (sat.Object == thisDevice)
            {
                Globals.ChosenSatellite = sat;
            }
        }

        StoryWinAnim.exitAnimationTrigger = true;
        //����� ������
        //Globals.ChosenSatellite.Name = "GPS";//���� ��� ����� ������

        Vector3 myPos = transform.position;


        if (RoomCamera.deviceClicked == thisDevice)
        {
            LookAtTarget.transform.position = transform.position ; //����� ����� ����� ������ �������� �����
                                                                   //LookAtTarget.transform.LeanMove(myPos, 1);
            Cam.transform.LeanMove(CamPositionAfterClick, 2).setEaseInOutQuad().setOnComplete(stopMovingCamera);


        }
    }

}
