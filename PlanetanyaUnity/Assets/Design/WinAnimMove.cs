using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimMove : MonoBehaviour
{
    private Transform window;
    [SerializeField] private bool fromTheTop;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private GameObject textGameObject;
    public float delay=0;
    public static bool exitAnimationTrigger;

    [SerializeField] private GameObject upPosition;
    [SerializeField] private GameObject downPosition;

    private Vector3 initialPosition;
    private Vector3 otherPosition;

    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = GetComponent<Transform>().localPosition;

        if (fromTheTop)
        {
            //otherPosition = new Vector3(0, initialPosition.y + Screen.height, 0);
            otherPosition = upPosition.GetComponent<Transform>().localPosition;
        }
        else
        {
            //otherPosition = new Vector3(0, initialPosition.y - Screen.height, 0);
            otherPosition = downPosition.GetComponent<Transform>().localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (exitAnimationTrigger)
        {
            exitAnimation();
        }
    }

    private void OnEnable()
    {
        //transform.localPosition = otherPosition;
        //transform.LeanMoveLocal(initialPosition, animationTime).setEaseOutBack().setDelay(delay).setOnComplete(enableText);
        enableText();
    }

    private void exitAnimation()
    {
        exitAnimationTrigger = false;

        //transform.localPosition = initialPosition;
        //transform.LeanMoveLocal(otherPosition, animationTime).setEaseInBack().setOnComplete(disableSelf);
        disableSelf();
    }

    void enableText()
    {
        textGameObject.SetActive(true);
    }

    void disableSelf()
    {
        gameObject.SetActive(false);
    }

}
