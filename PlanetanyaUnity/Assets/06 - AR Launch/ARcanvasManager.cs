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
    public static bool ARisON = false;

    float totalTime = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        totalTime += Time.deltaTime;
        //countext.text=counter.ToString();

        switch (counter)
        {
            case 0:
                Globals.rocketStatus = "lookingAround";
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("אחרי שבניתם לוויין מיפוי ולמדתם לשגר אותו, הגיע הזמן לשגר את הלוויין שלכם לחלל!", false);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("אחרי שבניתם לוויין ניווט ולמדתם לשגר אותו, הגיע הזמן לשגר את הלוויין שלכם לחלל!", false);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("אחרי שבניתם לוויין תקשורת ולמדתם לשגר אותו, הגיע הזמן לשגר את הלוויין שלכם לחלל!", false);
                }
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

                hideInstructionWindow();
                hideStoryWindow();
                if (!StoryWindow.activeSelf && !InstructionWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    SlideFromTop.exitAnimationTrigger = false;
                    counter = 3;
                }
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
                    CameraRotateAR.moveCamWithDrag = false;
                }
                //אם עובר זמן מסוים והמצלמה עוד לא כוונה לכיוון הלוויין, שיופיע חץ שיצביע עליו
                //חיברו אל הטיל
                break;

            case 6:
                hideInstructionWindow();
                hideStoryWindow();
                Globals.LevelStats5 += " זמן עד מציאת הלוויין: " + Globals.Reverse(Mathf.RoundToInt(totalTime).ToString()) + " שניות";
                counter = 7;
                break;
            
            case 7:
                showStoryWindow("סוף סוף - הגעתם לרגע האמת! עכשיו נשאר רק ללחוץ על הכפתור...", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    Globals.rocketStatus = "ToLaunch";
                    CameraRotateAR.moveCamWithDrag = true;
                }
                else
                {
                    Globals.rocketStatus = "satConnected";
                }
                break;

            case 8:
                hideStoryWindow();
                if (!StoryWindow.activeSelf && !InstructionWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    SlideFromTop.exitAnimationTrigger = false;
                }
                break;

            case 9:
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
                counter = 11;
                break;

            case 11:
                hideStoryWindow();
                hideInstructionWindow();
                if (!StoryWindow.activeSelf && !InstructionWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    SlideFromTop.exitAnimationTrigger = false;
                    counter = 12;
                }
                break;

                case 12:
                Globals.LevelStats5 += "\n זמן כולל: " + Globals.Reverse(Mathf.RoundToInt(totalTime).ToString()) + " שניות";
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
                StoryBTN.SetActive(false);
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

    public void turnOnARorFullScreen()
    {
        if (WebXRManager.Instance.isSupportedAR)
        {
            if (WebXRManager.Instance.XRState == WebXRState.NORMAL)
            {
                WebXRManager.Instance.ToggleAR();
                Debug.Log("AR is ON");
                ARisON = true;
            }
            else
            {
                Debug.Log("AR turn on fail");
                Camera.main.fieldOfView = 90;
            }
        }
        else
        {
            Debug.Log("AR not supported");
            Camera.main.fieldOfView = 90;
        }
        counter = 2;
    }
}
