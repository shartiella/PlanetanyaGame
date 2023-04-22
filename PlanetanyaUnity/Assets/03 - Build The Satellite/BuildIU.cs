using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class BuildIU : MonoBehaviour
{
    //פקדי UI
    [SerializeField] private GameObject doneBTN;
    [SerializeField] private GameObject feedbackWindow;
    public GameObject InfoPanel;
    public static bool showInfoPanel = false;
    public static bool isTouchingSatBody = false;
    [SerializeField] private GameObject guideInput;
    [SerializeField] private Material correctColor;
    [SerializeField] private Material wrongColor;
    [SerializeField] private AllObjects _allObjects;
    [SerializeField] private GameObject introWindow;
    [SerializeField] private GameObject endWindow;


    // Start is called before the first frame update
    void Start()
    {
        introWindow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (showInfoPanel)
        {
            //Debug.Log(showInfoPanel);
            InfoPanel.SetActive(true);
            InfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = AllObjects.currentSatPart.Description;
        }
        else
        {
            //Debug.Log(showInfoPanel);
            InfoPanel.SetActive(false);
        }
    }
    public void beginBuilding()
    {
        introWindow.SetActive(false);
        AllObjects.BuildingState = "building";
    }

    public void DoneBuilding()
    {
        AllObjects.BuildingState = "checking";
        feedbackWindow.SetActive(true);
        doneBTN.SetActive(false);
    }

    public void returnToBuilding()
    {
        AllObjects.BuildingState = "building";
        feedbackWindow.SetActive(false);
        guideInput.SetActive(false);
        endWindow.SetActive(false);

    }
    public void openGuideInput()
    {
        feedbackWindow.SetActive(false);
        guideInput.SetActive(true);
    }
    public void showFeedbackForGuide()
    {
        guideInput.SetActive(false);
        AllObjects.BuildingState = "feedback";
        endWindow.SetActive(true);

    }
    public void continueToNextLevel()
    {
        endWindow.SetActive(false);
        AllObjects.BuildingState = "finished";

        //יהיה עוד משהו לפני זה
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
