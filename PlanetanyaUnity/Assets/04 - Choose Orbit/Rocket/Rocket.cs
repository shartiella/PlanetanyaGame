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
    [SerializeField] private ParticleSystem trailSUCCESS;
    [SerializeField] private ParticleSystem explosion;
    private float launchTimer = 0.0f;
    private float pushTimer = 0.0f;
    [SerializeField] private GameObject launcher;
    private float crashTimer = 0.0f;
    public static float pushForce;
    public static float EarthRocketDistance;
    //private float MINearthRocketDistance=0;
    //private float MAXearthRocketDistance=0;
    //public static float combinedDistancesFromEarth = 0;
    //private float lastCombinedDistancesFromEarth = 0;
    public static float Eccentricity = 0;
    public static bool inStableOrbit = false;

    public static int launchCounter = 0;

    [SerializeField] private GameObject pushBTN;
    [SerializeField] private GameObject demoBTN;
    [SerializeField] private GameObject resetBTN;
    [SerializeField] private GameObject Goal;

    public static bool rocketIsGoingToCrash = false;
    public GameObject distancetocrash;

    //לבטל כשנדע מה טוב
    [SerializeField] private float Xforce;
    [SerializeField] private float Yforce;
    [SerializeField] private float pushAfter;
    [SerializeField] private float forceAmountonY;
    private float demoTimer = 0.0f;
    private int pushTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB=GetComponent<Rigidbody>();
        earthRB= earth.GetComponent<Rigidbody>();

        Globals.rocketStatus = "toLaunch";
        //Debug.Log(Globals.rocketStatus);

    }

    // Update is called once per frame
    void FixedUpdate()
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
            //isRocketGoingToCrash();
        }

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


        //EarthRocketVec = earth.transform.position - rocket.transform.position;
        EarthRocketDistance = (earth.transform.position - rocket.transform.position).magnitude;

        if (transform.position.y > 9.4f || transform.position.y < -12.8 || transform.position.x > 9.4f || transform.position.x < -12.8 || EarthRocketDistance > 12)
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

            if (Globals.rocketStatus == "launching")
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = true;
                fireEmission.rateOverDistance = 10;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = true;
                smokeEmission.rateOverTime = 400;

                var trailEmission = trail.emission;
                trailEmission.enabled = true;
            }
            else if (Globals.rocketStatus == "pushed")
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = true;
                fireEmission.rateOverDistance = 10;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = true;
                smokeEmission.rateOverTime = 400;

                var trailEmission = trail.emission;
                trailEmission.enabled = true;
            }
            else if (Globals.rocketStatus == "inOrbit" && !Globals.demo)
            {
                var trailEmission = trail.emission;
                trailEmission.enabled = false;

                var trailOK = trailSUCCESS.emission;
                trailOK.enabled = true;
            }
            else if (Globals.rocketStatus=="toLaunch")
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = false;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = false;

                var trailEmission = trail.emission;
                trailEmission.enabled = false;
            }
            else
            {
                var fireEmission = fire.emission;
                fireEmission.enabled = false;

                var smokeEmission = smoke.emission;
                smokeEmission.enabled = false;
            }

            if (OrbitManager.showResetAfterLaunch)
            {
                resetBTN.SetActive(true);
            }
            else
            {
                resetBTN.SetActive(false);
            }
        }
        else
        {
            var trailEmission = trail.emission; // Stores the module in a local variable
            trailEmission.enabled = false;
        }

        if (Globals.rocketStatus == "launching")
        {
            if (OrbitManager.showPushAfterLaunch && !Globals.demo)
            {
                pushBTN.SetActive(true);
            }
            else
            {
                pushBTN.SetActive(false);
            }

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

    public void launchIt()
    {
        //launchCounter++;
        //Debug.Log("launchCounter: " + launchCounter);
        Globals.rocketStatus = "launching";
        //Debug.Log(Globals.rocketStatus);
        Globals.Gravity = true;

        //rocketRB.AddForce(Globals.launchForce);
        rocketRB.velocity = Globals.launchForce;
    }


    //כפתורי כיווונון - לשפר
    public void gentlePush(string direction)
    {
        if (Globals.rocketStatus == "launched" || Globals.rocketStatus == "launching")
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
        //if (Globals.rocketStatus == "launched")
        //{
            OrbitManager.RocketHasBeenPushed = true;

            //if (Globals.ChosenSatellite.Orbit == "LEO")
            //{
            //    pushForce = 0.6f;
            //}
            //else if (Globals.ChosenSatellite.Orbit == "MEO")
            //{
            //    pushForce = 0.8f;
            //}
            //else if (Globals.ChosenSatellite.Orbit == "GEO")
            //{
            //    pushForce = 0.9f;
            //}
            rocketRB.velocity += transform.up * pushForce;
            Globals.rocketStatus = "pushed";
        //}
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

            if (Globals.rocketStatus != "crashed")
            {
                OrbitManager.resetCounter++;
            }

            rocketRB.velocity = Vector3.zero;
            rocketRB.angularVelocity = Vector3.zero;
            Globals.Gravity = false;
            Globals.orbitTime = 0;
            Globals.orbit = "none";
            Globals.rocketStatus = "toLaunch";
            //Debug.Log(Globals.rocketStatus);
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
            }
            else
            {
                launcher.SetActive(false);
            }
            OrbitManager.RocketHasBeenPushed = false;
            //if (OrbitManager.showGoalAfterCrash)
            //{
            //    Goal.SetActive(true);
            //}
            //else
            //{
            //    Goal.SetActive(false);
            //}

            pushBTN.SetActive(false);
            resetBTN.SetActive(false);
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
        //Debug.Log("Eccentricity: " + Eccentricity);

    }

    //שיגורי דמו
    public void launchDemo()
    {
        if (Globals.ChosenSatellite.Orbit != "none")
        {
            resetRocket();

            Globals.demo = true;
            demoBTN.SetActive(false);
            demoTimer = 0.0f;
            //pushTime = 0.0f;
            //Debug.Log(Globals.demo);

            Globals.rocketStatus = "launching";
            //Debug.Log(Globals.rocketStatus);

            if (Globals.ChosenSatellite.Orbit == "LEO")
            {
                Globals.Gravity = true;
                Xforce = -92;
                Yforce = 40;
                pushAfter = 1;
                forceAmountonY = -18;
            }
            else if (Globals.ChosenSatellite.Orbit == "MEO")
            {
                Globals.Gravity = true;
                Xforce = -93;
                Yforce = 58;
                pushAfter = 1.7f;
                forceAmountonY = -23;

            }
            else if (Globals.ChosenSatellite.Orbit == "GEO")
            {
                Globals.Gravity = true;
                Xforce = -94;
                Yforce = 68;
                pushAfter = 2.8f;
                forceAmountonY = -24;
            }

            rocketRB.AddForce(Xforce, Yforce, 0);
        }
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

    //public void isRocketGoingToCrash()
    //{
    //    Vector3 curPos = rocket.transform.position;
    //    Vector3 curSpee = rocketRB.velocity;
    //    float timeFactor = 0.5f;
    //    float currentEarthToRocketDistance;
    //    float minimumEarthToRocketDistance = 0;
    //    Vector3 positionOfMinDistance = Vector3.zero;

    //    for (int i = 0; i < 10; i++)
    //    {
    //        Vector3 nextPosition = curPos + curSpee * timeFactor + 0.5f * Globals.GravityForce(earth, rocket, 0.5f) / Time.fixedDeltaTime * timeFactor * timeFactor;
    //        Vector3 nextSpeed = curSpee + Globals.GravityForce(earth, rocket, 0.5f) / Time.fixedDeltaTime * timeFactor;

    //        curPos= nextPosition;
    //        curSpee= nextSpeed;

    //        currentEarthToRocketDistance = (curPos-earth.transform.position).magnitude;

    //        if (currentEarthToRocketDistance <= minimumEarthToRocketDistance || i == 0)
    //        {
    //            minimumEarthToRocketDistance = currentEarthToRocketDistance;
    //            positionOfMinDistance = curPos;
    //            Debug.Log("i: " + i + " dis " + currentEarthToRocketDistance);
    //        }
    //    }

    //    distancetocrash.transform.position = positionOfMinDistance;

    //    if (minimumEarthToRocketDistance < 1.5f)
    //    {
    //        rocketIsGoingToCrash= true;
    //        Debug.Log("rocket is about to crash at "+ minimumEarthToRocketDistance);
    //    }
    //    else
    //    {
    //        rocketIsGoingToCrash= false;
    //        Debug.Log("rocket is NOT about to crash at " + minimumEarthToRocketDistance);

    //    }
    //}
}
