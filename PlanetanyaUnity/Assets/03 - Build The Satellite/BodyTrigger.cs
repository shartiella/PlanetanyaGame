using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class BodyTrigger : MonoBehaviour
{
    public GameObject infoPanel;
    [SerializeField] private AllObjects _allObjects;
    public static SatPart currentPart;

    public static int connectedPartCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AllObjects.BuildingState == "building")
        {
            if (BuildIU.overallNumberOfCorrectParts == BuildIU.numberOfCorrectObjectsConnected && BuildIU.numberOfWrongObjectsConnected == 0)
            {
                AllObjects.BuildingState = "finished";

                if (BuildIU.counter >= 3)
                {
                    BuildIU.counter = 9;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        connectedPartCounter++;

        identifyPart(other.gameObject.name);
        //showInfo(currentPart.Description);
        currentPart.isConnected = true;
        AllObjects.connectedPart(currentPart);
    }

    void OnTriggerExit(Collider other)
    {
        identifyPart(other.gameObject.name);
        //infoPanel.SetActive(false);
        if (currentPart.isDragged)
        {
            currentPart.isConnected = false;
            AllObjects.disConnectedPart(currentPart);
            currentPart = null;
        }
    }

    void identifyPart(string touchingPartName)
    {
        foreach (SatPart sp in _allObjects.satParts)
        {
            if (sp.Name == touchingPartName)
            {
                currentPart = sp;
            }
        }
    }

    void showInfo(string description)
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = description;
    }
}
