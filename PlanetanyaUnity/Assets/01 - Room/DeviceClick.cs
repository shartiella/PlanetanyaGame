using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceClick : MonoBehaviour
{
    [SerializeField] private string thisDevice;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject LookAtTarget;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //לחיצה על אובייקט
    public void OnMouseDown()
    {
        cam.GetComponent<Animator>().enabled = true; //הפעלת אנימציית המצלמה
        cam.GetComponent<CinemachineVirtualCamera>().enabled = true; //הפעלת מעקב המצלמה אחרי האובייקט
    }
    public void OnMouseUp()
    {
        MoveCamera.deviceClicked = thisDevice; //הגדרת האובייקט המסומן
        LookAtTarget.transform.position = transform.position; //הגדרת מיקום המעקב כמיקום האובייקט הלחוץ


        //בחירת לוויין
        Globals.ChosenSatellite.Name = "GPS";//לבטל כדי לבחור לוויין

    }
}
