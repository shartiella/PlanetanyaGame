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

    public static Satellite ChosenSatellite;
    public static SatPart ChosenSatPart;
    public List<Satellite> SatellitesList;
    [SerializeField] private GameObject SatDishGO;
    [SerializeField] private GameObject BananaGO;
    [SerializeField] private GameObject AntennaGO;
    [SerializeField] private GameObject SolarArrayGO;
    [SerializeField] private GameObject AtomicClockGO;
    [SerializeField] private GameObject ComputerGO;
    [SerializeField] private GameObject CameraGO;

    // Start is called before the first frame update
    void Start()
    {
        //הגדרת חלקי הלוויינים
        SatPart SatDish = new SatPart();
        SatDish.Name = SatDishGO.name;
        SatDish.Description = "תיאור על צלחת קליטה";

        SatPart Banana = new SatPart();
        Banana.Name = BananaGO.name;
        Banana.Description = "תיאור על בננה";

        SatPart Antenna = new SatPart();
        Antenna.Name = AntennaGO.name;
        Antenna.Description = "תיאור על אנטנה";

        SatPart SolarArray = new SatPart();
        SolarArray.Name = SolarArrayGO.name;
        SolarArray.Description = "תיאור על לוחות סולאריים";

        SatPart AtomicClock = new SatPart();
        AtomicClock.Name = AtomicClockGO.name;
        AtomicClock.Description = "תיאור על שעון אטומי";

        SatPart Computer = new SatPart();
        Computer.Name = ComputerGO.name;
        Computer.Description = "תיאור על מחשב";

        SatPart Camera=new SatPart();
        Camera.Name = CameraGO.name;
        Camera.Description = "תיאור על מצלמה";

        //רשימת לוויינים
        Satellite GPS= new Satellite();
        GPS.Name = "GPS";
        GPS.Kind = "Navigation";
        GPS.Orbit = "MEO";
        GPS.PartsList = new List<SatPart>
        {
            Antenna, Computer, AtomicClock, SolarArray
        };
        GPS.DistractorsList = new List<SatPart>
        {
            Banana, Camera, SatDish
        };

        Satellite TV = new Satellite();
        TV.Name = "TV";
        TV.Kind = "Communication";
        TV.Orbit = "GEO";
        TV.PartsList = new List<SatPart>
        {
            SolarArray, Computer, SatDish, Antenna
        };
        TV.DistractorsList = new List<SatPart>
        {
            Banana, Camera, AtomicClock
        };

        SatellitesList = new List<Satellite>
        {
            GPS, TV
        };

        Debug.Log("created list");

        //זמני!!! בחירת לוויין
        ChosenSatellite = GPS;

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
public class SatPart
{
    public string Name;
    public string Description;
}

[System.Serializable]
public class Satellite
{
    public string Name;
    public string Kind;
    public string Orbit;
    public List<SatPart> PartsList;
    public List<SatPart> DistractorsList;

}

