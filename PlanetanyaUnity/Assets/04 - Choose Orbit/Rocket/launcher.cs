using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class launcher : MonoBehaviour
{
    [SerializeField] private Material holdingFeedback;
    [SerializeField] private Material defaultColor;

    private Vector3 initialLauncherPosition; //המיקום ההתחלתי של העיגול
    private Vector3 initialrocketPosition; //המיקום ההתחלתי של הטיל

    private Vector3 forceAtPlayer; //כוח השיגור
    [SerializeField] private float forceFactor;

    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject[] TrajectoryDots;
    [SerializeField] private int dotNumber;
    [SerializeField] private GameObject Earth;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject trajectoryDotsParent;
    [SerializeField] private GameObject resetBTN;

    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private Vector3 currentSpeed;
    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private Vector3 nextSpeed;

    [SerializeField] private float timeFactor;

    private Vector3 fullScale;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;

        transform.localScale = new Vector3(0, 0, 0);
        transform.LeanScale(fullScale, 0.5f).setDelay(0.5f).setEaseOutElastic();
    }

    // Update is called once per frame
    void Update()
    {

        if (Globals.rocketStatus == "inOrbit" || Globals.demo == true)
        {

            GetComponent<MeshRenderer>().enabled = false;
        }

        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
    }


    private void Awake()
    {
        //gameObject.SetActive(true);

        initialLauncherPosition = transform.position; //קביעת המיקום ההתחלתי של העיגול
        fullScale = transform.localScale;
        initialrocketPosition = new Vector3(0, 1.7f, 0); //קביעת המיקום ההתחלתי של הטיל

        TrajectoryDots = new GameObject[dotNumber]; //יצירת מערך נקודות הסימון
    }

    private void OnMouseDown()
    {
        GetComponent<Renderer>().material= holdingFeedback; //צביעת העיגול בצהוב

        //יצירת הנקודות תחת האובייקט
        for (int i = 1; i < dotNumber; i++)
        {
            TrajectoryDots[i] = Instantiate(dotPrefab, trajectoryDotsParent.transform);
            float relativeScale = (float)(dotNumber - i) / (float)dotNumber;
            TrajectoryDots[i].transform.localScale = new Vector3(relativeScale, 0.01f, relativeScale);
            //Debug.Log(TrajectoryDots[i].transform.localScale);
        }

    }

    private void OnMouseDrag()
    {
        if (Globals.rocketStatus == "toLaunch")
        {
            transform.position = Globals.currentMousePosition; //גרירה - העיגול עוקב אחרי העכבר
            //לצורך השיגור
            //distanceBetweenInitialToRelease = Vector3.Distance(initialPosition, Globals.currentMousePosition);
            //directionFromInitialPosition = (initialPosition - currentMousePosition).normalized;

            //direction = initialPosition - Globals.currentMousePosition;
            //Globals.launchForce = direction * forceFactor;

            //לצורך הנקודות
            forceAtPlayer = initialLauncherPosition - Globals.currentMousePosition;

            Globals.launchForce = forceAtPlayer * forceFactor;

            currentSpeed = Globals.launchForce;
            currentPosition = initialrocketPosition;
            //currentPosition = new Vector3(0, 0, 0);

            for (int i = 1; i < dotNumber; i++)
            {
                TrajectoryDots[i].transform.position = currentPosition;
                calculateNextPosition(i* timeFactor, TrajectoryDots[i]);
                currentPosition = nextPosition;
                currentSpeed = nextSpeed;
            }
            rocket.transform.LookAt(TrajectoryDots[3].transform);
            rocket.transform.Rotate(-90, 180, 0);

            Debug.Log(rocket.transform.rotation.eulerAngles);

            if (rocket.transform.rotation.eulerAngles.z == 180 && rocket.transform.rotation.eulerAngles.x > 300 && (rocket.transform.rotation.eulerAngles.y < 269 || rocket.transform.rotation.eulerAngles.y > 90))
            {
                OrbitManager.LaunchTowardsEarth = true;
            }
            else
            {
                OrbitManager.LaunchTowardsEarth = false;
            }
        }
    }

    private void OnMouseUp()
    {
        if (OrbitManager.LaunchTowardsEarth)
        {
            OrbitManager.lastLaunchWasTowardsEarth = true;
            Debug.Log("LaunchedTowardsEarth");
        }
        else
        {
            OrbitManager.lastLaunchWasTowardsEarth = false;
        }

        //Debug.Log(Globals.currentMousePosition.ToString());
        Globals.lastLaunchForce=Globals.launchForce;
        Debug.Log("launch force: " + Globals.launchForce);

        GetComponent<Renderer>().material = defaultColor;
        if (Globals.demo==false)
        {
            //Globals.numberOfLaunches++;
            Rocket.launchCounter++;
        }

        if (Globals.rocketStatus == "toLaunch")
        {
            //Globals.launchForce = forceAtPlayer * forceFactor;

            OrbitManager.lastFingerRelease = Globals.currentMousePosition; //שמירת המיקום האחרון של העכבר
            Globals.rocketStatus = "LAUNCH"; //קריאה לשיגור
            transform.position = initialLauncherPosition; //חזרה למיקום ההתחלתי
            GetComponent<MeshRenderer>().enabled = false; //הסתרה

            if (OrbitManager.showResetAfterLaunch)
            {
                resetBTN.SetActive(true);
            }

            //מחיקת הנקודות
            for (int i = 1; i < dotNumber; i++)
            {
                Destroy(TrajectoryDots[i]);
            }
        }

    }


    public void calculateNextPosition(float timeInterval, GameObject dot)
    {
        //return new Vector3(initialPosition.x, initialPosition.y + 0.1f, 0) +
        //    new Vector3(direction.x * forceFactor, direction.y * forceFactor, 0) * elapsedTime +
        //    0.5f * Globals.GravityForce(Earth, dot, 0.5f) * elapsedTime * elapsedTime;

        //חישוב המיקום של הנקודות לפי:
        //המיקום ממנו הן נמתחות
        //הכוח של השיגור
        //כוח המשיכה של כדור הארץ

        //    return new Vector3(initialPosition.x, initialPosition.y + 0.2f, 0) +
        //forceAtPlayer * forceFactor * elapsedTime +
        //0.5f * Globals.GravityForce(Earth, dot, 0.5f) * elapsedTime * elapsedTime;

        nextPosition = currentPosition + currentSpeed * timeInterval + 0.5f * Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeInterval * timeInterval ;
        nextSpeed = currentSpeed + Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeInterval;
        
    }

}
