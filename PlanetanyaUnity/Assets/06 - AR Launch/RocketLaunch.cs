//using log4net.Filter;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebXR;

public class RocketLaunch : MonoBehaviour
{
    //[SerializeField] private GameObject rocket;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem smoke;
    private Rigidbody rocketRB;

    public float yforce;
    public float firerate;
    public float smokerate;

    [SerializeField] private GameObject SatelliteToRocket;
    [SerializeField] private GameObject rocketBottom;
    [SerializeField] private GameObject rocketTop;
    [SerializeField] private GameObject rocketFull;

    [SerializeField] private GameObject launchBTN;
    [SerializeField] private GameObject cineMachine;

    //[SerializeField] private GameObject window1;
    //[SerializeField] private GameObject window2;
    //[SerializeField] private GameObject window3;
    //[SerializeField] private GameObject window4;
    //[SerializeField] private GameObject toNext;

    //[SerializeField] private GameObject ARBTN;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(WebXRManager.OnXRChange);
        //if (WebXRManager.Instance.isSupportedAR)
        //{
        //    AR.SetActive(true);
        //}
        //else
        //{
        //    AR.SetActive(false);
        //}
        rocketRB = GetComponent<Rigidbody>();
        Globals.rocketStatus = "ARoff";
        //Debug.Log(Globals.rocketStatus);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Globals.rocketStatus == "ARoff")// לפני הפעלת מציאות רבודה
        {
            //ARBTN.SetActive(true);
        }
        else if (Globals.rocketStatus == "lookingAround") //מסתכל מסביב עד שהוא מול הטיל
        {
            //ARBTN.SetActive(false);

            //window1.SetActive(true);
            SatelliteToRocket.SetActive(false);

        }
        //else if(Globals.rocketStatus== "LookAtRocket") //רואה את הטיל - עד שהוא לוחץ על כפתור חיבור הלווין
        //{
        //    //window1.SetActive(false);
        //    //window2.SetActive(true);
        //}
        else if (Globals.rocketStatus == "connectSat") //חיבור הטיל ללווין - עד שהלוויין מחובר
        {
            SatelliteToRocket.SetActive(true);

            //window2.SetActive(false);
            //window3.SetActive(true);
        }
        else if (Globals.rocketStatus == "satConnected")
        {
            if (!rocketTop.activeSelf && !rocketFull.activeSelf)
            {
                rocketTop.SetActive(true);
            }
            else if (typewriterUI.TypeWriterIsFinished)
            {
                rocketTop.SetActive(false);
                rocketBottom.SetActive(false);
                rocketFull.SetActive(true);
                launchBTN.SetActive(true);
            }
            SatelliteToRocket.SetActive(false);
        }
        else if (Globals.rocketStatus == "ToLaunch") //מוכן לשיגור - עד שלוחץ על כפתור השיגור
        {
            //window3.SetActive(false);
            //window4.SetActive(true);

            launchBTN.SetActive(true);
        }


        if (rocketRB.position.y > 200 && ARcanvasManager.counter==8)
        {
            ARcanvasManager.counter = 9;
            //toNext.SetActive(true);
        }

        if (Globals.rocketStatus == "launching") //משוגר
        {
            //            ARcanvasManager.counter = 5;

            //float angle = Mathf.Atan2(rocket.GetComponent<Rigidbody>().velocity.y, rocket.GetComponent<Rigidbody>().velocity.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, angle);
            //rocket.transform.Rotate(0, 180, 90);

            //window4.SetActive(false);

            rocketRB.AddForce(0, yforce, 0);
            launchBTN.SetActive(false);

            if (Globals.rocketStatus == "launching")
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = true;
                fireEmission.rateOverTime = firerate;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = true;
                smokeEmission.rateOverTime = smokerate;

            }
            else
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = false;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = false;
            }
        }
    }

    public void launchIt()
    {
        //Debug.Log(Globals.rocketStatus);
        cineMachine.SetActive(true); //לבטל כשנצליח להטמיע מציאות רבודה

        Globals.rocketStatus = "launching";

        //if (WebXRManager.Instance.XRState == WebXRState.NORMAL)
        //{
        //    Debug.Log("TRUE");
        //}
        //else
        //{
        //    Debug.Log("FALSE");
        //}

        //Globals.launchForce=new Vector3(0, yforce, 0);
        //GetComponent<Rigidbody>().velocity = Globals.launchForce;
    }

    //public void connectSat()
    //{
    //    Globals.rocketStatus = "connectSat";
    //}

    //public void toNextscene()
    //{
    //    if (WebXRManager.Instance.XRState == WebXRState.AR)
    //    {
    //        WebXRManager.Instance.ToggleAR();
    //        Debug.Log("AR is OFF");

    //    }
    //    else
    //    {
    //        Debug.Log("no AR");
    //    }
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}


    //private void OnMouseUp()
    //{
    //    Globals.rocketStatus = "LookAtRocket";
    //}

    //public void ARon()
    //{
    //    Globals.rocketStatus = "lookingAround";

    //    //לחבר כפתור של מציאות רבודה לפונקציה הזאת
    //    if (WebXRManager.Instance.XRState == WebXRState.NORMAL)
    //    {
    //        WebXRManager.Instance.ToggleAR();
    //        Debug.Log("AR is ON");

    //    }
    //    else
    //    {
    //        Debug.Log("no AR");
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    ARcanvasManager.counter++;
    //}
}
