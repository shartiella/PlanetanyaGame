using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimMove : MonoBehaviour
{
    [SerializeField] private Transform window;
    //[SerializeField] private bool fromTheTop;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private GameObject textGameObject;
    public float delay=0;
    public static bool exitAnimationTrigger;
    public static bool activeAnimation;
    [SerializeField] private Transform lowerPosition;



    //[SerializeField] private Transform upPosition;
    //[SerializeField] private Transform downPosition;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("AWAKE");

        initialPosition = transform.localPosition;
        //lowerPosition = new Vector3(0, initialPosition.y - 500, 0);

        //if (fromTheTop)
        //{
        //    otherPosition = new Vector3(0, initialPosition.y + 500, 0);
        //    //otherPosition = upPosition.localPosition;
        //}
        //else
        //{
        //    otherPosition = new Vector3(0, initialPosition.y - 500, 0);
        //    //otherPosition = downPosition.localPosition;
        //}
    }
    private void Start()
    {
        Debug.Log("START");
    }
    // Update is called once per frame
    void Update()
    {
        if (exitAnimationTrigger && gameObject.activeSelf)
        {
            exitAnimation();
        }
    }

    private void OnEnable()
    {
        Debug.Log("I move from " + lowerPosition + " to " + initialPosition);

        window.localPosition = lowerPosition.localPosition;
        activeAnimation = true;
        window.LeanMoveLocal(initialPosition, animationTime).setEaseOutBack().setDelay(delay).setOnComplete(enableText);
    }

    private void exitAnimation()
    {
        exitAnimationTrigger = false;

        if (exitAnimationTrigger == false)
        {
            Debug.Log("I move from " + initialPosition + " to " + lowerPosition);

            window.localPosition = initialPosition;
            activeAnimation = true;
            //window.LeanMoveLocal(lowerPosition, animationTime).setEaseInBack().setOnComplete(disableSelf);
            //disableSelf();
        }

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
