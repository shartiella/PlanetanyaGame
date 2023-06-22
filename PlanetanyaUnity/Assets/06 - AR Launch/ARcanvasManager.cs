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

    private TextMeshProUGUI InstructionTXT;
    [SerializeField] private GameObject InstructionBTN;
    private TextMeshProUGUI StoryTXT;
    [SerializeField] private GameObject StoryBTN;

    public static int counter = 0;
    public TextMeshProUGUI countext;

    // Start is called before the first frame update
    void Start()
    {
        InstructionTXT = InstructionWindow.GetComponentInChildren<TextMeshProUGUI>();
        StoryTXT = StoryWindow.GetComponentInChildren<TextMeshProUGUI>();

        if (WebXRManager.Instance.XRState == WebXRState.NORMAL)
        {
            WebXRManager.Instance.ToggleAR();
            Debug.Log("AR is ON");
        }
        else
        {
            Debug.Log("no AR");
        }
    }

    // Update is called once per frame
    void Update()
    {
        countext.text=counter.ToString();

        switch (counter)
        {
            case 0:
                Globals.rocketStatus = "lookingAround";
                showStoryWindow("הנה הטיל שישגר את הלוויין שלכם למסלול!");
                showBtnStoryWin = true;
                break;

            case 1:
                hideStoryWindow();
                advanceCounter();
                break;

            case 2:
                Globals.rocketStatus = "connectSat";
                showInstructionWindow("חברו את הלוויין אל הטיל");
                showBtnInstWin = false;
                break;

            case 3:
                hideInstructionWindow();
                advanceCounter();
                break;

            case 4:
                showInstructionWindow("הטיל מוכן לשיגור");
                showBtnInstWin = false;
                break;

            case 5:
                hideInstructionWindow();
                advanceCounter();
                break;
        }
    }

    public void advanceCounter()
    {
        counter++;
        Debug.Log("COUNTER: " + counter);
    }

    void showStoryWindow(string textContent)
    {
        typewriterUI.TextToType = textContent;
        StoryBTN.SetActive(false);
        StoryWindow.SetActive(true);
        if (showBtnStoryWin)
        {
            if (typewriterUI.TypeWriterIsFinished)
            {
                StoryBTN.SetActive(true);
            }
        }
    }

    void hideStoryWindow()
    {
        WinAnimMove.exitAnimationTrigger = true;
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
        WinAnimOpen.exitAnimationTrigger = true;
    }
}
