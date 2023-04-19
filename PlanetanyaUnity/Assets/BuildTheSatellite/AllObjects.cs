using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllObjects : MonoBehaviour
{
    public GameObject InfoPanel;
    public static bool showInfoPanel = false;
    public static bool isTouchingSatBody = false;

    public List<SatPart> satParts;
    public static SatPart currentSatPart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (showInfoPanel)
        {
           //Debug.Log(showInfoPanel);
            InfoPanel.SetActive(true);
            InfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = currentSatPart.Description;
        }
        else
        {
            //Debug.Log(showInfoPanel);
            InfoPanel.SetActive(false);
        }
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
