using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLaunchManager : MonoBehaviour
{
    public static int counter = 0;

    [SerializeField] private GameObject InstructionWindow;
    [SerializeField] private GameObject StoryWindow;
    [SerializeField] private GameObject StoryBTN;

    [SerializeField] private GameObject camplace1;
    [SerializeField] private GameObject camplace2;
    [SerializeField] private GameObject camplace3;
    private int camPos = 1;

    [SerializeField] private GameObject rocketTop;
    [SerializeField] private GameObject rocketBottom;
    [SerializeField] private ParticleSystem fire;

    private float pushTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (counter)
        {
            case 0:
                changeCamPosition();
                if (pushTimer < 8)
                {
                    pushTimer += Time.deltaTime;

                    if (pushTimer <= 5)
                    {
                        var fireEmission = fire.emission;
                        fireEmission.enabled = true;
                        fireEmission.rateOverTime = 15;
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
                counter= 2;
                break;

            case 2:
                counter= 3;
                break;

            case 3:
                //changeCamPosition();
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

    void changeCamPosition()
    {
        if (camPos == 1 && counter == 0)
        {
            camPos = 2;
            camplace1.LeanMoveLocal(camplace2.transform.localPosition, 10).setDelay(1).setEaseInOutQuad();
        }
        else if (camPos == 2 && counter==3) //לשנות קאונטר
        {
            camPos = 3;
            camplace1.LeanMoveLocal(camplace3.transform.localPosition, 10).setDelay(1).setEaseInOutQuad();
        }
    }
}
