using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class antiLauncher : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField] private TextMeshProUGUI instructionChooseOrbit;
    [SerializeField] private TextMeshProUGUI instructionHowToLaunch;
    [SerializeField] private TextMeshProUGUI demoInProgress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<LineRenderer>().SetPosition(0, initialPosition);
        //GetComponent<LineRenderer>().SetPosition(1, transform.position);

        //if (Globals.rocketStatus == "toLaunch")
        //{
        //    //GetComponent<MeshRenderer>().enabled = true;
        //    float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //    Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        //    transform.position = new Vector3(-pos_move.x, -pos_move.y, pos_move.z);
        //}else
        //{
        //    GetComponent<MeshRenderer>().enabled = false;

        //}

        demoInProgress.enabled = false;
        instructionHowToLaunch.enabled = false;
        instructionChooseOrbit.enabled = false;

        if (Globals.demo)
        {
            demoInProgress.enabled = true;
            demoInProgress.GetComponent<TextMeshProUGUI>().text = Globals.correctOrbit + " לולסמל המגודל רוגיש";
        }
        else
        {
            if (Globals.numberOfLaunches == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
                //instruction.GetComponent<MeshRenderer>().enabled = true;
                if (Globals.correctOrbit == "none")
                {
                    instructionChooseOrbit.enabled = true;
                    instructionHowToLaunch.enabled = false;
                }
                else
                {
                    instructionChooseOrbit.enabled = false;
                    instructionHowToLaunch.enabled = true;
                }

            }
            else
            {
                instructionHowToLaunch.enabled = false;

                if (Globals.rocketStatus == "toLaunch")
                {
                    GetComponent<MeshRenderer>().enabled = true;
                    transform.position = Globals.lastFingerRelease;
                }
                else if (Globals.rocketStatus == "inOrbit")
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }

            }
        }

        



    }

    private void Awake()
    {
        //GetComponent<MeshRenderer>().enabled = true;
        //initialPosition = transform.position;


    }

    private void OnMouseDown()
    {
        //GetComponent<Renderer>().material = holdingFeedback;
    }

    private void OnMouseUp()
    {
        //GetComponent<Renderer>().material = defaultColor;

        //if (Globals.rocketStatus == "toLaunch")
        //{
        //    Vector3 directionToInPo = initialPosition - transform.position;
        //    //Globals.launchForce = directionToInPo * 40;
        //    //Globals.rocketStatus = "LAUNCH";
        //    transform.position = initialPosition;
        //    GetComponent<MeshRenderer>().enabled = false;
        //}

    }

    private void OnMouseDrag()
    {


    }
}
