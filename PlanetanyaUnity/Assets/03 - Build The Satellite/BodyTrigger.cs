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
    [SerializeField] private GameObject Fader;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AllObjects.BuildingState == "building")
        {
            if (BuildIUCopy.overallNumberOfCorrectParts == BuildIUCopy.numberOfCorrectObjectsConnected && BuildIUCopy.numberOfWrongObjectsConnected == 0)
            {
                AllObjects.BuildingState = "finished";

                //יהיה עוד משהו לפני זה?
                Fader.SetActive(true);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        identifyPart(other.gameObject.name);
        //showInfo(currentPart.Description);
        currentPart.isConnected = true;
        AllObjects.connectedPart(currentPart);
    }

    void OnTriggerExit(Collider other)
    {
        identifyPart(other.gameObject.name);
        //infoPanel.SetActive(false);
        currentPart.isConnected = false;
        AllObjects.disConnectedPart(currentPart);
        currentPart = null;
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
