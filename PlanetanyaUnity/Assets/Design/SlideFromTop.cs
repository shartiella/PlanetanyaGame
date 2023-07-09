using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideFromTop : MonoBehaviour
{
    private Transform window;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private GameObject textGameObject;
    public float delay = 0;
    public static bool exitAnimationTrigger;
    public static bool activeAnimation;

    private Vector3 initialPosition;
    private Vector3 otherPosition;

    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = GetComponent<Transform>().localPosition;

        otherPosition = new Vector3(0, initialPosition.y + 500, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (exitAnimationTrigger && !activeAnimation)
        {
            exitAnimationTrigger = false;
            exitAnimation();
        }
    }

    private void OnEnable()
    {
        activeAnimation = true;
        transform.localPosition = otherPosition;
        transform.LeanMoveLocal(initialPosition, animationTime).setEaseOutQuart().setDelay(delay).setOnComplete(enableText);
    }

    private void exitAnimation()
    {
        typewriterUI.TypeWriterIsFinished = false;
        activeAnimation = true;

        transform.localPosition = initialPosition;
        transform.LeanMoveLocal(otherPosition, animationTime).setEaseInQuart().setOnComplete(disableSelf);
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
}
