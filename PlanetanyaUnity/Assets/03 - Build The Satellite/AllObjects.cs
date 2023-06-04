using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllObjects : MonoBehaviour
{
    public static string BuildingState = "";

    public List<SatPart> satParts; //התוכן מוגדר באינספקטור
    public static SatPart currentSatPart;
    public string satNameForCheck;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Globals.ChosenSatelliteName);
        //Globals.ChosenSatelliteName = satNameForCheck;

    }

    // Update is called once per frame
    void Update()
    {

    }

}

[System.Serializable]
public class SatPart
{
    public string Name;
    public string Description;
    public GameObject gObject;
    public List<string> relatedSatellites;
}
