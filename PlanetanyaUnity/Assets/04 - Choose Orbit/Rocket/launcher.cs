using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class launcher : MonoBehaviour
{
    [SerializeField] private Material holdingFeedback;
    [SerializeField] private Material defaultColor;
    [SerializeField] private GameObject Outline;

    private Vector3 initialLauncherPosition; //המיקום ההתחלתי של העיגול
    private Vector3 initialrocketPosition; //המיקום ההתחלתי של הטיל

    private Vector3 forceAtPlayer; //כוח השיגור

    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject[] TrajectoryDots;
    [SerializeField] private Vector3[] TrajectoryPositions;
    [SerializeField] private Vector3[] TrajectorySpeeds;
    [SerializeField] private int dotNumberToCalculate;
    [SerializeField] private int dotsToShow;
    private int dotsToSkip;
    [SerializeField] private GameObject dotForCalculations;
    [SerializeField] private GameObject Earth;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject trajectoryDotsParent;

    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private Vector3 currentSpeed;
    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private Vector3 nextSpeed;

    [SerializeField] private float timeFactor;

    //[SerializeField] private float trajectoryFix = 1;

    private Vector3 fullScale;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        //GetComponent<MeshRenderer>().enabled = true;
        Outline.SetActive(true);
        transform.position = initialLauncherPosition; //חזרה למיקום ההתחלתי
        GetComponent<Renderer>().material = defaultColor;

        transform.localScale = new Vector3(0, 0, 0);
        transform.LeanScale(fullScale, 0.5f).setDelay(0.5f).setEaseOutElastic();
    }

    // Update is called once per frame
    void Update()
    {

        if (Globals.rocketStatus == "inOrbit" || Globals.demo == true)
        {

            GetComponent<MeshRenderer>().enabled = false;
            Outline.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
            Outline.GetComponent<MeshRenderer>().enabled = true;
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

        TrajectoryDots = new GameObject[dotsToShow]; //יצירת מערך נקודות הסימון
        dotsToSkip = Mathf.RoundToInt(dotNumberToCalculate / dotsToShow);
        TrajectoryPositions = new Vector3[dotNumberToCalculate];
        TrajectorySpeeds = new Vector3[dotNumberToCalculate];
    }

    private void OnMouseDown()
    {
        GetComponent<Renderer>().material= holdingFeedback; //פידבק משיכה ויזואלי
        dotForCalculations = Instantiate(dotPrefab, trajectoryDotsParent.transform);

        //יצירת הנקודות תחת האובייקט
        for (int i = 1; i < dotsToShow; i++)
        {
            TrajectoryDots[i] = Instantiate(dotPrefab, trajectoryDotsParent.transform);
            //float relativeScale = (float)(dotsToShow - i) / (float)dotsToShow;
            //TrajectoryDots[i].transform.localScale = new Vector3(relativeScale, 0.01f, relativeScale);
        }

    }

    private void OnMouseDrag()
    {
        if (Globals.rocketStatus == "toLaunch")
        {
            Outline.SetActive(false);

            transform.position = Globals.currentMousePosition; //גרירה - העיגול עוקב אחרי העכבר
            //לצורך השיגור
            //distanceBetweenInitialToRelease = Vector3.Distance(initialPosition, Globals.currentMousePosition);
            //directionFromInitialPosition = (initialPosition - currentMousePosition).normalized;

            //direction = initialPosition - Globals.currentMousePosition;
            //Globals.launchForce = direction * forceFactor;

            //לצורך הנקודות
            forceAtPlayer = initialLauncherPosition - Globals.currentMousePosition;

            Globals.launchForce = forceAtPlayer * OrbitManager.forceFactor;

            currentSpeed = Globals.launchForce;
            currentPosition = initialrocketPosition;
            //currentPosition = new Vector3(0, 0, 0);

            float maxdistance = 0;
            int indexOfMaxDistance = 0;
            float currentdistance = 0;
            for (int i = 1; i < dotNumberToCalculate; i++)
            {
                dotForCalculations.transform.position = currentPosition;
                dotForCalculations.GetComponent<MeshRenderer>().enabled = false;

                TrajectoryPositions[i] = currentPosition;
                TrajectorySpeeds[i] = currentSpeed;

                //TrajectoryDots[i / dotsToSkip].transform.position = currentPosition;

                if (i%dotsToSkip== 0)
                {
                    TrajectoryDots[i/dotsToSkip].transform.position = TrajectoryPositions[i];
                }

                currentdistance = (currentPosition - Earth.transform.position).magnitude;
                if (currentdistance > maxdistance)
                {
                    maxdistance = currentdistance;
                    if (indexOfMaxDistance == (i/dotsToSkip - 1))
                    {
                        indexOfMaxDistance = i/dotsToSkip;
                    }
                }

                //if (i % 2 == 0)
                //{
                //    TrajectoryDots[i].GetComponent<MeshRenderer>().enabled = true;
                //}
                //else
                //{
                //    TrajectoryDots[i].GetComponent<MeshRenderer>().enabled = false;
                //}

                calculateNextPosition(i, dotForCalculations, TrajectoryPositions[i], TrajectorySpeeds[i]);
                currentPosition = nextPosition;
                currentSpeed = nextSpeed;
            }
            //Debug.Log(indexOfMaxDistance + " = indexOfMaxDistance");

            int dotsToShowAndShrink = indexOfMaxDistance + 3;
            for (int x = 1; x < dotsToShow; x++)
            {
                if (x <= dotsToShowAndShrink && indexOfMaxDistance > 0)
                {
                    TrajectoryDots[x].GetComponent<MeshRenderer>().enabled = true;
                    float relativeScale = (float)(dotsToShowAndShrink - x) / (float)dotsToShowAndShrink;
                    TrajectoryDots[x].transform.localScale = new Vector3(relativeScale, 0.01f, relativeScale);
                }
                else
                {
                    TrajectoryDots[x].GetComponent<MeshRenderer>().enabled = false;
                }
            }

            rocket.transform.LookAt(TrajectoryDots[2].transform);
            rocket.transform.Rotate(-90, 180, 0);

            //Debug.Log(rocket.transform.rotation.eulerAngles);

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
        OrbitManager.launchesCounter++;

        if (Globals.demo == false)
        {
            //Globals.numberOfLaunches++;
            Rocket.launchCounter++;
        }

        if (OrbitManager.LaunchTowardsEarth)
        {
            OrbitManager.lastLaunchWasTowardsEarth = true;
            //Debug.Log("LaunchedTowardsEarth");
        }
        else
        {
            OrbitManager.lastLaunchWasTowardsEarth = false;
        }

        //Debug.Log(Globals.currentMousePosition.ToString());
        Globals.lastLaunchForce=Globals.launchForce;
        //Debug.Log("launch force: " + Globals.launchForce);





        if (Globals.rocketStatus == "toLaunch")
        {
            //Globals.launchForce = forceAtPlayer * forceFactor;

            OrbitManager.lastFingerRelease = Globals.currentMousePosition; //שמירת המיקום האחרון של העכבר
            Globals.rocketStatus = "LAUNCH"; //קריאה לשיגור

            //GetComponent<MeshRenderer>().enabled = false; //הסתרה


            //מחיקת הנקודות
            for (int i = 1; i < dotsToShow; i++)
            {
                Destroy(TrajectoryDots[i]);
            }
        }

        Outline.SetActive(true);
        gameObject.SetActive(false);
    }


    public void calculateNextPosition(int i, GameObject dot, Vector3 curPos, Vector3 curSpee)
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





        //nextPosition = currentPosition + currentSpeed * timeInterval + 0.5f * Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeInterval * timeInterval ;
        //nextSpeed = currentSpeed + Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeInterval;


        nextPosition = curPos + curSpee * timeFactor + 0.5f * Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeFactor * timeFactor;
        nextSpeed = curSpee + Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeFactor;
    }

}
