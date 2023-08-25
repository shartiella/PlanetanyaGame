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
    [SerializeField] private AllObjects _allObjects;
    [SerializeField] private Globals _globals;
    private SatPart thisSatPart;
    [SerializeField] private GameObject StoryWindow;
    [SerializeField] private GameObject Storyicon;
    float distance_to_screen;
    [SerializeField] private float zAtDesk = 0;

    private void Start()
    {
        zAtDesk = 0;
    }

    // Start is called before the first frame update
    void Awake()
    {
        initialobjectPosition = transform.position; //קביעת המיקום ההתחלתי של האובייקט
    }

    private void OnEnable()
    {
        if (thisSatPart != null)
        {
            thisSatPart.isConnected = false;
        }
        transform.position = initialobjectPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        IdentifySelf();
        if (!thisSatPart.isDragged)
        {
            if (AllObjects.aPartIsBeingDragged)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().detectCollisions = false;
            }
            else
            {
                //GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().detectCollisions = true;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (BuildIU.overallNumberOfCorrectParts == BuildIU.numberOfCorrectObjectsConnected && BuildIU.numberOfWrongObjectsConnected > 0)
        {
            if (StoryWindow.activeSelf == false && !StoryWinAnim.activeAnimation && BuildIU.counter != 4)
            {
                if (BuildIU.numberOfWrongObjectsConnected <= 1)
                {
                    showStoryWindow("שימו לב - יכול להיות שיש לכם חלק מיותר...");
                }
                else
                {
                    showStoryWindow("שימו לב - יכול להיות שיש לכם חלקים מיותרים...");
                }
            }
        }
    }


    private void OnMouseDown()
    {
        if (AllObjects.BuildingState == "building")
        {
            thisSatPart.isDragged = true;
            AllObjects.aPartIsBeingDragged = true;
            //GetComponent<Rigidbody>().useGravity = false;

            distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            if (!thisSatPart.isConnected && StoryWindow.activeSelf==false && BuildIU.counter!=4)
            {
                showStoryWindow(thisSatPart.Description);
            }
            else
            {
                //Debug.Log("thisSatPart.isConnected - "+ thisSatPart.isConnected+ " typewriterUI.TypeWriterIsFinished - "+ typewriterUI.TypeWriterIsFinished);
            }
        }
    }

    private void OnMouseDrag()
    {
        dragTimer += Time.deltaTime;
        if (AllObjects.BuildingState == "building")
        {
            if (dragTimer >= 0.2f)//גרירה
            {
                //float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                //Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

                Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));


                transform.position = Globals.currentMousePosition; //גרירה - האובייקט עוקב אחרי העכבר
            }
        }
    }

    private void OnMouseUp()
    {
        if (AllObjects.BuildingState == "building")
        {
            thisSatPart.isDragged = false;
            AllObjects.aPartIsBeingDragged = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = true;

            if (StoryWindow.activeSelf && StoryWinAnim.activeAnimation== false && BuildIU.counter != 4)
            {
                hideStoryWindow();
            }
            else if (StoryWinAnim.activeAnimation)
            {
                StoryWinAnim.exitAnimationTrigger = true;
            }

            if (dragTimer >= 0.2f)//אם זו הייתה גרירה
            {
                
            }
            else //אם זו הייתה לחיצה
            {
                
            }
            dragTimer = 0; //איפוס טיימר הגרירה

            //בדיקה האם נוגע ואז האם נכון
            if (thisSatPart.isConnected) //אם משחררים אותו בפנים
            {
                //Debug.Log("I'm In");
                BodyTrigger.connectedPartCounter++;
                if (BuildIU.counter == 3)
                {
                    BuildIU.counter = 4;
                }
                if (BuildIU.counter == 5)
                {
                    BuildIU.counter = 6;
                }

                //Debug.Log("released inside");
                transform.position = new Vector3(transform.position.x, 0.5f, zAtDesk);
                GetComponent<Rigidbody>().useGravity= true;

                if (thisSatPart.isCorrect)
                {
                    //Debug.Log("I'm correct");
                    //BuildIU.numberOfCorrectObjectsConnected++;
                    //Debug.Log("correct "+BuildIU.numberOfCorrectObjectsConnected);
                }
                else
                {
                    //Debug.Log("I'm wrong");
                    //BuildIU.numberOfWrongObjectsConnected++;
                    //Debug.Log("wrong " + BuildIU.numberOfWrongObjectsConnected);

                }

            }
            else //אם משחררים אותו בחוץ
            {
                if (thisSatPart.isCorrect)
                {
                    //Debug.Log("I'm correct");
                    //BuildIU.numberOfCorrectObjectsConnected--;
                    //Debug.Log("correct " + BuildIU.numberOfCorrectObjectsConnected);

                }
                else
                {
                    //Debug.Log("I'm wrong");
                    //BuildIU.numberOfWrongObjectsConnected--;
                    //Debug.Log("wrong " + BuildIU.numberOfWrongObjectsConnected);

                }
                //Debug.Log("I'm out");
                //Debug.Log("released outside");
                transform.position = initialobjectPosition;
                //GetComponent<Rigidbody>().velocity = Vector3.zero;
                //GetComponent<Rigidbody>().useGravity = true;
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
                        thisSatPart.isCorrect= true;

                    }
                    //colorMe(); //להוריד
                }
            }
        }
    }

    private void colorMe()
    {
        List<MeshRenderer> renderers = GetComponentsInChildren<MeshRenderer>().ToList();
        foreach (MeshRenderer ren in renderers)
        {
            if (thisSatPart.isCorrect)
            {
                ren.material.color = Color.green;
            }
            else
            {
                ren.material.color = Color.red;
            }
        }
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

    void showStoryWindow(string textContent)
    {
        //typewriterUI.TextToType = textContent;
        StoryWinAnim.exitAnimationTrigger = false;
        StoryWindow.GetComponentInChildren<TextMeshProUGUI>().text = textContent;
        Storyicon.SetActive(true);
        StoryWindow.SetActive(true);
        if (typewriterUI.TypeWriterIsFinished)
        {

        }

    }

    void hideStoryWindow()
    {
        if (!StoryWinAnim.exitAnimationTrigger)
        {
            if (!StoryWinAnim.activeAnimation)
            {
                StoryWinAnim.exitAnimationTrigger = true;
            }
        }
    }
}
