using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject earth;

    [SerializeField] private Rigidbody rocketRB;
    [SerializeField] private Transform rocketT;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private ParticleSystem trail;
    [SerializeField] private ParticleSystem explosion;
    private float launchTimer = 0.0f;
    private float pushTimer = 0.0f;
    [SerializeField] private GameObject launcher;
    private float crashTimer = 0.0f;
    public float pushForce;

    // Start is called before the first frame update
    void Start()
    {
        Globals.rocketStatus = "toLaunch";
        Debug.Log(Globals.rocketStatus);

    }

    private void FixedUpdate()
    {

        if (Globals.Gravity)
        {
            //AddGravityForce(EarthRigidbody, rocketRigidbody);
            //rocket.GetComponent<Rigidbody>().AddForce(Globals.GravityForce(earth, rocket, 0.5f));
            rocketRB.velocity += Globals.GravityForce(earth, rocket, 0.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.ChosenSatellite.Orbit == "none")
        {
            launcher.SetActive(false);
        }
        else
        {
            launcher.SetActive(true);
        }

        if (Globals.rocketStatus == "restart")
        {
            resetRocket();
        }

        if (Globals.rocketStatus == "LAUNCH")
        {
            launchIt();
        }

        if (Globals.rocketStatus == "crashed")
        {
            var explosionEmission = explosion.emission;
            explosionEmission.enabled = true;
            explosionEmission.rateOverTime = 500;

            crashTimer += Time.deltaTime;

            if (crashTimer >= 0.2f)
            {
                explosionEmission.enabled = false;
            }

            if (crashTimer >= 1)
            {
                resetRocket();
                crashTimer = 0;
            }

        }

            if (Globals.rocketStatus == "launching" || Globals.rocketStatus == "launched" || Globals.rocketStatus == "pushed" || Globals.rocketStatus== "inOrbit")
        {
            float angle = Mathf.Atan2(rocketRB.velocity.y,rocketRB.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rocket.transform.Rotate(0, 180, 90);

            var trailEmission = trail.emission; // Stores the module in a local variable
            trailEmission.enabled = true; // Applies the new value directly to the Particle System

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
            var trailEmission = trail.emission; // Stores the module in a local variable
            trailEmission.enabled = false;
        }

        if (Globals.rocketStatus == "launching")
        {
            launchTimer += Time.deltaTime;
            //Debug.Log(launchTimer);

            if (launchTimer >= 1)
            {
                Globals.rocketStatus = "launched";
                //Debug.Log(Globals.rocketStatus);
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

    public void launchIt()
    {
        Globals.rocketStatus = "launching";
        Debug.Log(Globals.rocketStatus);
        Globals.Gravity = true;

        //rocketRB.AddForce(Globals.launchForce);
        rocketRB.velocity = Globals.launchForce;
    }


    //כפתורי כיווונון - לשפר
    public void gentlePush(string direction)
    {
        if (Globals.rocketStatus == "launched")
        {
            Globals.rocketStatus = "pushed";

            float pushForce = 0.5f;
            if (direction == "down")
            {
                //rocketRB.AddForce(0, -7, 0);
                rocketRB.velocity += new Vector3(0, -pushForce, 0);
            }
            else if (direction == "up")
            {
                //rocketRB.AddForce(0, 7, 0);
                rocketRB.velocity += new Vector3(0, pushForce, 0);

            }
            else if (direction == "right")
            {
                //rocketRB.AddForce(7, 0, 0);
                rocketRB.velocity += new Vector3(pushForce, 0,0);
            }
            else if (direction == "left")
            {
                //rocketRB.AddForce(-7, 0, 0);
                rocketRB.velocity += new Vector3(-pushForce, 0, 0);

            }
        }
    }

    public void push()
    {

        if (Globals.ChosenSatellite.Orbit == "LEO")
        {
            pushForce = 0.6f;
            //3.72  0.12
            //3.69  0.38
            //3.63  0.18
            //3.66  0.41
            //3.66  0.25
            //3.69  0.38
            //3.76  0.54
            //3.53  0.05
        }
        else if (Globals.ChosenSatellite.Orbit == "MEO")
        {
            pushForce = 0.8f;
            //3.85  -0.50
            //3.85  -0.44
            //3.85  -0.53
            //3.89  -0.47
            //3.82  -0.63
            //4.02  -0.18
        }
        else if (Globals.ChosenSatellite.Orbit == "GEO")
        {
            pushForce = 0.9f;
            //3.98  -0.82
            //3.85  -1.08
            //4.02  -0.79
            //3.82  -1.15
            //3.98  -0.86
        }
        rocketRB.velocity += transform.up * pushForce;
        Globals.rocketStatus = "pushed";
    }


    //כפתור איפוס הטיל
    public void resetRocket()
    {
        if (Globals.rocketStatus != "inOrbit")
        {
            rocketT.position = new Vector3(0, 1.7f, 0);
            rocketT.rotation = Quaternion.identity;

            rocketRB.velocity = Vector3.zero;
            rocketRB.angularVelocity = Vector3.zero;
            Globals.Gravity = false;
            Globals.orbitTime = 0;
            Globals.orbit = "none";
            Globals.rocketStatus = "toLaunch";
            Debug.Log(Globals.rocketStatus);
            var fireEmission = fire.emission;
            fireEmission.enabled = false;

            var smokeEmission = smoke.emission;
            smokeEmission.enabled = false;

            var explosionEmission = explosion.emission;
            explosionEmission.enabled = false;

            Globals.demo = false;
            launcher.GetComponent<MeshRenderer>().enabled = true;

        }
    }

 
}
