using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq.Expressions;

public class CanvasManager : MonoBehaviour
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
        //if (counter == 0)
        //{
        //    showStoryWindow("בוקר טוב! עכשיו מתעוררים?");
        //    showBtnStoryWin = true;
        //}
        //else if (counter == 1)
        //{
        //    hideStoryWindow();
        //    counter = 2;
        //}
        //else if (counter == 2)
        //{
        //    showStoryWindow("asd asdasd afsdf sss");
        //    //showBtnStoryWin = true;
        //}

        switch (counter)
        {
            case 0:
                showStoryWindow("בוקר טוב! עכשיו מתעוררים?");
                showBtnStoryWin = true;
                break;

            case 1:
                hideStoryWindow();
                advanceCounter();
                break;

            case 2:
                showStoryWindow("יש לכם עוד הרבה עבודה היום, אז כדאי שתתחילו להתקדם...");
                showBtnStoryWin = true;
                break;

            case 3:
                hideStoryWindow();
                advanceCounter();
                break;

            case 4:
                showInstructionWindow("שוטטו בחדר ולחצו על אחד המכשירים המהבהבים");
                showBtnInstWin = false;
                BlinkColor.glowOn= true;
                break;

            case 5:
                hideInstructionWindow();
                advanceCounter();
                break;

            case 6:
                if (MoveCamera.deviceClicked == "Phone")
                {
                    showStoryWindow("החלטתם משום מה להזמין המבורגר לארוחת בוקר. אנחנו לא שופטים...");
                }
                else if (MoveCamera.deviceClicked == "Computer")
                {
                    showStoryWindow("החלטתם שבא לכם לבקר בפלנתניה - המקום הכי מושלם בעולם!!!");
                }
                else if (MoveCamera.deviceClicked == "TV")
                {
                    showStoryWindow("החלטתם משום מה לפתוח את הבוקר בצפייה בטלוויזיה");
                }
                break;

            case 7:
                hideStoryWindow();
                advanceCounter();
                break;

            case 8:
                if (MoveCamera.deviceClicked == "Phone")
                {
                    showStoryWindow("השליח בדרך אליכם, ואתם עוקבים אחריו במפה");
                }
                else if (MoveCamera.deviceClicked == "Computer")
                {
                    showStoryWindow("התחלתם לתכנן את המסלול לנתניה באתר עם מפות");
                }
                else if (MoveCamera.deviceClicked == "TV")
                {
                    showStoryWindow("סבבה, זה לא שיש לכם דברים יותר חשובים לעשות...");
                }
                break;

            case 9:
                hideStoryWindow();
                advanceCounter();
                break;

            case 10:
                if (MoveCamera.deviceClicked == "Phone")
                {
                    showStoryWindow("אבל רגע, איך בעצם הטלפון יודע איפה השליח נמצא על המפה?");
                }
                else if (MoveCamera.deviceClicked == "Computer")
                {
                    showStoryWindow("אבל רגע, איך בעצם צילמו את המפה הזאת?");
                }
                else if (MoveCamera.deviceClicked == "TV")
                {
                    showStoryWindow("אבל רגע, איך בעצם הטלוויזיה יודעת מה לשדר לנו?");
                }
                break;

            case 11:
                hideStoryWindow();
                advanceCounter();
                break;

            case 12:
                showStoryWindow("כדי להבין את זה, בואו נקפוץ רגע לחלל...");
                break;
        }
    }

    public void advanceCounter()
    {
        counter++;
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
        SlideFromTop.exitAnimationTrigger = true;
    }
}
