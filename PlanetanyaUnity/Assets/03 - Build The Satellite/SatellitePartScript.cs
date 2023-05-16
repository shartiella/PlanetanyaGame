using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SatellitePartScript : MonoBehaviour
{
    //הערה חדשה בעברית
    private Vector3 initialobjectPosition; //המיקום ההתחלתי של האובייקט
    private float dragTimer;
    private bool isCorrect = false;
    [SerializeField] private AllObjects _allObjects;
    [SerializeField] private Globals _globals;
    private SatPart thisSatPart;


    // Start is called before the first frame update
    void Start()
    {
        initialobjectPosition = transform.position; //קביעת המיקום ההתחלתי של האובייקט

        //איזה חלק אני
        //עובר על רשימת החלקים המלאה ומכניס למשתנה החלק הזה את החלק שהוא
        foreach (SatPart sp in _allObjects.satParts)
        {
            //Debug.Log("checking " + sp.Name + " in sat Parts");
            if (sp.Name == transform.name)
            {
                //Debug.Log("found myself: "+transform.name);
                thisSatPart = sp;


                //האם אני שייך ללוויין המדובר
                foreach (string sat in sp.relatedSatellites)
                {
                    //Debug.Log("looking up " + sat + " in sats of "+ sp.Name);
                    if (Globals.ChosenSatelliteName == sat)
                    {
                        isCorrect = true;
                    }
                    //Debug.Log("I'm " + transform.name + " and I'm " + isCorrect);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        if (AllObjects.BuildingState == "feedback")
        {
            //if (isCorrect)
            //{
            //    GetComponent<MeshRenderer>().material.color = Color.green;
            //}
            //else
            //{
            //GetComponent<MeshRenderer>().material.color = Color.red;
            //}

            //GetComponentInChildren<MeshRenderer>().material.color = Color.red;

            List<MeshRenderer> renderers = GetComponentsInChildren<MeshRenderer>().ToList();
            foreach (MeshRenderer ren in renderers)
            {
                if (isCorrect)
                {
                    ren.material.color = Color.green;
                }
                else
                {
                    ren.material.color = Color.red;
                }
            }

            //GetComponent<Renderer>().material = defaultColor;
        }
        else
        {
            List<MeshRenderer> renderers = GetComponentsInChildren<MeshRenderer>().ToList();
            foreach (MeshRenderer ren in renderers)
            {
                ren.material.color = Color.white;
            }
        }
    }

    private void OnMouseDown()
    {
        //GetComponent<Renderer>().material = holdingFeedback; //צביעת העיגול בצהוב
        if (AllObjects.BuildingState== "building")
        {
            dragTimer = 0;

            AllObjects.currentSatPart = thisSatPart;

            //קריאה להפעלת חלונית המידע
            if (BuildIU.showInfoPanel)
            {
                BuildIU.showInfoPanel = false;
            }
            else
            {
                BuildIU.showInfoPanel = true;

            }
        }

    }

    private void OnMouseDrag()
    {
        if (AllObjects.BuildingState == "building")
        {
        transform.position = Globals.currentMousePosition; //גרירה - העיגול עוקב אחרי העכבר

        dragTimer += Time.deltaTime;

        if (dragTimer >= 0.2f)
        {
            BuildIU.showInfoPanel = false;
        }
        }

    }

    private void OnMouseUp()
    {
        //GetComponent<Renderer>().material = defaultColor;

        if (AllObjects.BuildingState == "building")
        {
        //בדיקה האם נוגע ואז האם נכון
        if (BuildIU.isTouchingSatBody)
        {
            Debug.Log("I'm In");

            if (isCorrect)
            {
                Debug.Log("I'm correct");
            }
            else
            {
                Debug.Log("I'm wrong");
            }
        }
        else
        {
            Debug.Log("I'm out");
            transform.position = initialobjectPosition;
        }
        }

    }

}
