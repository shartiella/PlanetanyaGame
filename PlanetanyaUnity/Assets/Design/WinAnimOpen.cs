using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimOpen : MonoBehaviour
{
    private Transform window;
    private Vector3 winScale;
    private Vector3 startScale;
    public static bool exitAnimationTrigger;
    [SerializeField] private GameObject textGameObject;

    // Start is called before the first frame update
    void Awake()
    {
        window = GetComponent<Transform>();
        winScale = window.localScale;
        startScale = new Vector3(winScale.x, 0, winScale.z);
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
        transform.localScale = startScale;
        transform.LeanScale(winScale, 0.5f).setEaseInOutBack().setOnComplete(enableText); 
        //transform.LeanMoveLocal(initialPosition, 0.5f).setEaseOutBack();
        //transform.LeanMove(initialPosition, 1);

    }

    private void exitAnimation()
    {
        exitAnimationTrigger = false;

        transform.localScale = winScale;
        transform.LeanScale(startScale, 0.5f).setEaseInOutBack().setOnComplete(disableSelf);
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
