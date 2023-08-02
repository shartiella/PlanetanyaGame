using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    //public static int GuideCode;
    public static string GroupName="שם הקבוצה";
    public static string LevelStats1 = "";
    public static string LevelStats2 = "";
    public static string LevelStats3 = "";
    public static string LevelStats4 = "";
    public static string LevelStats5 = "";
    public static string LevelStats6 = "";

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
    public static Vector3 lastLaunchForce = Vector3.zero;

    //public static string correctOrbit = "none";
    //public static Vector3 lastFingerRelease = new Vector3(0, (float)1.45, -2);
    public static int numberOfLaunches = 0;
    public static bool demo = false;
    public static Vector3 currentMousePosition;

    public List<Satellite> SatellitesList;
    public static Satellite ChosenSatellite;
    public string ChosenSatelliteName = "";

    // Start is called before the first frame update
    void Start()
    {
        //GroupName = "";

        FillSatList();

        if (ChosenSatellite == null)
        {
            if (ChosenSatelliteName != "")
            {
                ChooseSat(ChosenSatelliteName);  //לבטל כשצריך
                ChosenSatelliteName = ChosenSatellite.Name;
                //Debug.Log("chosen satellite is " + ChosenSatellite.Name);
            }
            else
            {
                //Debug.Log("NO chosen satellite");
            }
        }
        else
        {
            //Debug.Log("chosen satellite is " + ChosenSatellite.Name);
        }
    }

    private void Awake()
    {

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

    //כוח המשיכה של כדור הארץ
    public static float CalculateCustomGravity(GameObject attractor, GameObject target, float massProduct)
    {
        float G = 1; // Gravitational constant

        Vector3 difference = attractor.transform.position - target.transform.position;
        float distance = difference.magnitude;

        float unScaledForceMagnitude = massProduct / Mathf.Pow(distance, 2);
        float forceMagnitude = G * unScaledForceMagnitude;

        return forceMagnitude;
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public void FillSatList()
    {
        Satellite GPS = new Satellite();
        GPS.Name = "GPS";
        GPS.Kind = "ניווט";
        GPS.Orbit = "MEO";
        GPS.Object = "Phone";
        SatellitesList.Add(GPS);

        Satellite TV = new Satellite();
        TV.Name = "TV";
        TV.Kind = "תקשורת";
        TV.Orbit = "GEO";
        TV.Object = "TV";
        SatellitesList.Add(TV);

        Satellite MAP = new Satellite();
        MAP.Name = "MAP";
        MAP.Kind = "מיפוי";
        MAP.Orbit = "LEO";
        MAP.Object = "Computer";
        SatellitesList.Add(MAP);
    }

    public void ChooseSat(string satName)
    {
        //תעבור על רשימת הלוויינים
        foreach (Satellite satellite in SatellitesList)
        {
            //תמצא את הלווין ששמו הוא שם הלוויין שנבחר
            if (satellite.Name == satName)
            {
                //תגדיר אותו בתור הלוויין שנבחר
                ChosenSatellite = satellite;
                //Debug.Log("chosen satellite is " + ChosenSatellite.Name);

            }
        }
    }
}

[System.Serializable]
public class Satellite
{
    public string Name; //שם
    public string Kind; //סוג
    public string Orbit; //מסלול
    public string Object; //אובייקט יומיומי שמייצג אותו
}

