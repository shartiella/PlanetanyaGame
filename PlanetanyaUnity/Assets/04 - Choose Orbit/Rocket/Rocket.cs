using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject earth;

    private Rigidbody rocketRB;
    private Rigidbody earthRB;

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
    public static float EarthRocketDistance;
    //private float MINearthRocketDistance=0;
    //private float MAXearthRocketDistance=0;
    //public static float combinedDistancesFromEarth = 0;
    //private float lastCombinedDistancesFromEarth = 0;
    public static float Eccentricity = 0;
    public static bool inStableOrbit = false;

    public static int launchCounter = 0;

    [SerializeField] private GameObject pushBTN;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB=GetComponent<Rigidbody>();
        earthRB= earth.GetComponent<Rigidbody>();

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

        if (Globals.rocketStatus == "launched")
        {
            CalculateEccentricity();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (Globals.ChosenSatellite.Orbit == "none")
        //{
        //    launcher.SetActive(false);
        //}
        //else
        //{
        //    launcher.SetActive(true);
        //}

        //EarthRocketVec = earth.transform.position - rocket.transform.position;
        EarthRocketDistance = (earth.transform.position - rocket.transform.position).magnitude;

        if (EarthRocketDistance > 12)
        {
            Globals.rocketStatus = "crashed";
            OrbitManager.crashFromEarthCollision = false;
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

        if (Globals.rocketStatus == "launched")
        {
            //if (EarthRocketDistance > MAXearthRocketDistance)
            //{
            //    MAXearthRocketDistance = EarthRocketDistance;
            //    //Debug.Log("MAXearthRocketDistance " + MAXearthRocketDistance);
            //}
            //else if (EarthRocketDistance < MINearthRocketDistance)
            //{
            //    MINearthRocketDistance = EarthRocketDistance;
            //    //Debug.Log("MINearthRocketDistance " + MINearthRocketDistance);
            //}

            //combinedDistancesFromEarth = MINearthRocketDistance/MAXearthRocketDistance;
            //Debug.Log("combinedDistancesFromEarth "+combinedDistancesFromEarth);
            //if (lastCombinedDistancesFromEarth != combinedDistancesFromEarth)
            //{
            //    lastCombinedDistancesFromEarth = combinedDistancesFromEarth;
            //    inStableOrbit = false;
            //}
            //else
            //{
            //    inStableOrbit = true;
            //    Debug.Log("inStableOrbit " + inStableOrbit);

            //}

            //CalculateEccentricity();
        }

        if (Globals.rocketStatus == "launching" || Globals.rocketStatus == "launched" || Globals.rocketStatus == "pushed" || Globals.rocketStatus == "inOrbit")
        {
            float angle = Mathf.Atan2(rocketRB.velocity.y, rocketRB.velocity.x) * Mathf.Rad2Deg;
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
                if (OrbitManager.counter > 24)
                {
                    pushBTN.SetActive(true);
                }
                //MINearthRocketDistance = EarthRocketDistance;
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
        //launchCounter++;
        Debug.Log("launchCounter: " + launchCounter);
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
        if (Globals.rocketStatus == "launched")
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
    }


    //כפתור איפוס הטיל
    public void resetRocket()
    {
        if (Globals.rocketStatus != "inOrbit")
        {
            rocketT.position = new Vector3(0, 1.7f, 0);
            rocketT.rotation = Quaternion.identity;

            //MINearthRocketDistance = 0;
            //MAXearthRocketDistance = 0;
            //combinedDistancesFromEarth = 0;
            if (OrbitManager.counter > 24)
            {
                pushBTN.SetActive(false);
            }

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
            if (OrbitManager.showLauncherAfterCrash)
            {
                launcher.SetActive(true);
                launcher.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                launcher.SetActive(false);
            }
            //launcher.GetComponent<MeshRenderer>().enabled = true;

        }
    }

    public void CalculateEccentricity()
    {
        Vector3 r = transform.position;
        Vector3 v = rocketRB.velocity;

        Vector3 h = Vector3.Cross(r, v);

        float mu = 24;//מיו שחישבנו

        Vector3 e = (Vector3.Cross(v, h)) / mu - r / r.magnitude;

        Eccentricity = e.magnitude;
        Debug.Log("Eccentricity: " + Eccentricity);

    }

}
