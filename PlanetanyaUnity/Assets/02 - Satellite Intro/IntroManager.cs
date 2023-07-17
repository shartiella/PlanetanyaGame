using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private bool showBtnInstWin;
    [SerializeField] private GameObject StoryWindow;
    [SerializeField] private bool showBtnStoryWin;
    private TextMeshProUGUI InstructionTXT;
    [SerializeField] private GameObject InstructionBTN;
    private TextMeshProUGUI StoryTXT;
    [SerializeField] private GameObject StoryBTN;
    public static int counter = 0;
    [SerializeField] private GameObject Moon;
    [SerializeField] private GameObject Satellites;
    [SerializeField] private GameObject Satellite;

    [SerializeField] private Camera cam;
    private int cameraMoveCounter = 0;

    float totalTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        InstructionTXT = InstructionWindow.GetComponentInChildren<TextMeshProUGUI>();
        //InstructionBTN= InstructionWindow.GetComponentInChildren<GameObject>();
        StoryTXT = StoryWindow.GetComponentInChildren<TextMeshProUGUI>();
        //StoryBTN = InstructionWindow.GetComponentInChildren<GameObject>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        totalTime += Time.deltaTime;

        switch (counter)
        {
            case 0:
                moveCameraAtTheStart();
                showStoryWindow("��� ���� ���� � ��� ����� ������", true);
                break;

            case 1:
                hideStoryWindow();
                counter = 2;
                break;

            case 2:
                showStoryWindow("�� ���� ���� ���� ����", true);
                Moon.SetActive(true);
                break;

            case 3:
                hideStoryWindow();
                counter = 4;
                break;

            case 4:
                showInstructionWindow("��� ���� ��� ����� ������� �����");
                showBtnInstWin = false;
                BlinkColor.glowOn = true;
                if (typewriterUI.TypeWriterIsFinished)
                {
                    showStoryWindow("�� ������ ������� �� ���� ���� ������ ��������. ���?", true);
                    Moon.SetActive(true);
                }
                break;

            case 5:
                hideInstructionWindow();
                advanceCounter();
                hideStoryWindow();
                counter = 6;

                break;

            case 6:
                showStoryWindow("�� �� ������ �� ���� ����, ��� ���� ����� ������ ����. ��� ''����''?", true);
                break;

            case 7:
                hideStoryWindow();
                counter = 8;
                break;

            case 8:
                showStoryWindow("�� �� �� �������� ��� ��������� ���� ��� ������, ���� ����� ������ ���� ���� ����", true);
                Satellite.SetActive(true);
                Moon.SetActive(false);
                break;


            case 9:
                hideStoryWindow();
                counter = 10;
                break;

            case 10:
                showStoryWindow("����, ������ ����� ���� �������� ��������� ������ ����� ������ ������� ����� ���� ������� ����", true);
                Satellites.SetActive(true);
                Satellite.SetActive(false);
                break;

            case 11:
                hideStoryWindow();
                counter = 12;
                break;

            case 12:
                if (Globals.ChosenSatellite.Name == "GPS")
                {
                    showStoryWindow("����, ������ ���� ���� �� ������ ���� ����� ������� �����", true);
                }
                else if (Globals.ChosenSatellite.Name == "TV")
                {
                    showStoryWindow("����, ��������� ���� ����� �� ������ ����� ������� ������", true);
                }
                else if (Globals.ChosenSatellite.Name == "MAP")
                {
                    showStoryWindow("����, ����� ������ ������ ��� ����� ����� ������� �����", true);
                }
                break;

            case 13:
                hideStoryWindow();
                hideInstructionWindow();
                if (!StoryWindow.activeSelf && !InstructionWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    SlideFromTop.exitAnimationTrigger = false;
                    counter = 14;
                }
                break;

            case 14:
                Globals.LevelStats2 += " ��� ����: " + Globals.Reverse(Mathf.RoundToInt(totalTime).ToString()) + " �����";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }


    }



    public void advanceCounter()
    {
        counter++;
        //Debug.Log(counter);
    }

    void showStoryWindow(string textContent, bool showBTN)
    {
        if (!StoryWindow.activeSelf)
        {
            typewriterUI.TextToType = textContent;
            StoryWindow.SetActive(true);
        }
        else
        {
            if (typewriterUI.TypeWriterIsFinished)
            {
                if (showBTN)
                {
                    StoryBTN.SetActive(true);
                }
            }
        }
    }

    void hideStoryWindow()
    {
        if (!StoryWinAnim.exitAnimationTrigger)
        {
            if (!StoryWinAnim.activeAnimation)
            {
                StoryBTN.SetActive(false);

                StoryWinAnim.exitAnimationTrigger = true;

            }
        }
    }


    void showInstructionWindow(string textContent)
    {
        typewriterUI.TextToType = textContent;
        InstructionBTN.SetActive(false);
        InstructionWindow.SetActive(true);
        if (showBtnInstWin)
        {
            if (typewriterUI.TypeWriterIsFinished)
            {
                InstructionBTN.SetActive(true);
            }
        }
    }

    void hideInstructionWindow()
    {
        SlideFromTop.exitAnimationTrigger = true;
    }

    void moveCameraAtTheStart()
    {
        cameraMoveCounter++;

        if (cameraMoveCounter==1)
        {
            Vector3 firstStaticCameraPosition = cam.transform.localPosition;
            Vector3 otherPosition = new Vector3(0, 0, -21);
            cam.transform.localPosition = otherPosition;
            cam.transform.LeanMoveLocal(firstStaticCameraPosition, 3).setDelay(1).setEaseInOutSine();
        }
        else
        {
            cam.GetComponent<CameraRotate>().enabled = true;
        }

    }

}
