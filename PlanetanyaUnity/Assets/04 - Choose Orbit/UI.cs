using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    [SerializeField] private float neededOrbitTime;

    [SerializeField] private TextMeshProUGUI meter;

    [SerializeField] private Rigidbody rocketRB;

    private float demoTimer = 0.0f;
    private int pushTimes=0;

    //לבטל כשנדע מה טוב
    [SerializeField] private float Xforce;
    [SerializeField] private float Yforce;
    [SerializeField] private float pushAfter;
    [SerializeField] private float forceAmountonY;

    //פקדי UI
    [SerializeField] private Button demoBtn;
    [SerializeField] private TMP_Dropdown dropdownO;
    [SerializeField] private GameObject pushBtn;
    [SerializeField] private Button resetBtn;

    // Start is called before the first frame update
    void Start()
    {
        //Globals.correctOrbit = "MEO";
        Debug.Log("correct Orbit " + Globals.ChosenSatellite.Orbit);

    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.rocketStatus == "launched" || Globals.rocketStatus == "launching" || Globals.rocketStatus == "pushed")
        {
            //בדיקת מסלול
            if (Globals.orbit == Globals.ChosenSatellite.Orbit && Globals.demo == false)
            {
                Globals.orbitTime += Time.deltaTime;
                meter.text = " :ןוכנה לולסמב ןמז" + Environment.NewLine + Math.Round(Globals.orbitTime, 2).ToString();

                if (Globals.orbitTime >= neededOrbitTime)
                {
                    winPanel.SetActive(true);
                    winPanel.GetComponentInChildren<TextMeshProUGUI>().text = "!םתחלצה" + Environment.NewLine + "לולסמל םתעגה" + Environment.NewLine + "!" + Globals.ChosenSatellite.Orbit;
                    meter.text = "";
                    Globals.rocketStatus = "inOrbit";

                    demoBtn.gameObject.SetActive(false);
                    resetBtn.gameObject.SetActive(false);
                    pushBtn.gameObject.SetActive(false);
                    //dropdownO.gameObject.SetActive(false);
                }
            }
            else
            {
                meter.text = "";
            }

        }

        //דמו


        if (Globals.demo)
        {

            demoTimer += Time.deltaTime;
            //pushTime += Time.deltaTime;
            //Debug.Log("demo timer " + demoTimer);

            if (demoTimer > pushAfter)
            {
                if (pushTimes == 0)
                {
                    Globals.rocketStatus = "pushed";
                    rocketRB.AddForce(0, forceAmountonY, 0);
                    pushTimes++;
                }
                demoTimer = 0.0f;
            }
        }
        else
        {
            demoTimer = 0.0f;
            pushTimes=0;
        }

        if (Globals.ChosenSatellite.Orbit == "none")
        {
            demoBtn.gameObject.SetActive(false);
            resetBtn.gameObject.SetActive(false);
            pushBtn.gameObject.SetActive(false);
            //dropdownO.gameObject.SetActive(true);
            //dropdownO.enabled = true;
        }
        else if (Globals.demo)
        {
            demoBtn.gameObject.SetActive(false);
            resetBtn.gameObject.SetActive(true);
            pushBtn.gameObject.SetActive(false);
            //dropdownO.gameObject.SetActive(false);
        }
        else if (Globals.rocketStatus == "toLaunch")
        {
            demoBtn.gameObject.SetActive(true);
            resetBtn.gameObject.SetActive(false);
            pushBtn.gameObject.SetActive(false);
            //dropdownO.gameObject.SetActive(true);
            //dropdownO.enabled = true;

        }
        else if (Globals.rocketStatus == "launched" || Globals.rocketStatus == "launching" || Globals.rocketStatus == "pushed")
        {
            demoBtn.gameObject.SetActive(false);
            resetBtn.gameObject.SetActive(true);
            pushBtn.gameObject.SetActive(true);
            //dropdownO.gameObject.SetActive(true);
            //dropdownO.enabled = false;
        }
    }

    //אתחול השיגור
    public void RestartGame()
    {
        Globals.rocketStatus = "restart";
        //demoTimer = 0.0f;
        //pushTime = 0.0f;
        //Globals.demo = false;
        //Globals.ChosenSatellite.Orbit = "none";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //מעבר לסצנה הבאה
    public void nextScene()
    {
        Globals.rocketStatus = "restart";
        //demoTimer = 0.0f;
        //pushTime = 0.0f;
        //Globals.demo = false;
        //Globals.ChosenSatellite.Orbit = "none";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //שיגורי דמו
    public void launchTest()
    {
        if (Globals.ChosenSatellite.Orbit != "none")
        {
            Globals.demo = true;
            demoTimer = 0.0f;
            //pushTime = 0.0f;
            Debug.Log(Globals.demo);

            Globals.rocketStatus = "launching";
            Debug.Log(Globals.rocketStatus);

            if (Globals.ChosenSatellite.Orbit == "LEO")
            {
                Globals.Gravity = true;
                Xforce = -92;
                Yforce = 40;
                pushAfter = 1;
                forceAmountonY = -18;
            }
            else if (Globals.ChosenSatellite.Orbit == "MEO")
            {
                Globals.Gravity = true;
                Xforce = -93;
                Yforce = 58;
                pushAfter = 1.7f;
                forceAmountonY = -23;

            }
            else if (Globals.ChosenSatellite.Orbit == "GEO")
            {
                Globals.Gravity = true;
                Xforce = -94;
                Yforce = 68;
                pushAfter = 2.8f;
                forceAmountonY = -24;
            }

            rocketRB.AddForce(Xforce, Yforce, 0);
        }
       
           


        //if (Globals.rocketStatus == "toLaunch")
        //{
        //    Globals.rocketStatus = "launching";
        //    Debug.Log(Globals.rocketStatus);


        //    //rocketRigidbody.transform.Rotate(0, 0, -30);
        //     //launch for MEO: 100,50,0

        //}
    }
}
