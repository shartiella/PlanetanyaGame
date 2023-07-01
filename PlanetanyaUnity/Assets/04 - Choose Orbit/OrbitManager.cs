using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.WSA;

public class OrbitManager : MonoBehaviour
{
    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private bool showBtnInstWin;
    [SerializeField] private GameObject StoryWindow;
    //[SerializeField] private bool showBtnStoryWin;

    private TextMeshProUGUI InstructionTXT;
    [SerializeField] private GameObject InstructionBTN;
    private TextMeshProUGUI StoryTXT;
    [SerializeField] private GameObject StoryBTN;
    [SerializeField] private GameObject pushBTN;

    public static int counter = 0;
    public static bool showLauncherAfterCrash = false;

    [SerializeField] private GameObject launcher;
    [SerializeField] private GameObject antiLauncher;

    [SerializeField] private GameObject Orbits;
    [SerializeField] private GameObject OrbitsNames;

    public static bool crashFromEarthCollision=false;
    public static bool LaunchTowardsEarth = false;

    public static Vector3 lastFingerRelease;

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
                showStoryWindow("��� ������ ������ ����? \n ������ ���� ������� ���", true);
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

                if (LaunchTowardsEarth)
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
                    showStoryWindow("������ �� ���� �� ����� ������. ���� ����� ������� ����� ����, ��� ���� ��� �����.", true);
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
                showStoryWindow("�� �������� ������ �������, ���� ����� ����� ����. ��� ����?", true);
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
                showInstructionWindow("���� �� ���� ������ ����� ����� ���� ���� ����");
                SlideFromTop.exitAnimationTrigger = false;
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 21;
                }
                break;

            case 21:
                showStoryWindow("��� ����� �� ������ ������� ������ ����� �����, ���� ��� ���� ����� ��� ���� ������", true);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    pushBTN.SetActive(true);
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
                if (Rocket.Eccentricity > 0 && Rocket.Eccentricity < 0.2)
                {
                    pushBTN.SetActive(false);
                    hideInstructionWindow();
                    hideStoryWindow();
                    counter = 26;
                }
                break;

            case 26:
                showStoryWindow("�� ����� - ��� ������ ����� ���� ��� ������!", true);
                break;

            case 27:
                hideStoryWindow();
                Globals.rocketStatus = "crashed";
                showLauncherAfterCrash = false;
                counter = 28;
                break;

            case 28:
                showStoryWindow("���� ���� ���� �� ��� ������� ����� �� ��������",true);
                Orbits.SetActive(true);
                break;

            case 29:
                hideStoryWindow();
                typewriterUI.TextToType = "����\n������\n������������";
                OrbitsNames.SetActive(true);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 30;
                }
                break;

            case 30:
                showStoryWindow("��� ����� �������� ��������", true);
                StoryWinAnim.exitAnimationTrigger = false;
                break;

            case 31:
                hideStoryWindow();
                counter = 32;
                break;

            case 32:
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

            case 33:
                hideStoryWindow();
                counter = 34;
                break;

            case 34:
                launcher.SetActive(true);
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
                    counter = 35;
                }
                break;

            case 35:
                //����� ����� ��� ���� 2 ���������
                //����� ��� ���� ���� ������
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
}