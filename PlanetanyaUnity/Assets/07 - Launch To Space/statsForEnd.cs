
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class statsForEnd : MonoBehaviour
{
    public TextMeshProUGUI groupName;
    public TextMeshProUGUI deviceName;
    public TextMeshProUGUI satKind;
    public TextMeshProUGUI orbitName;
    public TextMeshProUGUI connectionNum;
    public TextMeshProUGUI launchesNum;
    public TextMeshProUGUI totalTime;

    [SerializeField] private Transform initialPositionT;
    private Vector3 initialPosition;
    [SerializeField] private Transform otherPositionT;
    private Vector3 otherPosition;
    int counter=0;
    string timeStg = "00:00";
    float curNum = 0;
    float curmin = 0;
    float cursec = 0;

    [SerializeField] private GameObject tryagain;
    [SerializeField] private GameObject restartBTN;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = initialPositionT.localPosition;
        otherPosition = otherPositionT.localPosition;
        initialPositionT.localPosition = otherPosition;

        //groupName.text = Globals.GroupName;
        //deviceName.text = Globals.ChosenSatellite.ObjectInHebrew;
        //satKind.text = Globals.ChosenSatellite.Kind;
        //orbitName.text = Globals.ChosenSatellite.OrbitInHebrew;
        //connectionNum.text = Globals.connectionNum.ToString();
        //launchesNum.text = Globals.launchesNum.ToString();
        //totalTime.text = Globals.totalGameTime.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (counter)
        {
            case 1:
                typeStat(groupName,Globals.GroupName);
                break;

            case 2:
                typeStat(deviceName, Globals.ChosenSatellite.ObjectInHebrew);
                break;

            case 3:
                typeStat(satKind, Globals.ChosenSatellite.Kind);
                break;

            case 4:
                typeStat(orbitName, Globals.ChosenSatellite.OrbitInHebrew);
                break;

            case 5:
                typeNum(connectionNum, Globals.connectionNum);
                break;

            case 6:
                typeNum(launchesNum, Globals.launchesNum);
                break;

            case 7:
                typeTime(totalTime, Globals.totalGameTime);
                break;

                case 8:
                typewriterUI.TextToType = tryagain.GetComponent<TextMeshProUGUI>().text;
                tryagain.SetActive(true);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 9;
                }
                break;

            case 9:
                restartBTN.SetActive(true);
                break;
        }
    }

    private void OnEnable()
    {
        transform.LeanMoveLocal(initialPosition, 0.5f).setDelay(0).setEaseOutBack().setOnComplete(startFillingIn);
    }

    void startFillingIn()
    {
        counter = 1;
    }

    void typeStat(TextMeshProUGUI statTMP, string textContent)
    {
        if (!statTMP.gameObject.activeSelf)
        {
            typewriterUI.TextToType = textContent;
            statTMP.gameObject.SetActive(true);
        }

        if (statTMP.gameObject.activeSelf && typewriterUI.TypeWriterIsFinished)
        {
            counter++;
        }
    }

    void typeNum(TextMeshProUGUI statTMP, int numberToShow)
    {
        if (!statTMP.gameObject.activeSelf)
        {
            statTMP.gameObject.SetActive(true);
            curNum = 0; 
        }
        else
        {
            curNum += 0.2f;
            statTMP.text = Mathf.FloorToInt(curNum).ToString();
        }

        if (curNum >= numberToShow)
        {
            curNum = 0;
            counter++;
        }
    }

    void typeTime(TextMeshProUGUI statTMP, float numberToShow)
    {
        float timeMins = Mathf.FloorToInt(numberToShow / 60);
        float timeSecs = numberToShow % 60;

        if (!statTMP.gameObject.activeSelf)
        {
            statTMP.gameObject.SetActive(true);
            timeStg = "00:00";
            //Debug.Log("numberToShow: " + numberToShow + " = " + (timeMins * 60 + timeSecs));
        }
        else
        {
            timeStg = "";

            if (curmin < timeMins)
            {
                curmin += 0.2f;
            }
            if (curmin < 10)
            {
                timeStg += "0" + Mathf.FloorToInt(curmin);
            }
            else
            {
                timeStg += Mathf.FloorToInt(curmin);
            }

            if (cursec < timeSecs)
            {
                cursec += 0.2f;
            }
            if (cursec < 10)
            {
                timeStg += ":0" + Mathf.FloorToInt(cursec);
            }
            else
            {
                timeStg += ":" + Mathf.FloorToInt(cursec);
            }
        }

        statTMP.text = timeStg;

        if (curmin >= timeMins && cursec >= timeSecs)
        {
            counter++;
        }
    }
}
