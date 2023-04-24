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

    public void OnMouseDown()
    {
        cam.GetComponent<Animator>().enabled = true;
        cam.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    public void OnMouseUp()
    {
        MoveCamera.deviceClicked = thisDevice;
        Globals.ChosenSatelliteName = "GPS";
        LookAtTarget.transform.position = transform.position;
    }
}
