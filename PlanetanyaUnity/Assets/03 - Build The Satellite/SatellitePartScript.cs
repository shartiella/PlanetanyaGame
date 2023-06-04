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
    private Vector3 initialobjectPosition; //המיקום ההתחלתי של האובייקט
    private float dragTimer;
    private bool isCorrect = false;
    [SerializeField] private AllObjects _allObjects;
    [SerializeField] private Globals _globals;
    private SatPart thisSatPart;
    private bool isCurrentlyConnected = false;

    // Start is called before the first frame update
    void Start()
    {
        initialobjectPosition = transform.position; //קביעת המיקום ההתחלתי של האובייקט



    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.ChosenSatellite != null && thisSatPart == null)
        {
            IdentifySelf();
        }




        if (BodyTrigger.currentTouchingPart == transform.gameObject)
        {
            isCurrentlyConnected = true;
            //Debug.Log("CURRENTLY TOUCHING BODY");
        }
        else
        {
            isCurrentlyConnected = false;
        }
    }

    private void OnMouseDown()
    {


        //Debug.Log(thisSatPart.Name + isCorrect);



    }

    private void OnMouseDrag()
    {
        dragTimer += Time.deltaTime;
        if (AllObjects.BuildingState == "building")
        {
            if (dragTimer >= 0.2f)//גרירה
            {
                Debug.Log(gameObject.transform.position);
                float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

                transform.position = Globals.currentMousePosition; //גרירה - האובייקט עוקב אחרי העכבר
            }

        }



    }

    private void OnMouseUp()
    {
        if (AllObjects.BuildingState == "building")
        {
            if (dragTimer >= 0.2f)//אם זו הייתה גרירה
            {
                hideInfo();
            }
            else //אם זו הייתה לחיצה
            {
                onMouseClick();
            }
            dragTimer = 0; //איפוס טיימר הגרירה

            //בדיקה האם נוגע ואז האם נכון
            if (isCurrentlyConnected)
            {
                Debug.Log("I'm In");

                if (isCorrect)
                {
                    Debug.Log("I'm correct");
                    BuildIU.numberOfCorrectObjectsConnected++;
                    Debug.Log("correct "+BuildIU.numberOfCorrectObjectsConnected);
                }
                else
                {
                    Debug.Log("I'm wrong");
                    BuildIU.numberOfWrongObjectsConnected++;
                    Debug.Log("wrong " + BuildIU.numberOfWrongObjectsConnected);

                }
            }
            else
            {
                if (isCorrect)
                {
                    Debug.Log("I'm correct");
                    BuildIU.numberOfCorrectObjectsConnected--;
                    Debug.Log("correct " + BuildIU.numberOfCorrectObjectsConnected);

                }
                else
                {
                    Debug.Log("I'm wrong");
                    BuildIU.numberOfWrongObjectsConnected--;
                    Debug.Log("wrong " + BuildIU.numberOfWrongObjectsConnected);

                }
                Debug.Log("I'm out");
                transform.position = initialobjectPosition;
            }
        }

    }

    private void onMouseClick()
    {
        if (AllObjects.BuildingState == "building")
        {
            if (AllObjects.currentSatPart == thisSatPart)
            {
                if (BuildIU.showInfoPanel)
                {
                    hideInfo();
                }
                else
                {
                    showInfo();
                }
            }
            else
            {
                AllObjects.currentSatPart = thisSatPart;
                showInfo();
            }
        }
    }

    private void IdentifySelf()
    {
        //Debug.Log("IDENTIFY " + transform.name);

        //איזה חלק אני והאם אני נכון
        foreach (SatPart sp in _allObjects.satParts)
        {
            if (sp.Name == transform.name)
            {
                //כשהוא מוצא את עצמו לפי שם האובייקט, הוא מגדיר את עצמו כאובייקט ה"נוכחי" כדי לזהות את עצמו
                thisSatPart = sp;


                //האם אני שייך ללוויין שנבחר
                foreach (string sat in sp.relatedSatellites)
                {
                    if (Globals.ChosenSatellite.Name == sat)
                    {
                        isCorrect = true;
                        BuildIU.overallNumberOfCorrectParts++;
                        Debug.Log("overall " + BuildIU.overallNumberOfCorrectParts);
                    }
                    Debug.Log("I'm " + transform.name + " and I'm " + isCorrect);
                    colorMe();


                }
            }
        }
    }

    private void colorMe()
    {
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
    }

    private void showInfo()
    {
        BuildIU.showInfoPanel = true;
        Debug.Log("showInfoPanel true");
    }

    private void hideInfo()
    {
        BuildIU.showInfoPanel = false;
        Debug.Log("showInfoPanel false");

    }

    private void giveFeedback()
    {
        if (AllObjects.BuildingState == "feedback")
        {
            colorMe();
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
}
