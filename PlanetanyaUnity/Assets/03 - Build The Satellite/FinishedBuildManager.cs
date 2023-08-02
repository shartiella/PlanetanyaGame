using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedBuildManager : MonoBehaviour
{
    [SerializeField] private GameObject GPS;
    [SerializeField] private GameObject TV;
    [SerializeField] private GameObject MAP;

    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private GameObject StoryWindow;
    [SerializeField] private GameObject StoryBTN;

    public static int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (Globals.ChosenSatellite.Name == "GPS")
        {
            GPS.SetActive(true);
        }
        else if (Globals.ChosenSatellite.Name == "TV")
        {
            TV.SetActive(true);
        }
        else if (Globals.ChosenSatellite.Name == "MAP")
        {
            MAP.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (counter)
        {
            case 0:
                if (Globals.ChosenSatellite.Name == "GPS")
                {
                    showStoryWindow("כל הכבוד! בניתם לוויין ניווט בהצלחה!", true);
                }
                else if (Globals.ChosenSatellite.Name == "TV")
                {
                    showStoryWindow("כל הכבוד! בניתם לוויין תקשורת בהצלחה!", true);
                }
                else if (Globals.ChosenSatellite.Name == "MAP")
                {
                    showStoryWindow("כל הכבוד! בניתם לוויין מיפוי בהצלחה!", true);
                }
                break;
            
            case 1:
                hideStoryWindow();
                counter = 2;
                break;

            case 2:
                showStoryWindow("עכשיו כשהלוויין מוכן, צריך להבין איך לשגר אותו בכלל - ולאן?", true);
                break;

            case 3:
                hideStoryWindow();
                hideInstructionWindow();
                if (!StoryWindow.activeSelf && !InstructionWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    SlideFromTop.exitAnimationTrigger = false;
                    counter = 4;
                }

                //StoryBTN.SetActive(false);
                //StoryWinAnim.exitAnimationTrigger = true;
                //SlideFromTop.exitAnimationTrigger = false;
                //counter = 4;
                break;

            case 4:
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
        InstructionWindow.SetActive(true);
    }

    void hideInstructionWindow()
    {
        SlideFromTop.exitAnimationTrigger = true;
    }
}
