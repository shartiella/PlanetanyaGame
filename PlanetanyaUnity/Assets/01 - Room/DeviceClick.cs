using Cinemachine;
using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private Material screenOn;
    [SerializeField] private GameObject screen;

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

            //Debug.Log(screen.GetComponent<MeshRenderer>().materials[1].name);

            //screen.GetComponent<MeshRenderer>().material = screenOn;

            MeshRenderer screenren = screen.GetComponent<MeshRenderer>();
            List<Material> renmats = screenren.materials.ToList();

            if (renmats.Count > 1)
            {
                foreach(Material mat in renmats)
                {
                    if (mat.name.StartsWith("phoneScreenOff"))
                    {
                        int indexofmat = renmats.IndexOf(mat);
                        var mats = screen.GetComponent<MeshRenderer>().materials;
                        mats[indexofmat] = screenOn;
                        screen.GetComponent<MeshRenderer>().materials = mats;
                    }
                }
            }
            else
            {
                screen.GetComponent<MeshRenderer>().material = screenOn;
            }

            Vector3 myPos = transform.position;

            if (RoomCamera.deviceClicked == thisDevice)
            {
                //LookAtTarget.transform.position = transform.position; //הגדרת מיקום המעקב כמיקום האובייקט הלחוץ
                                                                      //LookAtTarget.transform.LeanMove(myPos, 1);
                Vector3 positionTo = transform.position;
                LookAtTarget.transform.LeanMove(positionTo, 0.5f).setEaseInOutQuad();

                Cam.transform.LeanMove(CamPositionAfterClick, 2).setEaseInOutQuad().setOnComplete(stopMovingCamera);
            }
        }
    }

}
