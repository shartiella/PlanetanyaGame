using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BuildIU : MonoBehaviour
{
    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private bool showBtnInstWin;
    [SerializeField] private GameObject StoryWindow;
    [SerializeField] private bool showBtnStoryWin;
    private TextMeshProUGUI InstructionTXT;
    [SerializeField] private GameObject InstructionBTN;
    [SerializeField] private GameObject StoryTXT;
    [SerializeField] private GameObject StoryBTN;
    [SerializeField] private GameObject Fader;

    public static int counter = 0;
    public static int numberOfObjectConnections = 0;
    public static int numberOfCorrectObjectsConnected = 0;
    public static int numberOfWrongObjectsConnected = 0;
    public static int overallNumberOfCorrectParts = 0;

    [SerializeField] private AllObjects _allObjects;

    public static float totalTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //InstructionTXT = InstructionWindow.GetComponentInChildren<TextMeshProUGUI>();
        ////InstructionBTN= InstructionWindow.GetComponentInChildren<GameObject>();
        //StoryTXT = StoryWindow.GetComponentInChildren<TextMeshProUGUI>();
        //StoryBTN = InstructionWindow.GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        switch (counter)
        {
            case 0:
                BlinkColor.glowOn = false;
                showStoryWindow("כדי להבין איך עובד לוויין "+Globals.ChosenSatellite.Kind+", תצטרכו להרכיב אחד!", true);
                break;

            case 1:
                hideStoryWindow();
                counter = 2;
                break;

            case 2:
                showInstructionWindow("גררו אל השולחן את הרכיבים הנכונים כדי להרכיב מהם את הלוויין");
                if (typewriterUI.TypeWriterIsFinished)
                {
                    AllObjects.BuildingState = "building";
                    BlinkColor.glowOn = true;
                    StoryTXT.GetComponent<typewriterUI>().enabled = false;
                    counter = 3;
                }
                break;

            case 3:
                //שני דברים יכולים לקרות פה
                //או שמתחילים לחבר חלקים לבסיס, ומתקדמים לרמז
                //או שפותחים את הרשימה, ואז מתקדמים לקייס שבע
                break;

            case 4:
                hideInstructionWindow();
                StoryTXT.GetComponent<typewriterUI>().enabled = true;
                showStoryWindow("כדי לדעת אילו חלקים לחבר, כדאי שתציצו ברשימה שעל השולחן...", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 5;
                }
                break;

            case 5:
                StoryTXT.GetComponent<typewriterUI>().enabled = false;
                break;

            case 6:
                if (StoryWindow.activeSelf)
                {
                    hideStoryWindow();
                }
                counter = 8;
                break;

            case 7:
                hideInstructionWindow();
                counter = 8;
                break;

                case 8:

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
                //יהיה עוד משהו לפני זה?
                Fader.SetActive(true);
                Globals.LevelStats3 += " מספר חיבורים כולל: " + Globals.Reverse(BodyTrigger.connectedPartCounter.ToString()); //כמה פעמים חוברו חלקים עד שהצליחו
                Globals.LevelStats3 += "\n זמן כולל: " + Globals.Reverse(Mathf.RoundToInt(totalTime).ToString()) + " שניות";
                counter = 11;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;

            case 11:

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














































////פקדי UI
//[SerializeField] private GameObject doneBTN;
//    //[SerializeField] private GameObject feedbackWindow;
//    [SerializeField] private GameObject feedback;

//    public GameObject InfoPanel;
//    //public static bool showInfoPanel = false;
//    //public static bool isTouchingSatBody = false;
//    //[SerializeField] private GameObject guideInput;
//    [SerializeField] private Material correctColor;
//    [SerializeField] private Material wrongColor;
//    [SerializeField] private AllObjects _allObjects;
//    [SerializeField] private GameObject introWindow;
//    //[SerializeField] private GameObject endWindow;

//    [SerializeField] private Globals _globals;
//    public static int numberOfCorrectObjectsConnected = 0;
//    public static int numberOfWrongObjectsConnected = 0;
//    public static int overallNumberOfCorrectParts = 0;
//    public TextMeshProUGUI mashov;
//    public TextMeshProUGUI correctNum;
//    public TextMeshProUGUI wrongNum;
//    public TextMeshProUGUI missingNum;

//    public static bool showDoneBTN;

//    [SerializeField] private GameObject Fader;

//    // Start is called before the first frame update
//    void Start()
//    {
//        open1stWindow();

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //if (Globals.ChosenSatellite == null)
//        //{
//        //    //תעבור על רשימת הלוויינים
//        //    foreach (Satellite satellite in _globals.SatellitesList)
//        //    {
//        //        //תמצא את הלווין ששמו הוא שם הלוויין שנבחר
//        //        if (satellite.Name == _allObjects.satNameForCheck)
//        //        {
//        //            //תגדיר אותו בתור הלוויין שנבחר
//        //            Globals.ChosenSatellite = satellite;
//        //            Debug.Log("chosen satellite is " + Globals.ChosenSatellite.Name);

//        //        }
//        //    }
//        //}

//        if (AllObjects.BuildingState == "")
//        {
//            update1stWindow();

//        }
//        else if (AllObjects.BuildingState == "building")
//        {

//            if(Input.GetMouseButtonUp(0))
//            {
//                if (numberOfCorrectObjectsConnected + numberOfWrongObjectsConnected > 0)
//                {
//                    doneBTN.SetActive(true);
//                }
//                else
//                {
//                    doneBTN.SetActive(false);
//                }
//            }


//        }
//    }

    //public void open1stWindow()
    //{
    //    introWindow.SetActive(true);
    //}

    //public void update1stWindow()
    //{
    //    introWindow.GetComponentInChildren<TextMeshProUGUI>().text = "מה צריך להיות בלוויין " + Globals.ChosenSatellite.Kind + " כדי שהוא יעבוד?";
    //    introWindow.GetComponentInChildren<TextMeshProUGUI>().text += " גררו את החלקים כדי להרכיב את הלוויין.";
    //}

    //public void beginBuilding()
    //{
    //    introWindow.SetActive(false);
    //    AllObjects.BuildingState = "building";
    //}

    //public void DoneBuilding()
    //{
    //    AllObjects.BuildingState = "checking";
    //    doneBTN.SetActive(false);
    //    InfoPanel.SetActive(false);

    //    if (overallNumberOfCorrectParts == numberOfCorrectObjectsConnected && numberOfWrongObjectsConnected==0)
    //    {
    //        continueToNextLevel();
    //    }
    //    else
    //    {
    //        feedback.SetActive(true);
    //        //אולי לשנות למשוב יותר כזה:
    //        //לוויין ה-משהו- שלכם לא מוכן עדיין! בדקו ברשימה מהם 2 החלקים החסרים
    //        //וגם, חיברתם 2 חלקים שלא מתאימים לסוג הלוויין הזה
    //        mashov.text = "בלוויין ה" + Globals.ChosenSatellite.Kind + " שבניתם יש:";
    //        correctNum.text = numberOfCorrectObjectsConnected.ToString();
    //        wrongNum.text = numberOfWrongObjectsConnected.ToString();
    //        missingNum.text = (overallNumberOfCorrectParts - numberOfCorrectObjectsConnected).ToString();
    //    }
    //}

    //public void returnToBuilding()
    //{
    //    AllObjects.BuildingState = "building";
    //    feedback.SetActive(false);
    //    //guideInput.SetActive(false);
    //    //endWindow.SetActive(false);

    //}

    //public void continueToNextLevel()
    //{
    //    //endWindow.SetActive(false);
    //    AllObjects.BuildingState = "finished";

    //    //יהיה עוד משהו לפני זה?
    //    Fader.SetActive(true);
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

//    //}
//}
