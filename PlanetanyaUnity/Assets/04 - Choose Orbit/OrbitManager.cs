using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class OrbitManager : MonoBehaviour
{
    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private bool showBtnInstWin;
    [SerializeField] private GameObject StoryWindow;
    //[SerializeField] private bool showBtnStoryWin;

    public static float forceFactor = 1;

    private TextMeshProUGUI InstructionTXT;
    [SerializeField] private GameObject InstructionBTN;
    [SerializeField] private TMP_Text StoryTXT;
    [SerializeField] private GameObject StoryBTN;
    [SerializeField] private GameObject pushBTN;

    public static int counter = 0;
    public static bool showLauncherAfterCrash = false;
    public static bool showResetAfterLaunch = false;
    public static bool showPushAfterLaunch = false;
    public static bool showGoalAfterCrash = false;

    [SerializeField] private GameObject launcher;
    [SerializeField] private GameObject antiLauncher;

    [SerializeField] private GameObject Orbits;
    [SerializeField] private GameObject OrbitsNames;
    [SerializeField] private GameObject resetBTN;
    [SerializeField] private GameObject Goal;

    [SerializeField] private GameObject demoBTN;

    [SerializeField] private GameObject GEOexample;
    [SerializeField] private GameObject MEOexample;
    [SerializeField] private GameObject LEOexample;

    public static bool crashFromEarthCollision=false;
    public static bool LaunchTowardsEarth = false;
    public static bool lastLaunchWasTowardsEarth = false;
    public static bool RocketHasBeenPushed = false;

    public static float neededOrbitTime=5;

    public static Vector3 lastFingerRelease;

    //float eccentricityPercent =0;
    string eccentricityFeedback = "";

    float flytime = 0;
    float totalTime = 0;
    public static int launchesCounter = 0;
    public static int crashesCounter = 0;
    public static int resetCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        InstructionTXT = InstructionWindow.GetComponentInChildren<TextMeshProUGUI>();
        StoryTXT = StoryWindow.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        switch (counter)
        {
            case 0:
                showStoryWindow("כדי לשלוח לוויין לחלל, צריך לשגר אותו למסלול סביב כדור הארץ בעזרת טיל!", true);
                break;

            case 1:
                hideStoryWindow();
                launcher.SetActive(true);
                showLauncherAfterCrash = true;
                counter = 2;
                break;

            case 2:
                showStoryWindow("רואים את העיגול הסגול? נסו למשוך אותו ולשחרר כדי לשגר את הטיל. הנקודות הסגולות יסמנו את המסלול החזוי.", false);
                if (typewriterUI.TypeWriterIsFinished && !StoryWinAnim.activeAnimation)
                {

                    counter = 3;
                }
                break;

            case 3:
                //קייס ריק כדי שהמשגר לא יופיע כל הזמן
                if (Globals.rocketStatus == "crashed")
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    counter = 4;
                }
                break;

            case 4:
                hideStoryWindow();
                showInstructionWindow("שגרו את הטיל אל הנקודה הירוקה");
                forceFactor = 5;
                if (typewriterUI.TypeWriterIsFinished && !SlideFromTop.activeAnimation)
                {
                    Goal.SetActive(true);
                    showGoalAfterCrash = true;
                    launchesCounter = 0;
                    counter = 5;
                }
                break;

            case 5:
                if (Globals.rocketStatus == "launching")
                {
                    hideStoryWindow();
                }
                else if (Globals.rocketStatus == "crashed")
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    counter = 6;
                }
                break;

            case 6:
                if (GoalScript.GoalsAccomplished < GoalScript.positionCounter)
                {
                    switch (launchesCounter)
                    {
                        case 1:
                            showStoryWindow("לא נורא, זה היה רק הניסיון הראשון שלכם... נסו שוב!", false);
                            break;

                        case 2:
                            showStoryWindow("נסו שוב, אנחנו מאמינים בכם!", false);
                            break;

                        case 3:
                            showStoryWindow("כדאי לקרוא שוב את ההנחיה...", false);
                            break;

                        default:
                            showStoryWindow("אתם לא עושים לנו דווקא, נכון?", false);
                            break;
                    }

                    if (typewriterUI.TypeWriterIsFinished)
                    {
                        counter = 5;
                    }
                }
                else
                {
                    if (launchesCounter <= 1)
                    {
                        showStoryWindow("הצלחתם בניסיון הראשון! נתתם לטיל מספיק כוח כדי לברוח מכוח המשיכה של כדור הארץ ולעוף לחלל! עכשיו נסו את האתגר הבא...", false);
                    }
                    else
                    {
                        showStoryWindow("הצלחתם! נתתם לטיל מספיק כוח כדי לברוח מכוח המשיכה של כדור הארץ ולעוף לחלל! עכשיו נסו את האתגר הבא...", false);
                    }
                    forceFactor = 1;

                    if (typewriterUI.TypeWriterIsFinished)
                    {
                        Goal.SetActive(true);
                        Globals.LevelStats4 += "נק' 1: " + Globals.Reverse(launchesCounter.ToString()) +" שיגורים";
                        launchesCounter = 0;
                        counter = 7;
                    }
                }
                break;

            case 7:
                if (Globals.rocketStatus == "launching")
                {
                    hideStoryWindow();
                }
                else if (Globals.rocketStatus == "crashed")
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    counter = 8;
                }
                break;

                case 8:
                if (GoalScript.GoalsAccomplished < GoalScript.positionCounter)
                {
                    switch (launchesCounter)
                    {
                        case 1:
                            showStoryWindow("לא כזה פשוט, נכון? נסו שוב...", false);
                            break;

                        case 2:
                            showStoryWindow("נסו להבין איך לגרום למסלול להגיע אל הנקודה...", false);
                            break;

                        default:
                            showStoryWindow("נסו שוב, אנחנו מאמינים בכם!", false);
                            break;
                    }
                    if (typewriterUI.TypeWriterIsFinished)
                    {
                        counter = 7;
                    }
                }
                else
                {
                    switch (launchesCounter)
                    {
                        case 1:
                            showStoryWindow("כל הכבוד! הפעם הטיל לא ברח, כי כוח המשיכה של כדור הארץ משך אותו אליו. עכשיו קבלו טיפ...", true);
                            break;

                        default:
                            showStoryWindow("כל הכבוד! הפעם הטיל לא ברח, כי כוח המשיכה של כדור הארץ משך אותו אליו. עכשיו קבלו טיפ...", true);
                            break;
                    }
                }
                break;

                case 9:
                hideStoryWindow();
                Globals.LevelStats4 += "\nנק' 2: " + Globals.Reverse(launchesCounter.ToString()) + " שיגורים";
                counter = 10;
                break;

                case 10:
                showStoryWindow("אחרי כל שיגור, שימו לב לעיגול הסגול הקטן - הוא יסמן איפה שחררתם את האצבע בשיגור הקודם.", true);
                antiLauncher.SetActive(true);
                break;

                case 11:
                hideStoryWindow();
                counter = 12;
                break;

            case 12:
                showStoryWindow("ככה תוכלו לדעת מה לשפר במקרה הצורך! ועכשיו לנקודה הבאה... ", false);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    launchesCounter = 0;
                    Goal.SetActive(true);
                    counter = 13;
                }
                break;

            case 13:
                if (Globals.rocketStatus == "launching")
                {
                    hideStoryWindow();
                }
                else if (Globals.rocketStatus == "crashed")
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    counter = 14;
                }
                break;

            case 14:
                if (GoalScript.GoalsAccomplished < GoalScript.positionCounter)
                {
                    switch (launchesCounter)
                    {
                        case 1:
                            showStoryWindow("לא הצלחתם - נסו להבין למה ונסו שוב!", false);
                            break;

                        case 2:
                            showStoryWindow("כדאי להיעזר במיקום השיגור האחרון כדי להבין איך לשפר את השיגור", false);
                            break;

                        case 3:
                            showStoryWindow("נסו לשגר את הטיל כך שהמסלול החזוי עובר דרך הנקודה הירוקה", false);
                            break;

                        default:
                            showStoryWindow("נסו שוב, אנחנו מאמינים בכם!", false);
                            break;
                    }

                    if (typewriterUI.TypeWriterIsFinished)
                    {
                        counter = 13;
                    }
                }
                else
                {
                    hideInstructionWindow();

                    switch (launchesCounter)
                    {
                        case 1:
                            showStoryWindow("מעולה!! אתם בדרך הנכונה - זה מתחיל להיראות כמו מסלול סביב הכדור!", true);
                            break;

                        case 2:
                            showStoryWindow("מעולה! אתם בדרך הנכונה - זה מתחיל להיראות כמו מסלול סביב הכדור!?", true);
                            break;

                        default:
                            showStoryWindow("הצלחתם סוף סוף! אתם בדרך הנכונה - זה מתחיל להיראות כמו מסלול סביב הכדור!", true);
                            break;
                    }
                }
                break;

            case 15:
                hideStoryWindow();
                Globals.LevelStats4 += "\nנק' 3: " + Globals.Reverse(launchesCounter.ToString()) + " שיגורים";
                launchesCounter = 0;
                counter = 16;
                break;

            case 16:
                showStoryWindow("כמו שראיתם, עוצמת וכיוון השיגור משפיעים מאוד על המסלול של הטיל", true);
                break;

                case 17:
                hideStoryWindow();
                counter = 18;
                break;

            case 18:
                showStoryWindow("אם נרצה להכניס את הטיל למסלול הקפה סביב הכדור - נצטרך למצוא את עוצמת השיגור המתאימה", true);
                break;

            case 19:
                hideStoryWindow();
                SlideFromTop.exitAnimationTrigger = false;
                showInstructionWindow("שגרו את הטיל בעוצמה המתאימה");
                if (typewriterUI.TypeWriterIsFinished)
                {
                    launcher.SetActive(true);
                    counter = 20;
                }
                break;

            case 20:
                if (Globals.rocketStatus == "launching")
                {
                    hideStoryWindow();
                }
                else if (Globals.rocketStatus == "crashed")
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    counter = 21;
                }
                break;

            case 21: //משוב על עוצמת השיגור
                if (lastLaunchWasTowardsEarth)
                {
                    //לתוך האדמה
                    showStoryWindow("כדאי לזכור שלשגר טילים כלפי האדמה זה לא מאוד אפקטיבי...", false);
                    counter = 20;
                }
                else
                {
                    if (Globals.lastLaunchForce.magnitude > 4 && Globals.lastLaunchForce.magnitude < 5)
                    {
                        //עוצמה מתאימה
                        hideInstructionWindow();
                        showStoryWindow("כל הכבוד! שיגרתם את הטיל בעוצמה מתאימה! אבל...", true);
                    }
                    else
                    {
                        if (Globals.lastLaunchForce.magnitude <= 4)
                        {
                            //חלש מידי
                            showStoryWindow("שיגרתם את הטיל עם עוצמה נמוכה מידי. כוח המשיכה של כדור הארץ גרם לטיל ליפול בחזרה.", false);
                        }
                        else if (Globals.lastLaunchForce.magnitude >= 5)
                        {
                            //חזק מידי
                            if (crashFromEarthCollision)
                            {
                                showStoryWindow("שיגרתם את הטיל עם עוצמה חזקה מידי. הטיל הצליח להתרומם מכדור הארץ, אבל בסוף נפל בחזרה.", false);
                            }
                            else
                            {
                                showStoryWindow("שיגרתם את הטיל עם עוצמה חזקה מידי. הטיל התגבר על כוח המשיכה של כדור הארץ, וטס לחלל.", false);
                            }
                        }

                        counter = 20;
                    }
                }
                break;

            case 22:
                hideStoryWindow();
                Globals.LevelStats4 += "\nעוצמה: " + Globals.Reverse(launchesCounter.ToString()) + " שיגורים";
                launchesCounter = 0;
                counter = 23;
                break;

            case 23:
                showStoryWindow("גם עכשיו - הטיל עדיין מתרסק בסוף. למה בעצם? כי רק לשגר אותו זה לא מספיק...", true);
                break;

            case 24:
                hideStoryWindow();
                counter = 25;
                break;

            case 25:
                showStoryWindow("צריך לעזור לו להגיע למסלול יציב סביב הכדור, על ידי דחיפה קלה שתמנע את ההתרסקות שלו", true);
                break;

            case 26:
                hideStoryWindow();
                counter = 27;
                break;


            case 27:
                showStoryWindow("את הדחיפה תוכלו לתת לטיל בעזרת הכפתור הכתום שיופיע לאחר השיגור", false);
                showPushAfterLaunch = true;
                crashesCounter = 0;
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 28;
                }
                break;

            case 28:
                SlideFromTop.exitAnimationTrigger = false;
                showResetAfterLaunch = true;
                Rocket.pushForce = 0.8f;
                showInstructionWindow("הכניסו את הטיל למסלול יציב בעזרת שימוש בדחיפה");
                if (typewriterUI.TypeWriterIsFinished && Globals.rocketStatus == "pushed")
                {
                    hideStoryWindow();
                    counter = 29;
                }

                break;

            case 29:
                if (Globals.rocketStatus == "launched")
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    counter = 30;
                }
                break;

            case 30:
                if (Globals.rocketStatus == "launched")
                {
                    flytime += Time.deltaTime;
                }
                else if (Globals.rocketStatus == "crashed")
                {
                    flytime = 0;
                }
                if (Rocket.Eccentricity > 0.7)
                {
                    if (RocketHasBeenPushed)
                    {
                        showStoryWindow("הטיל שלכם לא במסלול בכלל", false);
                    }
                    else
                    {
                        showStoryWindow("הטיל שלכם עומד להתרסק", false);
                    }
                }
                else if (Rocket.Eccentricity > 0 && RocketHasBeenPushed && flytime>=1)
                {
                    pushBTN.SetActive(false);
                    resetBTN.SetActive(false);
                    showResetAfterLaunch = false;
                    showPushAfterLaunch = false;
                    hideStoryWindow();
                    hideInstructionWindow();
                    counter = 31;
                }
                break;

            case 31:
                flytime = 0;
                showStoryWindow("כל הכבוד - אתם יודעים עכשיו לשגר טיל למסלול יציב!", true);
                resetBTN.SetActive(false);
                break;

            case 32:
                hideStoryWindow();
                Globals.LevelStats4 += "\nיציב: " + Globals.Reverse(launchesCounter.ToString()) + " שיגורים, "+ Globals.Reverse(crashesCounter.ToString()) + " התרסקויות";
                launchesCounter = 0;
                crashesCounter = 0;
                counter = 33;
                break;

            case 33:
                if (Rocket.Eccentricity > 0.15)
                {
                    showStoryWindow("אבל... לא נעים לנו להגיד... המסלול שלכם אליפטי מידי...", true);
                }
                else
                {
                    showStoryWindow("ולא רק זה - גם שיגרתם אותו למסלול מעגלי - כל הכבוד!", true);
                }
                break;

            case 34:
                hideStoryWindow();
                counter = 35;
                break;

            case 35:
                if (Rocket.Eccentricity > 0.15)
                {
                    showStoryWindow("נסו לתת את הדחיפה כשהטיל נמצא בנקודה הכי רחוקה במסלול שלו, רגע לפני שהוא מתחיל לחזור לכדור הארץ", true);
                }
                else
                {
                    showStoryWindow("הצלחתם כי נתתם לטיל את הדחיפה כשהוא היה הכי רחוק מכדור הארץ במסלול שלו", true);
                }
                break;

            case 36:
                hideStoryWindow();
                showPushAfterLaunch = true;
                showResetAfterLaunch = true;
                Globals.rocketStatus = "crashed";
                if (Rocket.Eccentricity > 0.15)
                {
                    counter = 37;
                }
                else
                {
                    Globals.LevelStats4 += "\nמעגלי: " + Globals.Reverse(launchesCounter.ToString()) + " שיגורים, " + Globals.Reverse(crashesCounter.ToString()) + " התרסקויות";
                    counter = 42;
                }
                break;

            case 37:
                showInstructionWindow("שגרו את הטיל למסלול יציב ומעגלי סביב כדור הארץ");
                crashesCounter = 0;
                resetCounter = 0;
                if (typewriterUI.TypeWriterIsFinished && Globals.rocketStatus == "pushed")
                {
                    counter = 38;
                }
                break;

            case 38:
                if (Globals.rocketStatus!= "toLaunch" || Globals.rocketStatus != "crashed")
                {
                    if (Rocket.Eccentricity > 0.7)
                    {
                        eccentricityFeedback = "הטיל שלכם לא במסלול בכלל";
                    }
                    else if (Rocket.Eccentricity > 0.15)
                    {
                        eccentricityFeedback = "הטיל שלכם במסלול אליפטי מידי";
                    }
                    else if (RocketHasBeenPushed)
                    {
                        eccentricityFeedback = "כל הכבוד - אתם יודעים עכשיו לשגר טיל למסלול גם יציב וגם מעגלי!";
                        LEOexample.SetActive(false);
                        MEOexample.SetActive(false);
                        GEOexample.SetActive(false);
                        pushBTN.SetActive(false);
                        resetBTN.SetActive(false);
                        hideInstructionWindow();
                        hideStoryWindow();
                        counter = 40;
                    }
                    showStoryWindow(eccentricityFeedback, false);
                }
                if (crashesCounter+resetCounter >= 3 && Globals.rocketStatus == "crashed")
                {
                    MEOexample.SetActive(true);
                    showStoryWindow("רוצים רמז? ככה המסלול אמור להיראות", false);
                }
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 39;
                }
                break;

            case 39:
                if (Globals.rocketStatus != "toLaunch" || Globals.rocketStatus != "crashed")
                {
                    if (Rocket.Eccentricity > 0.7)
                    {
                        eccentricityFeedback = "הטיל שלכם לא במסלול בכלל";
                    }
                    else if (Rocket.Eccentricity > 0.15)
                    {
                        eccentricityFeedback = "הטיל שלכם במסלול אליפטי מידי";
                    }
                    else if (RocketHasBeenPushed)
                    {
                        eccentricityFeedback = "כל הכבוד - אתם יודעים עכשיו לשגר טיל למסלול גם יציב וגם מעגלי!";
                        LEOexample.SetActive(false);
                        MEOexample.SetActive(false);
                        GEOexample.SetActive(false);
                        pushBTN.SetActive(false);
                        resetBTN.SetActive(false);
                        showResetAfterLaunch = false;
                        hideInstructionWindow();
                        hideStoryWindow();
                        counter = 40;
                    }
                    StoryWindow.GetComponentInChildren<TextMeshProUGUI>().text = eccentricityFeedback;
                }
                if (crashesCounter + resetCounter >= 3 && Globals.rocketStatus=="crashed")
                {
                    MEOexample.SetActive(true);
                    StoryWindow.GetComponentInChildren<TextMeshProUGUI>().text = "רוצים רמז? המסלול אמור להיראות כמו הסימון בנקודות הירוקות";
                }
                break;

            case 40:
                showStoryWindow("כל הכבוד - אתם יודעים עכשיו לשגר טיל למסלול גם יציב וגם מעגלי!", true);
                resetBTN.SetActive(false);
                break;

            case 41:
                hideStoryWindow();
                Globals.rocketStatus = "crashed";
                resetBTN.SetActive(false);
                showLauncherAfterCrash = false;
                Globals.LevelStats4 += "\nמעגלי: " + Globals.Reverse(launchesCounter.ToString()) + " שיגורים, " + Globals.Reverse(crashesCounter.ToString()) + " התרסקויות";
                counter = 42;
                break;

            case 42:
                showStoryWindow("סביב כדור הארץ יש כמה מסלולים שונים של לוויינים", true);
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    Rocket.pushForce = 0.6f;
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    Rocket.pushForce = 0.8f;
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    Rocket.pushForce = 0.9f;
                }
                Orbits.SetActive(true);
                resetBTN.SetActive(false);
                break;

            case 43:
                hideStoryWindow();
                typewriterUI.TextToType = "נמוך\nבינוני\nגיאוסינכרוני";
                OrbitsNames.SetActive(true);
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 44;
                }
                break;

            case 44:
                showStoryWindow("אלה שלושת המסלולים העיקריים", true);
                StoryWinAnim.exitAnimationTrigger = false;
                break;

            case 45:
                hideStoryWindow();
                counter = 46;
                break;

            case 46:
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("לוויין המיפוי שבניתם צריך לראות את כדור הארץ מקרוב, ולכן...", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("לוויין הניווט שבניתם צריך להקיף את כדור הארץ פעמיים ביום, ולכן...", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("לוויין התקשורת שבניתם צריך להיות מסונכרן עם סיבוב כדור הארץ, ולכן...", true);
                }
                break;

            case 47:
                hideStoryWindow();
                counter = 48;
                break;

            case 48:
                launcher.SetActive(true);
                showLauncherAfterCrash = true;
                showResetAfterLaunch= true;
                showPushAfterLaunch= true;
                launchesCounter = 0;
                crashesCounter = 0;
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showInstructionWindow("שגרו את הטיל למסלול לווייני נמוך");
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showInstructionWindow("שגרו את הטיל למסלול לווייני בינוני");
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showInstructionWindow("שגרו את הטיל למסלול לווייני גיאוסינכרוני");
                }
                SlideFromTop.exitAnimationTrigger = false;
                if (typewriterUI.TypeWriterIsFinished)
                {
                    counter = 49;
                }
                break;

            case 49:
                if (Globals.rocketStatus == "crashed")
                {
                    OrbitsNames.SetActive(false);
                }
                if (Globals.rocketStatus == "crashed" && launchesCounter >= 2)
                {
                    if (!Globals.demo)
                    {
                        demoBTN.SetActive(true);
                    }
                    else
                    {
                        demoBTN.SetActive(false);
                    }
                }
                if (Globals.rocketStatus == "pushed")
                {
                    pushBTN.SetActive(false);
                }
                if (checkOrbit())
                {
                    counter = 50;
                }
                break;

            case 50:
                hideInstructionWindow();
                hideStoryWindow();
                demoBTN.SetActive(false);
                Orbits.SetActive(false);
                resetBTN.SetActive(false);
                showResetAfterLaunch = false;
                OrbitsNames.SetActive(false);
                Globals.LevelStats4 += "\nנכון: " + Globals.Reverse(launchesCounter.ToString()) + " שיגורים, " + Globals.Reverse(crashesCounter.ToString()) + " התרסקויות";
                counter = 51;
                break;

            case 51:
                StoryWinAnim.exitAnimationTrigger = false;
                if (Globals.ChosenSatellite.Orbit == "LEO")
                {
                    showStoryWindow("כל הכבוד! הצלחתם לשגר טיל למסלול לווייני נמוך", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "MEO")
                {
                    showStoryWindow("כל הכבוד! הצלחתם לשגר טיל למסלול לווייני בינוני", true);
                }
                else if (Globals.ChosenSatellite.Orbit == "GEO")
                {
                    showStoryWindow("כל הכבוד! הצלחתם לשגר טיל למסלול לווייני גיאוסינכרוני", true);
                }
                break;

            case 52:
                hideStoryWindow();
                hideInstructionWindow();
                if (!StoryWindow.activeSelf && !InstructionWindow.activeSelf)
                {
                    StoryWinAnim.exitAnimationTrigger = false;
                    SlideFromTop.exitAnimationTrigger = false;
                    counter = 53;
                }
                break;

            case 53:
                Globals.LevelStats4 += "\nזמן כולל: " + Globals.Reverse(Mathf.RoundToInt(BuildIU.totalTime).ToString()) + " שניות";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                counter = 54;
                break;

        }


    }

    public void advanceCounter()
    {
        counter++;
        //Debug.Log(counter);
    }

    void showStoryWindow(string textContent, bool showBtn)
    {
        if (!StoryWindow.activeSelf)
        {
            typewriterUI.TextToType = textContent;

            StoryWindow.SetActive(true);
        }

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
        //InstructionBTN.SetActive(false);
        InstructionWindow.SetActive(true);
        //if (showBtnInstWin)
        //{
        //    if (typewriterUI.TypeWriterIsFinished)
        //    {
        //        InstructionBTN.SetActive(true);
        //    }
        //}
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

    private bool checkOrbit()
    {
        if (Globals.rocketStatus == "launched" || Globals.rocketStatus == "launching" || Globals.rocketStatus == "pushed")
        {
            //בדיקת מסלול
            if (Globals.orbit == Globals.ChosenSatellite.Orbit && Globals.demo == false)
            {
                Globals.orbitTime += Time.deltaTime;
                //Debug.Log(" :ןוכנה לולסמב ןמז" + Environment.NewLine + Math.Round(Globals.orbitTime, 2).ToString());

                if (Globals.orbitTime >= neededOrbitTime)
                {
                    //winPanel.SetActive(true);
                    //winPanel.GetComponentInChildren<TextMeshProUGUI>().text = "!םתחלצה" + Environment.NewLine + "לולסמל םתעגה" + Environment.NewLine + "!" + Globals.ChosenSatellite.Orbit;
                    //meter.text = "";
                    Globals.rocketStatus = "inOrbit";

                    return true;
                    //demoBtn.gameObject.SetActive(false);
                    //resetBtn.gameObject.SetActive(false);
                    //pushBtn.gameObject.SetActive(false);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //meter.text = "";
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}