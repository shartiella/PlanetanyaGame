using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class FinalLaunchManager : MonoBehaviour
{
    public static int counter = 0;

    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private GameObject StoryWindow;
    [SerializeField] private GameObject StoryBTN;

    [SerializeField] private CinemachineVirtualCamera Cam;

    private Transform camplace1;
    private Transform camplace2;
    private Transform camplace3;
    [SerializeField] private Transform LEOcamplace1;
    [SerializeField] private Transform LEOcamplace2;
    [SerializeField] private Transform LEOcamplace3;
    [SerializeField] private Transform MEOcamplace1;
    [SerializeField] private Transform MEOcamplace2;
    [SerializeField] private Transform MEOcamplace3;
    [SerializeField] private Transform GEOcamplace1;
    [SerializeField] private Transform GEOcamplace2;
    [SerializeField] private Transform GEOcamplace3;
    private int camPos = 1;

    private GameObject rocketTop;
    private GameObject rocketBottom;
    private ParticleSystem fire;
    private GameObject Sat;
    [SerializeField] private GameObject rocketLEO;
    [SerializeField] private GameObject rocketMEO;
    [SerializeField] private GameObject rocketGEO;

    [SerializeField] private GameObject LEOrocketTop;
    [SerializeField] private GameObject LEOrocketBottom;
    [SerializeField] private ParticleSystem LEOfire;
    [SerializeField] private GameObject LEOSat;
    [SerializeField] private GameObject MEOrocketTop;
    [SerializeField] private GameObject MEOrocketBottom;
    [SerializeField] private ParticleSystem MEOfire;
    [SerializeField] private GameObject MEOSat;
    [SerializeField] private GameObject GEOrocketTop;
    [SerializeField] private GameObject GEOrocketBottom;
    [SerializeField] private ParticleSystem GEOfire;
    [SerializeField] private GameObject GEOSat;

    private GameObject target;
    [SerializeField] private GameObject LEOTarget;
    [SerializeField] private GameObject MEOTarget;
    [SerializeField] private GameObject GEOTarget;

    [SerializeField] private GameObject LEOlight;
    [SerializeField] private GameObject MEOlight;
    [SerializeField] private GameObject GEOlight;

    private float pushTimer = 0;

    float totalTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (Globals.ChosenSatellite.Orbit == "LEO")
        {
            //target = LEOTarget;
            rocketTop= LEOrocketTop;
            rocketBottom= LEOrocketBottom;
            fire = LEOfire;
            rocketLEO.SetActive(true);
            camplace1 = LEOcamplace1;
            camplace2 = LEOcamplace2;
            camplace3 = LEOcamplace3;
            Cam.Follow = LEOcamplace1.transform;
            Cam.LookAt = LEOTarget.transform;
            Sat = LEOSat;
        }
        else if (Globals.ChosenSatellite.Orbit == "MEO")
        {
            //target = MEOTarget;
            rocketTop = MEOrocketTop;
            rocketBottom = MEOrocketBottom;
            fire = MEOfire;
            rocketMEO.SetActive(true);
            camplace1 = MEOcamplace1;
            camplace2 = MEOcamplace2;
            camplace3 = MEOcamplace3;
            Cam.Follow = MEOcamplace1.transform;
            Cam.LookAt = MEOTarget.transform;
            Sat = MEOSat;
        }
        else if (Globals.ChosenSatellite.Orbit == "GEO")
        {
            //target = GEOTarget;
            rocketTop = GEOrocketTop;
            rocketBottom = GEOrocketBottom;
            fire = GEOfire;
            rocketGEO.SetActive(true);
            camplace1 = GEOcamplace1;
            camplace2 = GEOcamplace2;
            camplace3 = GEOcamplace3;
            Cam.Follow = GEOcamplace1.transform;
            Cam.LookAt = GEOTarget.transform;
            Sat = GEOSat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        switch (counter)
        {
            case 0:
                changeCamPosition();
                hideStoryWindow();
                if (pushTimer < 8)
                {
                    pushTimer += Time.deltaTime;

                    if (pushTimer <= 5)
                    {
                        var fireEmission = fire.emission;
                        fireEmission.enabled = true;
                        fireEmission.rateOverTime = 30;

                    }
                    else
                    {
                        var fireEmission = fire.emission;
                        fireEmission.enabled = false;
                    }
                }
                else 
                {
                    counter = 1;
                }
                break;

            case 1:
                rocketTop.GetComponent<pullTowardsEarth>().enabled = true;
                rocketTop.GetComponent<CapsuleCollider>().enabled = true;
                rocketBottom.GetComponent<pullTowardsEarth>().enabled = true;
                rocketBottom.GetComponent<CapsuleCollider>().enabled = true;
                Sat.SetActive(true);
                if (!StoryWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    //StoryBTN.SetActive(false);
                    counter = 2;
                }
                break;

            case 2:
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("כל הכבוד – עשיתם את זה! בניתם ושיגרתם לוויין מיפוי למסלול נמוך!", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("כל הכבוד – עשיתם את זה! בניתם ושיגרתם לוויין ניווט למסלול בינוני!", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("כל הכבוד – עשיתם את זה! בניתם ושיגרתם לוויין תקשורת למסלול גיאוסינכרוני!", true);
                }
                break;

            case 3:
                hideStoryWindow();
                counter = 4;
                break;

            case 4:
                showStoryWindow("זה היה תהליך ארוך ומאתגר, אבל היה שווה את זה!", true);
                break;

            case 5:
                hideStoryWindow();
                changeCamPosition();
                counter = 6;
                break;

            case 6:
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("לוויינים עומדים מאחורי הרבה דברים בחיי היומיום שלנו, כמו למשל ניווט ותקשורת.", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("לוויינים עומדים מאחורי הרבה דברים בחיי היומיום שלנו, כמו למשל מיפוי ותקשורת.", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("לוויינים עומדים מאחורי הרבה דברים בחיי היומיום שלנו, כמו למשל מיפוי וניווט.", true);
                }
                break;

            case 7:
                hideStoryWindow();
                counter = 8;
                break;

            case 8:
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("אבל מה שחשוב, הוא שעכשיו אתם יודעים איך מצלמים מפות מהחלל!", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("אבל מה שחשוב, הוא שעכשיו אתם יודעים איך הטלפון יודע איפה השליח נמצא על המפה!", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("אבל מה שחשוב, הוא שעכשיו אתם יודעים איך הטלוויזיה יודעת מה לשדר לנו!", true);
                }
                break;

            case 9:
                hideStoryWindow();
                hideInstructionWindow();
                if (!StoryWindow.activeSelf && !InstructionWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    SlideFromTop.exitAnimationTrigger = false;
                    counter = 10;
                }
                break;

            case 10:
                if (!StoryWindow.activeSelf && camPos==4)
                {
                    counter = 11;
                }
                break;

            case 11:
                Globals.LevelStats6 += " זמן כולל: " + Globals.Reverse(Mathf.RoundToInt(totalTime).ToString()) + " שניות";
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
        //StoryBTN.SetActive(false);
        StoryWindow.SetActive(true);
        if (typewriterUI.TypeWriterIsFinished)
        {
            if (showBtn && !StoryBTN.activeSelf)
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

    void changeCamPosition()
    {
        if (camPos == 1 && counter == 0)
        {
            camPos = 2;
            camplace1.LeanMoveLocal(camplace2.localPosition, 10).setDelay(1).setEaseInOutQuad();
        }
        else if (camPos == 2 && counter==5) //לשנות קאונטר
        {
            camPos = 3;
            if (Globals.ChosenSatellite.Orbit == "LEO")
            {
                LEOlight.SetActive(true);
            }
            else if (Globals.ChosenSatellite.Orbit == "MEO")
            {
                MEOlight.SetActive(true);
            }
            else if (Globals.ChosenSatellite.Orbit == "GEO")
            {
                GEOlight.SetActive(true);
            }
            camplace1.LeanMoveLocal(camplace3.localPosition, 20).setDelay(1).setEaseInOutQuad().setOnComplete(endLevel);
        }
    }

    void endLevel()
    {
        counter = 10;
        camPos = 4;
    }
}
