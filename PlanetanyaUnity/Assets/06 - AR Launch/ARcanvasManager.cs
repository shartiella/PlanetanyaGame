using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                Globals.rocketStatus = "lookingAround";
                showStoryWindow("אחרי שבניתם לוויין ולמדתם לשגר אותו, הגיע הזמן לשגר את הלוויין שלכם לחלל!", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 1;
                }
                break;

            case 1:
                showInstructionWindow("לכו לחלון בדרך לפלנטריום");
                if (typewriterUI.TypeWriterIsFinished)
                {
                    arrivedBTN.SetActive(true);
                }
                break;

            case 2:
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
                hideStoryWindow();
                counter = 3;
                break;

            case 3:
                showStoryWindow("זה הטיל שלכם - והוא כמעט מוכן לשיגור!", true);
                break;

            case 4:
                hideStoryWindow();
                counter = 5;
                break;

            case 5:
                showStoryWindow("כל מה שצריך זה רק לחבר אליו את הלוויין", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    showInstructionWindow("חפשו את הלוויין וגררו אותו אל הטיל");
                    Globals.rocketStatus = "connectSat";
                }
                //אם עובר זמן מסוים והמצלמה עוד לא כוונה לכיוון הלוויין, שיופיע חץ שיצביע עליו
                //חיברו אל הטיל
                break;

            case 6:
                hideInstructionWindow();
                hideStoryWindow();
                counter= 7;
                break;
            
            case 7:
                showStoryWindow("סוף סוף - הגעתם לרגע האמת! עכשיו נשאר רק ללחוץ על הכפתור...", false);
                break;

            case 8:
                hideStoryWindow();
                break;

            case 9:
                StoryWinAnim.exitAnimationTrigger = false;
                showStoryWindow("לאן הטיל נעלם?", true);
                break;

            case 10:
                if (WebXRManager.Instance.XRState == WebXRState.AR)
                {
                    WebXRManager.Instance.ToggleAR();
                    Debug.Log("AR is OFF");
                }
                else
                {
                    Debug.Log("no AR");
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
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
