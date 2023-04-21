using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllObjects : MonoBehaviour
{
    public static string BuildingState = "";

    public List<SatPart> satParts;
    public static SatPart currentSatPart;

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
        //GetComponent<Renderer>().material = holdingFeedback; //צביעת העיגול בצהוב

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
