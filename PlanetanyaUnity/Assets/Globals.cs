using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    public static string orbit = "";
    public static float orbitTime = 0.0f;
    public static string rocketStatus = "toLaunch";
    //אפשרויות:
    //toLaunch מוכן לשיגור
    //launching בתהליך שיגור
    //launched שוגר - אבל עוד לא במסלול
    //pushed בתהליך דחיפה
    //crashed התרסק ועוד לא אותחל
    //inOrbit נכנס למסלול כלשהו
    public static bool Gravity = false;
    public static Vector3 launchForce= Vector3.zero;
    public static string correctOrbit = "none";
    public static Vector3 lastFingerRelease = new Vector3(0, (float)1.45, -2);
    public static int numberOfLaunches = 0;
    public static bool demo = false;
    public static Vector3 currentMousePosition;

    public List<Satellite> SatellitesList;
    public static Satellite ChosenSatellite;
    public static string ChosenSatelliteName;


    // Start is called before the first frame update
    void Start()
    {

        //זמני!!! בחירת לוויין
        foreach (Satellite satellite in SatellitesList)
        {
            if (satellite.Name == ChosenSatelliteName)
            {
                ChosenSatellite = satellite;
                Debug.Log("chosen satellite is "+ ChosenSatellite.Name);

            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //כוח המשיכה של כדור הארץ
    public static Vector3 GravityForce(GameObject attractor, GameObject target, float massProduct)
    {
        float G = 1;

        Vector3 difference = attractor.transform.position - target.transform.position;
        float distance = difference.magnitude;

        float unScaledForceMagnitude = massProduct / Mathf.Pow(distance, 2);
        float forceMagnitude = G * unScaledForceMagnitude;

        Vector3 forceDirection = difference.normalized;

        return forceDirection * forceMagnitude;
        //target.AddForce(forceVector);
    }

}

[System.Serializable]
public class Satellite
{
    public string Name;
    public string Kind;
    public string Orbit;

}

