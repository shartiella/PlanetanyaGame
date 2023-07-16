using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class Orbits : MonoBehaviour
{
    [SerializeField] private Transform rocket;
    [SerializeField] private Transform earth;

    [SerializeField] private GameObject GEO;
    [SerializeField] private GameObject MEO;
    [SerializeField] private GameObject LEO;

    [SerializeField] private Material GEOcolor;
    [SerializeField] private Material MEOcolor;
    [SerializeField] private Material LEOcolor;
    [SerializeField] private Material Correct;
    [SerializeField] private Material Wrong;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        IdentifyOrbit();
        ColorOrbit(Globals.orbit);

        if (Globals.rocketStatus == "toLaunch")
        {
            LEO.GetComponent<MeshRenderer>().material = LEOcolor;
            MEO.GetComponent<MeshRenderer>().material = MEOcolor;
            GEO.GetComponent<MeshRenderer>().material = GEOcolor;

        }
        //Vector3 EarthRocketVector = EarthTransform.position - rocketTransform.position;
        //float EarthRocketDistance = EarthRocketVec.magnitude;

        //Debug.Log("distance "+ EarthRocketDistance);
        //forceMeter.text = EarthRocketDistance.ToString();

        //if (EarthRocketDistance > 3 && EarthRocketDistance < 4.5)
        //{
        //    MEO++;

        //    if (MEO > 300)
        //    {
        //        orbit = "MEO";
        //        Debug.Log(orbit);
        //        meotransform.GetComponent<MeshRenderer>().material = rocketTransform.gameObject.GetComponent<MeshRenderer>().material;

        //        //Vector3 feedbackPosition = new Vector3(0, (float)823.45, 0);
        //        //GameObject feedbackPanel=Instantiate(feedbackPrefab,feedbackPosition, Quaternion.identity);
        //    }
        //}
    }

    //בדיקה האם הטיל נמצא במסלול מסוים
    public void IdentifyOrbit()
    {
        if (Globals.rocketStatus == "launched" || Globals.rocketStatus == "launching")
        {
            //מציאת המרחק בין הטיל לכדור הארץ
            //Vector3 EarthRocketVec = earth.position - rocket.position;
            //float EarthRocketDistance = EarthRocketVec.magnitude;


            //מיון המרחקים למסלולים
            if (Rocket.EarthRocketDistance < 1.9)
            {
                Globals.orbit = "ground";
                Globals.orbitTime = 0;
            }
            else if (Rocket.EarthRocketDistance > 2 && Rocket.EarthRocketDistance < 3)
            {
                Globals.orbit = "LEO";
            }
            else if (Rocket.EarthRocketDistance > 3.1 && Rocket.EarthRocketDistance < 4.2)
            {
                Globals.orbit = "MEO";
            }
            else if (Rocket.EarthRocketDistance > 4.3 && Rocket.EarthRocketDistance < 5.5)
            {
                Globals.orbit = "GEO";
            }
            else if (Rocket.EarthRocketDistance > 5.5)
            {
                Globals.orbit = "far";
                Globals.orbitTime = 0;
            }
            else
            {
                Globals.orbitTime = 0;
            }

            if (Rocket.EarthRocketDistance > 12)
            {
                //Globals.rocketStatus = "crashed";
            }

            //Debug.Log("distance " + EarthRocketDistance);
            //Debug.Log(Globals.orbit);
            //forceMeter.text = EarthRocketDistance.ToString();
        }

    }

    private void ColorOrbit(string orbit)
    {
        if (Globals.rocketStatus == "launched" || Globals.rocketStatus == "launching")
        {
            if (orbit == "MEO" && orbit == Globals.ChosenSatellite.Orbit)
            {
                MEO.GetComponent<MeshRenderer>().material = Correct;
            }
            else
            {
                MEO.GetComponent<MeshRenderer>().material = MEOcolor;
            }

            if (orbit == "LEO" && orbit == Globals.ChosenSatellite.Orbit)
            {
                LEO.GetComponent<MeshRenderer>().material = Correct;
            }
            else
            {
                LEO.GetComponent<MeshRenderer>().material = LEOcolor;
            }

            if (orbit == "GEO" && orbit == Globals.ChosenSatellite.Orbit)
            {
                GEO.GetComponent<MeshRenderer>().material = Correct;
            }
            else
            {
                GEO.GetComponent<MeshRenderer>().material = GEOcolor;
            }
        }

    }
}
