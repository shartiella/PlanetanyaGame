using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    //[SerializeField] private GameObject rocket;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem smoke;
    private Rigidbody rocketRB;

    public float yforce;
    public float firerate;
    public float smokerate;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.rocketStatus == "launching" || Globals.rocketStatus == "launched" || Globals.rocketStatus == "pushed" || Globals.rocketStatus == "inOrbit")
        {
            //float angle = Mathf.Atan2(rocket.GetComponent<Rigidbody>().velocity.y, rocket.GetComponent<Rigidbody>().velocity.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, angle);
            //rocket.transform.Rotate(0, 180, 90);

            rocketRB.AddForce(0, yforce, 0);

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
        Globals.rocketStatus = "launching";
        Debug.Log(Globals.rocketStatus);
        //Globals.Gravity = true;

        //Globals.launchForce=new Vector3(0, yforce, 0);
        //GetComponent<Rigidbody>().velocity = Globals.launchForce;
    }
}
