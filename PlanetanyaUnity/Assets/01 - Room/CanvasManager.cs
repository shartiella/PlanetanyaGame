using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static int CameraPositionCounter = 0;
    [SerializeField] private GameObject Cam;
    [SerializeField] private GameObject LookAtTarget;

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
        if (CameraPositionCounter==1)
        {
            changeCameraPosition1stTime();
        }
        else
        {

        }

        switch (counter)
        {
            case 0:
                showStoryWindow("בוקר טוב! עכשיו מתעוררים?");
                showBtnStoryWin = true;
                break;

            case 1:
                hideStoryWindow();
                counter = 2;
                break;

            case 2:
                CameraPositionCounter = 1;
                break;

            case 3:
                showStoryWindow("יש לכם עוד הרבה עבודה היום, אז כדאי שתתחילו להתקדם...");
                showBtnStoryWin = true;
                break;

            case 4:
                hideStoryWindow();
                counter = 5;
                break;

            case 5:
                showInstructionWindow("שוטטו בחדר ולחצו על אחד המכשירים המהבהבים");
                showBtnInstWin = false;
                BlinkColor.glowOn= true;
                break;

            case 6:
                hideInstructionWindow();
                counter = 7;
                break;

            case 7:
                if (RoomCamera.deviceClicked == "Phone")
                {
                    showStoryWindow("החלטתם משום מה להזמין המבורגר לארוחת בוקר. אנחנו לא שופטים...");
                }
                else if (RoomCamera.deviceClicked == "Computer")
                {
                    showStoryWindow("החלטתם שבא לכם לבקר בפלנתניה - המקום הכי מושלם בעולם!!!");
                }
                else if (RoomCamera.deviceClicked == "TV")
                {
                    showStoryWindow("החלטתם משום מה לפתוח את הבוקר בצפייה בטלוויזיה");
                }
                break;

            case 8:
                hideStoryWindow();
                counter = 9;
                break;

            case 9:
                if (RoomCamera.deviceClicked == "Phone")
                {
                    showStoryWindow("השליח בדרך אליכם, ואתם עוקבים אחריו במפה");
                }
                else if (RoomCamera.deviceClicked == "Computer")
                {
                    showStoryWindow("התחלתם לתכנן את המסלול לנתניה באתר עם מפות");
                }
                else if (RoomCamera.deviceClicked == "TV")
                {
                    showStoryWindow("סבבה, זה לא שיש לכם דברים יותר חשובים לעשות...");
                }
                break;

            case 10:
                hideStoryWindow();
                counter = 11;
                break;

            case 11:
                if (RoomCamera.deviceClicked == "Phone")
                {
                    showStoryWindow("אבל רגע, איך בעצם הטלפון יודע איפה השליח נמצא על המפה?");
                }
                else if (RoomCamera.deviceClicked == "Computer")
                {
                    showStoryWindow("אבל רגע, איך בעצם צילמו את המפה הזאת?");
                }
                else if (RoomCamera.deviceClicked == "TV")
                {
                    showStoryWindow("אבל רגע, איך בעצם הטלוויזיה יודעת מה לשדר לנו?");
                }
                break;

            case 12:
                hideStoryWindow();
                counter = 13;
                break;

            case 13:
                showStoryWindow("כדי להבין את זה, בואו נקפוץ רגע לחלל...");
                break;

                case 14:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                counter = 15;
                break;

        }
    }

    public void advanceCounter()
    {
        counter++;
        Debug.Log(counter);
    }

    void showStoryWindow(string textContent)
    {
        typewriterUI.TextToType = textContent;
        StoryBTN.SetActive(false);
        StoryWindow.SetActive(true);
        if (typewriterUI.TypeWriterIsFinished)
        {
            if (showBtnStoryWin)
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

    void changeCameraPosition1stTime()
    {
        CameraPositionCounter = 2;
        if (CameraPositionCounter == 2)
        {
            Vector3 pos = new Vector3(1.544f, 1.866f, 2.175f);
            Vector3 lookAt = new Vector3(1.87f, 1.26f, -7.81f);

            //Cam.transform.position = pos;
            Cam.transform.LeanMove(pos, 0.5f).setEaseInOutQuad();
            Cam.GetComponent<CinemachineVirtualCamera>().enabled = true;
            LookAtTarget.transform.LeanMove(lookAt, 2).setOnComplete(stopMovingCamera);
        }

        CameraPositionCounter = 3;
    }

    void stopMovingCamera()
    {
        Cam.GetComponent<CinemachineVirtualCamera>().enabled = false;

        if (counter == 2)
        {
            counter = 3;
        }

    }
}
