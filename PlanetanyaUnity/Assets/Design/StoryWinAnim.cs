using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryWinAnim : MonoBehaviour
{
    [SerializeField] private Transform initialPositionT;
    private Vector3 initialPosition;
    [SerializeField] private Transform otherPositionT;
    private Vector3 otherPosition;
    public float otherPositionY = -500;

    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private GameObject textGameObject;
    public float delay = 0;

    public static bool exitAnimationTrigger = false;
    public static bool activeAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = initialPositionT.localPosition;
        otherPosition = otherPositionT.localPosition;
        initialPositionT.localPosition = otherPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (exitAnimationTrigger && !activeAnimation)
        {
            //Debug.Log("TRUE");
            exitAnimationTrigger = false;
            //textGameObject.SetActive(false);
            exitAnimation();
        }
    }

    private void OnEnable()
    {
        //otherPosition = otherPositionT.localPosition;

        activeAnimation = true;
        transform.LeanMoveLocal(initialPosition, animationTime).setDelay(delay).setEaseOutBack().setOnComplete(enableText);
    }

    private void exitAnimation()
    {
        //זה כנראה קורה יותר מפעם אחת וזו הבעיה
        //Debug.Log("EXIT");
        typewriterUI.TypeWriterIsFinished = false;
        activeAnimation = true;

        //otherPosition = new Vector3(initialPosition.x, initialPosition.y + otherPositionY, initialPosition.z);

        transform.LeanMoveLocal(otherPosition, animationTime).setEaseInBack().setOnComplete(disableSelf);
    }

    void enableText()
    {
        activeAnimation = false;
        textGameObject.SetActive(true);
    }

    void disableSelf()
    {
        activeAnimation = false;
        gameObject.SetActive(false);
    }

    public void OnBackgroundClick()
    {
        Debug.Log("click");
        //typewriterUI._tmpProText.text = "";
        //GetComponentInChildren<TextMeshProUGUI>().text = typewriterUI.TextToType;
        //typewriterUI.TypeWriterIsFinished = true;
        typewriterUI.skipTyping = true;
    }
}
