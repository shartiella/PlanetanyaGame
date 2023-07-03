using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OrbitManager : MonoBehaviour
{
    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private bool showBtnInstWin;
    [SerializeField] private GameObject StoryWindow;
    //[SerializeField] private bool showBtnStoryWin;

    private TextMeshProUGUI InstructionTXT;
    [SerializeField] private GameObject InstructionBTN;
    [SerializeField] private TMP_Text StoryTXT;
    [SerializeField] private GameObject StoryBTN;
    [SerializeField] private GameObject pushBTN;

    public static int counter = 0;
    public static bool showLauncherAfterCrash = false;
    public static bool showResetAfterLaunch = false;

    [SerializeField] private GameObject launcher;
    [SerializeField] private GameObject antiLauncher;

    [SerializeField] private GameObject Orbits;
    [SerializeField] private GameObject OrbitsNames;
    [SerializeField] private GameObject resetBTN;

    [SerializeField] private GameObject demoBTN;

    public static bool crashFromEarthCollision=false;
    public static bool LaunchTowardsEarth = false;
    public static bool lastLaunchWasTowardsEarth = false;

    [SerializeField] private float neededOrbitTime=5;

    public static Vector3 lastFingerRelease;

    //float eccentricityPercent =0;
    string eccentricityFeedback = "";


    // Start is called before the first frame update
    void Start()
    {
        InstructionTXT = InstructionWindow.GetComponentInChildren<TextMeshProUGUI>();
        StoryTXT = StoryWindow.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (counter)
        {
            case 0:
                showStoryWindow("��� ������ ������ ����?\n������ ���� ������� ���", true);
                break;

            case 1:
                hideStoryWindow();
                counter = 2;
                break;

            case 2:
                if (StoryWindow.activeSelf == false)
                {
                    showStoryWindow("��� ���� �� ���� ������!", false);
                    counter = 3;
                }
                break;

            case 3:
                if (typewriterUI.TypeWriterIsFinished)
                {
                    showInstructionWindow("��� ����� �� ����� ������ ������, ���� �� ������ �����");
                    counter = 4;
                }
                break;

            case 4:
                if (typewriterUI.TypeWriterIsFinished)
                {
                    launcher.SetActive(true);
                    counter = 5;
                }
                break;

            case 5:
                if (Globals.rocketStatus == "launched")
                {
                    hideStoryWindow();
                    hideInstructionWindow();
                    counter = 6;
                }
                break;

            case 6:
                if (Globals.rocketStatus == "toLaunch" && Rocket.launchCounter == 1)
                {
                    showStoryWindow("������ ���� ���� ���� ������ �� ����� ������ �����. ����� ��� ���!",false);
                    antiLauncher.SetActive(true);
                    if (typewriterUI.TypeWriterIsFinished)
                    {
                        counter = 7;
                    }
                }
                break;

            case 7:
                showInstructionWindow("���� �� ���� ��� ������");
                if (typewriterUI.TypeWriterIsFinished)
                {
                    launcher.SetActive(true);
                    counter = 8;
                }
                break;

            case 8:
                if (Globals.rocketStatus == "toLaunch" && Rocket.launchCounter == 2)
                {
                    hideStoryWindow();
                    counter = 9;
                }
                break;

            case 9:
                showStoryWindow("��� ������ ���� ������ ������ ���� ���� ����...", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    launcher.SetActive(true);
                    counter = 10;
                }
                break;

            case 10:
                if (Globals.rocketStatus == "toLaunch" && Rocket.launchCounter == 3)
                {
                    hideStoryWindow();
                    counter = 11;
                }
                break;

            case 11:
                showStoryWindow("���� ���� ���� ������ ������ � �� ����� ����, ��� ����� ����", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    hideInstructionWindow();
                    counter = 12;
                }
                break;

            case 12:
                showInstructionWindow("���� �� ���� ������ �������");
                if (typewriterUI.TypeWriterIsFinished)
                {
                    launcher.SetActive(true);
                    counter = 13;
                }
                break;

            case 13:
                if (Globals.rocketStatus == "crashed" && Rocket.launchCounter > 3)
                {
                    hideStoryWindow();
                    counter = 14;
                }
                break;

            case 14: //���� �� ����� ������
                launcher.SetActive(true);

                if (lastLaunchWasTowardsEarth)
                {
                    //���� �����
                    showStoryWindow("���� ����� ����� ����� ���� ����� �� �� ���� �������...", false);
                }
                else if (Globals.lastLaunchForce.magnitude <= 4)
                {
                    //��� ����
                    showStoryWindow("������ �� ���� �� ����� ����� ����. ��� ������ �� ���� ���� ��� ���� ����� �����.", false);
                }
                else if (Globals.lastLaunchForce.magnitude >= 5)
                {
                    //��� ����
                    if (crashFromEarthCollision)
                    {
                        showStoryWindow("������ �� ���� �� ����� ���� ����. ���� ����� ������� ����� ����, ��� ���� ��� �����.", false);
                    }
                    else
                    {
                        showStoryWindow("������ �� ���� �� ����� ���� ����. ���� ����� �� ��� ������ �� ���� ����, ��� ����.", false);
                    }
                }
                else
                {
                    //������
                    hideInstructionWindow();
                    showStoryWindow("�� �����! ������ �� ���� ������ ������.", true);
                }

                if (typewriterUI.TypeWriterIsFinished)
                {
                    if (Globals.rocketStatus == "crashed" && Rocket.launchCounter > 4)
                    {
                        counter = 13;
                    }
                }
                else
                {
                    if (Globals.rocketStatus == "launched" && Rocket.launchCounter > 4)
                    {
                        counter = 13;
                    }
                }

                break;

            case 15:
                showLauncherAfterCrash = true;
                hideStoryWindow();
                counter = 16;
                break;

            case 16:
                showStoryWindow("���, �� ����� - ���� ����� ����� ����. ��� ����?", true);
                break;

            case 17:
                hideStoryWindow();
                counter = 18;
                break;

            case 18:
                showStoryWindow("������ ����� �� ���� ������ ����� ������ ������ ���� ���� ������ ��� ����� � ���� ����", true);
                break;

            case 19:
                hideStoryWindow();
                counter = 20;
                break;

            case 20:
                showStoryWindow("��� ����� �� ������ ������� ������ ����� �����, ���� ��� ���� ����� ��� ���� ������", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 21;
                }
                break;

            case 21:
                showInstructionWindow("������ �� ���� ������ ����� ����� ����� �����");
                SlideFromTop.exitAnimationTrigger = false;
                if (typewriterUI.TypeWriterIsFinished && Globals.rocketStatus=="pushed")
                {
                    counter = 22;
                    showResetAfterLaunch = true;
                }
                break;

            case 22:
                hideStoryWindow();
                counter = 23;
                break;

            case 23:
                showStoryWindow("����� ���� ����� ����� �� ������ �� ���� ����� ���� ������ ������ ������", true);
                break;

            case 24:
                hideStoryWindow();
                counter = 25;
                break;

            case 25:
                showStoryWindow("�� ������ ���� ��� ������ ���� ������ ������ ����� �� ������� ����� ����", false);
                if (typewriterUI.TypeWriterIsFinished && Globals.rocketStatus == "pushed")
                {
                    counter= 26;
                }
                break;

            case 26:
                hideStoryWindow();
                counter= 27;
                break;

                case 27:
                //eccentricityPercent = Mathf.Round((1 - Rocket.Eccentricity) * 10);
                //eccentricityFeedback = "������ ���� ����� �-%0" + eccentricityPercent.ToString();
                if (Rocket.Eccentricity > 1)
                {
                    eccentricityFeedback = "���� ���� �� ������ ����";
                }
                else if (Rocket.Eccentricity > 0.15)
                {
                    eccentricityFeedback = "���� ���� ������ ������ ����";
                }
                else
                {
                    pushBTN.SetActive(false);
                    resetBTN.SetActive(false);
                    hideInstructionWindow();
                    hideStoryWindow();
                    counter = 29;
                }
                showStoryWindow(eccentricityFeedback, false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 28;
                }
                break;

            case 28:
                //eccentricityPercent = Mathf.Round((1 - Rocket.Eccentricity) * 10);
                //eccentricityFeedback = "������ ���� ����� �-%0" + eccentricityPercent.ToString();

                if (Rocket.Eccentricity > 1)
                {
                    eccentricityFeedback = "���� ���� �� ������ ����";
                }
                else if (Rocket.Eccentricity > 0.15)
                {
                    eccentricityFeedback = "���� ���� ������ ������ ����";
                }
                else
                {
                    pushBTN.SetActive(false);
                    resetBTN.SetActive(false);
                    hideInstructionWindow();
                    hideStoryWindow();
                    counter = 29;
                }

                StoryWindow.GetComponentInChildren<TextMeshProUGUI>().text = eccentricityFeedback;
                break;

            case 29:
                showStoryWindow("�� ����� - ��� ������ ����� ���� ��� ������!", true);
                break;

            case 30:
                hideStoryWindow();
                Globals.rocketStatus = "crashed";
                showLauncherAfterCrash = false;
                counter = 31;
                break;

            case 31:
                showStoryWindow("���� ���� ���� �� ��� ������� ����� �� ��������", true);
                Orbits.SetActive(true);
                break;

            case 32:
                hideStoryWindow();
                typewriterUI.TextToType = "����\n������\n������������";
                OrbitsNames.SetActive(true);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 33;
                }
                break;

            case 33:
                showStoryWindow("��� ����� �������� ��������", true);
                StoryWinAnim.exitAnimationTrigger = false;
                break;

            case 34:
                hideStoryWindow();
                counter = 35;
                break;

            case 35:
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("������ ������ ������ ���� ����� �� ���� ���� �����, ����...", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("������ ������ ������ ���� ����� �� ���� ���� ������ ����, ����...", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("������ ������� ������ ���� ����� ������� �� ����� ���� ����, ����...", true);
                }
                break;

            case 36:
                hideStoryWindow();
                counter = 37;
                break;

            case 37:
                launcher.SetActive(true);
                showLauncherAfterCrash = true;
                Rocket.launchCounter = 0;
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showInstructionWindow("���� �� ���� ������ ������� ����");
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showInstructionWindow("���� �� ���� ������ ������� ������");
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showInstructionWindow("���� �� ���� ������ ������� ������������");
                }
                SlideFromTop.exitAnimationTrigger = false;
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 38;
                }
                break;

            case 38:
                if (Globals.rocketStatus == "crashed")
                {
                    OrbitsNames.SetActive(false);
                }
                if (Globals.rocketStatus == "crashed" && Rocket.launchCounter >= 2)
                {
                    if (!Globals.demo)
                    {
                        demoBTN.SetActive(true);
                    }
                    else
                    {
                        demoBTN.SetActive(false);
                    }
                }
                if (Globals.rocketStatus == "pushed")
                {
                    pushBTN.SetActive(false);
                }
                if (checkOrbit())
                {
                    counter = 39;
                }
                break;

            case 39:
                hideInstructionWindow();
                hideStoryWindow();
                demoBTN.SetActive(false);
                Orbits.SetActive(false);
                resetBTN.SetActive(false);
                counter = 40;
                break;

            case 40:
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("�� �����! ������ ���� ��� ������ ������� ����", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("�� �����! ������ ���� ��� ������ ������� ������", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("�� �����! ������ ���� ��� ������ ������� ������������", true);
                }
                break;

            case 41:
                hideStoryWindow();
                counter = 42;
                break;

            case 42:
                showStoryWindow("����� ���� ������ ������ ������ ���� ����, ���� ���� ���� �� ������� ���� ����!", true);

                break;
        }


    }

    public void advanceCounter()
    {
        counter++;
        Debug.Log(counter);
    }

    void showStoryWindow(string textContent, bool showBtn)
    {
        typewriterUI.TextToType = textContent;
        StoryBTN.SetActive(false);
        StoryWindow.SetActive(true);
        if (typewriterUI.TypeWriterIsFinished)
        {
            if (showBtn)
            {
                StoryBTN.SetActive(true);
            }
            //if (showLauncher)
            //{
            //    Debug.Log("showLauncher");
            //    launcher.SetActive(true);
            //}
        }
    }

    void hideStoryWindow()
    {
        if (!StoryWinAnim.exitAnimationTrigger)
        {
            if (!StoryWinAnim.activeAnimation)
            {
                StoryWinAnim.exitAnimationTrigger = true;
            }
        }
    }

    void showInstructionWindow(string textContent)
    {
        typewriterUI.TextToType = textContent;
        //InstructionBTN.SetActive(false);
        InstructionWindow.SetActive(true);
        //if (showBtnInstWin)
        //{
        //    if (typewriterUI.TypeWriterIsFinished)
        //    {
        //        InstructionBTN.SetActive(true);
        //    }
        //}
    }

    void hideInstructionWindow()
    {
        if (!SlideFromTop.exitAnimationTrigger)
        {
            if (!SlideFromTop.activeAnimation)
            {
                SlideFromTop.exitAnimationTrigger = true;
            }
        }
    }

    private bool checkOrbit()
    {
        if (Globals.rocketStatus == "launched" || Globals.rocketStatus == "launching" || Globals.rocketStatus == "pushed")
        {
            //����� �����
            if (Globals.orbit == Globals.ChosenSatellite.Orbit && Globals.demo == false)
            {
                Globals.orbitTime += Time.deltaTime;
                Debug.Log(" :����� ������ ���" + Environment.NewLine + Math.Round(Globals.orbitTime, 2).ToString());

                if (Globals.orbitTime >= neededOrbitTime)
                {
                    //winPanel.SetActive(true);
                    //winPanel.GetComponentInChildren<TextMeshProUGUI>().text = "!������" + Environment.NewLine + "������ �����" + Environment.NewLine + "!" + Globals.ChosenSatellite.Orbit;
                    //meter.text = "";
                    Globals.rocketStatus = "inOrbit";

                    return true;
                    //demoBtn.gameObject.SetActive(false);
                    //resetBtn.gameObject.SetActive(false);
                    //pushBtn.gameObject.SetActive(false);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //meter.text = "";
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}