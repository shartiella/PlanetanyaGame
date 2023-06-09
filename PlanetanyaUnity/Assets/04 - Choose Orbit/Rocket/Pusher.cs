using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] private Rigidbody rocket;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        float force = 0;
        Debug.Log(Globals.ChosenSatellite.Orbit);
        if (Globals.ChosenSatellite.Orbit == "LEO")
        {
            force = 0.5f;
        }
        else if (Globals.ChosenSatellite.Orbit == "MEO")
        {
            force = 0.4f;
        }
        else if (Globals.ChosenSatellite.Orbit == "GEO")
        {
            force = 0.3f;
        }
        Rocket.pushForce += force;
    }

    private void OnMouseUp()
    {
        Rocket.pushForce = 0;
    }
}
