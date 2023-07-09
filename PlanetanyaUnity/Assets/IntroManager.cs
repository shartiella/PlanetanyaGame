using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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



    // Start is called before the first frame update
    void Start()
    {
        InstructionTXT = InstructionWindow.GetComponentInChildren<TextMeshProUGUI>();
        //InstructionBTN= InstructionWindow.GetComponentInChildren<GameObject>();
        StoryTXT = StoryWindow.GetComponentInChildren<TextMeshProUGUI>();
        //StoryBTN = InstructionWindow.GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (counter)
        {
            case 0:
                showStoryWindow("��� ���� ���� � ��� ����� ������",true);
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
                showInstructionWindow("��� ���� ���� ����� �� ���� ����� ���� ������� �����");
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
                 showStoryWindow("�� �� ������ �� ���� ����, ��� ���� ����� ������ ����. ��� ����?", true);
          
                break;
            case 7:
                hideStoryWindow();
                counter = 8;
                break;

            case 8:
          
                showStoryWindow ("�� �� �� �������� ��� ��������� ���� ��� ������, ���� ����� ������ ���� ���� ����", true);
                Satellite.SetActive(true);
                Moon.SetActive(false);
                break;
 

            case 9:
               
                hideStoryWindow();
                counter = 10;
                break;

            case 10:

                showStoryWindow("����, ������ ����� ���� �������� ��������� ������ ����� �������� ����� ������ ������� ����� ���� ������� ����", true);
                Satellites.SetActive(true);

                break;

            case 11:
                hideStoryWindow();
                break;

        }


    }

    

    public void advanceCounter()
    {
        counter++;
        Debug.Log(counter);
    }

    void showStoryWindow(string textContent, bool showBTN)
    {
        typewriterUI.TextToType = textContent;
        StoryWindow.SetActive(true);
        if (typewriterUI.TypeWriterIsFinished)
        {
            if (showBTN)
            {
               StoryBTN.SetActive(true);
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

    }
