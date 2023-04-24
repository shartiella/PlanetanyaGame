using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class LaunchToSpace : MonoBehaviour
{
    [SerializeField] private Rigidbody rocketRB;

    private float timerToLaunch = 0.0f;
    private float demoTimer = 0.0f;
    private int pushTimes = 0;

    //לבטל כשנדע מה טוב
    [SerializeField] private float Xforce;
    [SerializeField] private float Yforce;
    [SerializeField] private float pushAfter;
    [SerializeField] private float forceAmountonY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timerToLaunch < 1 && timerToLaunch>-1)
        {
            timerToLaunch += Time.deltaTime;
            Debug.Log(timerToLaunch);
        }
        if (timerToLaunch > 1)
        {
            Globals.correctOrbit = "MEO";
            launchTest();
            timerToLaunch = -2;
        }

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
            pushTimes = 0;
        }
    }

    public void nextScene()
    {
        Globals.rocketStatus = "restart";
        //demoTimer = 0.0f;
        //pushTime = 0.0f;
        //Globals.demo = false;
        Globals.correctOrbit = "none";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //שיגורי דמו
    public void launchTest()
    {
        if (Globals.correctOrbit != "none")
        {
            Globals.demo = true;
            demoTimer = 0.0f;
            //pushTime = 0.0f;
            Debug.Log(Globals.demo);

            Globals.rocketStatus = "launching";
            Debug.Log(Globals.rocketStatus);

            if (Globals.correctOrbit == "LEO")
            {
                Globals.Gravity = true;
                Xforce = -92;
                Yforce = 40;
                pushAfter = 1;
                forceAmountonY = -18;
            }
            else if (Globals.correctOrbit == "MEO")
            {
                Globals.Gravity = true;
                Xforce = -93;
                Yforce = 58;
                pushAfter = 1.7f;
                forceAmountonY = -23;

            }
            else if (Globals.correctOrbit == "GEO")
            {
                Globals.Gravity = true;
                Xforce = -94;
                Yforce = 68;
                pushAfter = 2.8f;
                forceAmountonY = -24;
            }
            rocketRB.AddForce(Xforce, Yforce, 0);
        }
    }
}
