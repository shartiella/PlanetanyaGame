using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SatellitePartScript : MonoBehaviour
{
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
                Debug.Log("found myself: "+transform.name);
                thisSatPart = sp;


                //האם אני שייך ללוויין המדובר
                foreach (string sat in sp.relatedSatellites)
                {
                    Debug.Log("looking up " + sat + " in sats of "+ sp.Name);
                    if (_globals.ChosenSatelliteName == sat)
                    {
                        isCorrect = true;
                    }
                    Debug.Log("I'm " + transform.name + " and I'm " + isCorrect);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));


    }

    private void OnMouseDown()
    {
        //GetComponent<Renderer>().material = holdingFeedback; //צביעת העיגול בצהוב

        dragTimer = 0;

        AllObjects.currentSatPart = thisSatPart;

        //קריאה להפעלת חלונית המידע
        if (AllObjects.showInfoPanel)
        {
            AllObjects.showInfoPanel = false;
        }
        else
        {
            AllObjects.showInfoPanel = true;
            
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Globals.currentMousePosition; //גרירה - העיגול עוקב אחרי העכבר

        dragTimer += Time.deltaTime;

        if (dragTimer >= 0.2f)
        {
            AllObjects.showInfoPanel = false;
        }
    }

    private void OnMouseUp()
    {
        //GetComponent<Renderer>().material = defaultColor;

        //בדיקה האם נוגע ואז האם נכון
        if (AllObjects.isTouchingSatBody)
        {
            Debug.Log("I'm In");

            if (isCorrect)
            {
                Debug.Log("I'm correct");
            }
            else
            {
                Debug.Log("I'm wrong");
                transform.position = initialobjectPosition;
            }
        }
        else
        {
            Debug.Log("I'm out");
            transform.position = initialobjectPosition;
        }
    }

}
