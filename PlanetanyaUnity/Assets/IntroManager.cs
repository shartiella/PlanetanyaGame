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
                showStoryWindow("הנה כדור הארץ – כאן אנחנו נמצאים",true);
                break;

            case 1:
                hideStoryWindow();
                counter = 2;
                break;

            case 2:
     
             
                    showStoryWindow("את כדור הארץ מקיף הירח", true);
                        Moon.SetActive(true);
               
       



                break;

            case 3:
          
                hideStoryWindow();
                counter = 4;
                break;

            case 4:
                showInstructionWindow("געו במסך בכדי לראות את הירח וכדור הארץ מזוויות שונות");
                showBtnInstWin = false;
                BlinkColor.glowOn = true;
                if (typewriterUI.TypeWriterIsFinished)
                {
                    showStoryWindow("כל הגופים שמקיפים את כדור הארץ נקראים לוויינים. למה?", true);
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
                 showStoryWindow("כי הם מלווים את כדור הארץ, לכן הירח מכונה לוויין טבעי. למה טבעי?", true);
          
                break;
            case 7:
                hideStoryWindow();
                counter = 8;
                break;

            case 8:
          
                showStoryWindow ("כי יש גם לוויינים שהם מלאכותיים שבני אדם תיכננו, יצרו ושלחו למסלול סביב כדור הארץ", true);
                Satellite.SetActive(true);
                Moon.SetActive(false);
                break;
 

            case 9:
               
                hideStoryWindow();
                counter = 10;
                break;

            case 10:

                showStoryWindow("כיום, מקיפים אותנו אלפי לוויינים מלאכותיים מסוגים שונים ומסלולים שונים ואנחנו מסתמכים עליהם בחיי היומיום שלנו", true);
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
