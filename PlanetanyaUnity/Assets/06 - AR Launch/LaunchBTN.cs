using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaunchBTN : MonoBehaviour
{
    [SerializeField] private GameObject cineMachine;
    [SerializeField] private GameObject btnbtn;
    [SerializeField] private GameObject blackBG;
    [SerializeField] private GameObject countdownText;
    [SerializeField] private Transform btnlowerposition;
    private bool startCountdown = false;
    private float launchCountdown = 0.0f;
    private bool endCountdown = false;
    private int numbersShown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountdown)
        {
            launchCountdown += Time.deltaTime;
            if (launchCountdown <= 1)
            {
                showNumber("3");
            }
            else if (launchCountdown <= 2)
            {
                showNumber("2");
            }
            else if (launchCountdown <= 3)
            {
                showNumber("1");
            }
            else if (launchCountdown <= 4)
            {
                showNumber("שיגור!");
            }
            else
            {
                startCountdown = false;
                endCountdown = true;
            }
        }
        else if (endCountdown)
        {
            endtheCountdown();
            endCountdown = false;
        }
    }

    private void OnEnable()
    {
        //Globals.rocketStatus = "ToLaunch";

        Vector3 finalsize =transform.localScale;
        transform.localScale = Vector3.zero;
        transform.LeanScale(finalsize, 1).setEaseOutElastic();
    }

    private void OnMouseDown()
    {
        //Debug.Log("button press");
        ARcanvasManager.counter++;
        //Vector3 lowerbtn = new Vector3(btnbtn.transform.position.x, btnbtn.transform.position.y, btnbtn.transform.position.z);
        btnbtn.LeanMove(btnlowerposition.position, 1.5f).setEaseOutBack().setOnComplete(afteranimation);
    }

    void afteranimation()
    {
        gameObject.LeanScale(Vector3.zero, 0.5f).setEaseOutBack();

        Image bg = blackBG.GetComponent<Image>();
        blackBG.SetActive(true);
        LeanTween.value(blackBG, 0, 0.8f, 0.5f).setOnUpdate((float val) =>
        {
            Color c = bg.color;
            c.a = val;
            bg.color = c;
        });
        startCountdown = true;
    }

    void showNumber(string num)
    {
        if (numbersShown < launchCountdown)
        {
            countdownText.SetActive(false);
            countdownText.GetComponent<TextMeshProUGUI>().text = num;
            countdownText.SetActive(true);
            numbersShown++;
        }
    }

    void endtheCountdown()
    {
        Image bg = blackBG.GetComponent<Image>();
        LeanTween.value(blackBG, 0.8f, 0, 0.5f).setOnUpdate((float val) =>
        {
            Color c = bg.color;
            c.a = val;
            bg.color = c;
        });
        countdownText.transform.LeanScale(Vector3.zero, 0.5f).setEaseOutBack();

        cineMachine.SetActive(true); //לבטל כשנצליח להטמיע מציאות רבודה
        Globals.rocketStatus = "launching";
        gameObject.SetActive(false);
    }
}
