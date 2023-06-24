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
        if (exitAnimationTrigger)
        {
            exitAnimation();
        }
    }

    private void OnEnable()
    {
        transform.localPosition = otherPosition;
        transform.LeanMoveLocal(initialPosition, animationTime).setEaseOutQuart().setDelay(delay).setOnComplete(enableText);
    }

    private void exitAnimation()
    {
        exitAnimationTrigger = false;

        transform.localPosition = initialPosition;
        transform.LeanMoveLocal(otherPosition, animationTime).setEaseInQuart().setOnComplete(disableSelf);
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
