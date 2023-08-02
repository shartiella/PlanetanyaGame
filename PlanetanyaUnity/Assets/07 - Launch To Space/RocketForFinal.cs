using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class RocketForFinal : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject earth;

    [SerializeField] private Rigidbody rocketRB;
    private Rigidbody earthRB;

    [SerializeField] private Transform rocketT;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem smoke;

    private float launchTimer = 0.0f;
    private float pushTimer = 0.0f;

    //לבטל כשנדע מה טוב
    [SerializeField] private float Xforce;
    [SerializeField] private float Yforce;
    [SerializeField] private float pushAfter;
    [SerializeField] private float forceAmountonY;
    private float demoTimer = 0.0f;
    private int pushTimes = 0;
    //private float timerToLaunch = 0.0f;

    public float forceMultiplier=0;




    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("ROCKERFORFINAL");
        //Globals.rocketStatus = "toLaunch";
        launchDemo();
    }

    // Update is called once per frame
    void Update()
    {
        rocketRB.velocity += Globals.GravityForce(earth, rocket, 0.5f * forceMultiplier);

        //דמו
        if (Globals.demo)
        {
            playDemo();
        }
        else
        {
            demoTimer = 0.0f;
            pushTimes = 0;
        }

        if (Globals.rocketStatus == "launching" || Globals.rocketStatus == "launched" || Globals.rocketStatus == "pushed" || Globals.rocketStatus == "inOrbit")
        {
            float angle = Mathf.Atan2(rocketRB.velocity.y, rocketRB.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rocket.transform.Rotate(0, 180, 90);

            if (Globals.rocketStatus == "launching")
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = true;
                fireEmission.rateOverDistance = 10;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = true;
                smokeEmission.rateOverTime = 400;

            }
            else if (Globals.rocketStatus == "pushed")
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = true;
                fireEmission.rateOverDistance = 10;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = true;
                smokeEmission.rateOverTime = 400;

            }
            else if (Globals.rocketStatus == "toLaunch")
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = false;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = false;

            }
            else
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = false;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = false;
            }

        }
        else
        {

        }

        if (Globals.rocketStatus == "launching")
        {

            launchTimer += Time.deltaTime;
            //Debug.Log(launchTimer);

            if (launchTimer >= 1)
            {
                Globals.rocketStatus = "launched";
                //if (OrbitManager.counter > 24 && !Globals.demo)
                //{
                //    pushBTN.SetActive(true);
                //}
                launchTimer = 0;
            }
        }

        if (Globals.rocketStatus == "pushed")
        {
            pushTimer += Time.deltaTime;
            //Debug.Log(pushTimer);

            if (pushTimer >= 0.5)
            {
                Globals.rocketStatus = "launched";
                //Debug.Log(Globals.rocketStatus);
                pushTimer = 0;
            }
        }
    }


    //public void push()
    //{
    //    if (Globals.rocketStatus == "launched")
    //    {
    //        if (Globals.ChosenSatellite.Orbit == "LEO")
    //        {
    //            pushForce = 0.6f;
    //            //3.72  0.12
    //            //3.69  0.38
    //            //3.63  0.18
    //            //3.66  0.41
    //            //3.66  0.25
    //            //3.69  0.38
    //            //3.76  0.54
    //            //3.53  0.05
    //        }
    //        else if (Globals.ChosenSatellite.Orbit == "MEO")
    //        {
    //            pushForce = 0.8f;
    //            //3.85  -0.50
    //            //3.85  -0.44
    //            //3.85  -0.53
    //            //3.89  -0.47
    //            //3.82  -0.63
    //            //4.02  -0.18
    //        }
    //        else if (Globals.ChosenSatellite.Orbit == "GEO")
    //        {
    //            pushForce = 0.9f;
    //            //3.98  -0.82
    //            //3.85  -1.08
    //            //4.02  -0.79
    //            //3.82  -1.15
    //            //3.98  -0.86
    //        }
    //        rocketRB.velocity += transform.up * pushForce;
    //        Globals.rocketStatus = "pushed";
    //    }
    //}

    //שיגורי דמו
    public void launchDemo()
    {
        Globals.demo = true;
        demoTimer = 0.0f;
        //pushTime = 0.0f;

        Globals.rocketStatus = "launching";
        //Debug.Log(Globals.rocketStatus);

        Globals.Gravity = true;
        Xforce = -92;
        Yforce = 40;
        pushAfter = 1;
        forceAmountonY = -18;

        rocketRB.AddForce(Xforce, Yforce, 0);
    }

    private void playDemo()
    {
        demoTimer += Time.deltaTime;

        if (demoTimer > pushAfter)
        {
            if (pushTimes == 0)
            {
                Globals.rocketStatus = "pushed";
                rocketRB.AddForce(0, forceAmountonY, 0);
                pushTimes++;
            }
            demoTimer = 0.0f;
        }
    }
}
