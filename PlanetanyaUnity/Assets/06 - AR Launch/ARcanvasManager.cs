using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebXR;

public class ARcanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private bool showBtnInstWin;
    [SerializeField] private GameObject StoryWindow;
    [SerializeField] private bool showBtnStoryWin;

    [SerializeField] private GameObject InstructionBTN;
    [SerializeField] private GameObject StoryBTN;
    [SerializeField] private GameObject blackBG;
    [SerializeField] private GameObject arrivedBTN;

    public static int counter = 0;
    public TextMeshProUGUI countext;

    // Start is called before the first frame update
    void Start()
    {
        //InstructionTXT = InstructionWindow.GetComponentInChildren<TextMeshProUGUI>();
        //StoryTXT = StoryWindow.GetComponentInChildren<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        countext.text=counter.ToString();

        switch (counter)
        {
            case 0:
                showInstructionWindow("��� ���� �� ����, ��� ����� ���� ���������");
                arrivedBTN.SetActive(true);
                break;

            case 1:
                //����� ���� �� �����
                blackBG.SetActive(false);
                arrivedBTN.SetActive(false);
                if (WebXRManager.Instance.XRState == WebXRState.NORMAL)
                {
                    WebXRManager.Instance.ToggleAR();
                    Debug.Log("AR is ON");
                }
                else
                {
                    Debug.Log("no AR");
                }
                hideInstructionWindow();
                counter = 2;
                break;

            case 2:
                showInstructionWindow("���� �� ����");
                break;

            //case 0:
            //    Globals.rocketStatus = "lookingAround";
            //    showStoryWindow("��� ���� ����� �� ������� ���� ������!",true);
            //    break;

            //case 1:
            //    hideStoryWindow();
            //    advanceCounter();
            //    break;

            //case 2:
            //    Globals.rocketStatus = "connectSat";
            //    showInstructionWindow("���� �� ������� �� ����");
            //    break;

            //case 3:
            //    hideInstructionWindow();
            //    advanceCounter();
            //    break;

            //case 4:
            //    showInstructionWindow("���� �� ����");
            //    break;

            //case 5:
            //    hideInstructionWindow();
            //    advanceCounter();
            //    break;
        }
    }

    public void advanceCounter()
    {
        counter++;
        Debug.Log("COUNTER: " + counter);
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
        InstructionWindow.SetActive(true);
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
