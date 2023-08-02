
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class antiLauncher : MonoBehaviour
{
    private Vector3 initialPosition;
    //[SerializeField] private TextMeshProUGUI instructionChooseOrbit;
    //[SerializeField] private TextMeshProUGUI instructionHowToLaunch;
    //[SerializeField] private TextMeshProUGUI demoInProgress;

    private Vector3 fullScale;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.LeanScale(fullScale, 0.5f).setDelay(2).setEaseOutElastic();
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

        //demoInProgress.enabled = false;
        //instructionHowToLaunch.enabled = false;
        //instructionChooseOrbit.enabled = false;

        if (Globals.demo)
        {
            //demoInProgress.enabled = true;
            //demoInProgress.GetComponent<TextMeshProUGUI>().text = Globals.ChosenSatellite.Orbit + " לולסמל המגודל רוגיש";
        }
        else
        {
            if (Rocket.launchCounter == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
                //instruction.GetComponent<MeshRenderer>().enabled = true;
                if (Globals.ChosenSatellite.Orbit == "none")
                {
                    //instructionChooseOrbit.enabled = true;
                    //instructionHowToLaunch.enabled = false;
                }
                else
                {
                    //instructionChooseOrbit.enabled = false;
                    //instructionHowToLaunch.enabled = true;
                }

            }
            else
            {
                //instructionHowToLaunch.enabled = false;

                if (Globals.rocketStatus == "toLaunch")
                {
                    GetComponent<MeshRenderer>().enabled = true;
                    transform.position = OrbitManager.lastFingerRelease;
                }
                //else if (Globals.rocketStatus == "inOrbit")
                //{
                //    GetComponent<MeshRenderer>().enabled = false;
                //}

            }
        }
    }

    private void Awake()
    {
        //GetComponent<MeshRenderer>().enabled = true;
        //initialPosition = transform.position;

        fullScale = transform.localScale;

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
